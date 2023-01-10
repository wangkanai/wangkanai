// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Blazor.Components.Reflection;
using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Blazor.Components.RenderTree;

namespace Wangkanai.Blazor.Components;

public readonly struct ParameterView
{
	private static readonly RenderTreeFrame[] _emptyFrames =
	{
		RenderTreeFrame.Element(0, string.Empty).WithComponentSubtreeLength(1)
	};

	private readonly ParameterViewLifetime                  _lifetime;
	private readonly RenderTreeFrame[]                      _frames;
	private readonly int                                    _ownerIndex;
	private readonly IReadOnlyList<CascadingParameterState> _cascadingParameters;

	internal ParameterView(in ParameterViewLifetime lifetime, RenderTreeFrame[] frames, int ownerIndex)
		: this(lifetime, frames, ownerIndex, Array.Empty<CascadingParameterState>()) { }

	private ParameterView(in ParameterViewLifetime lifetime, RenderTreeFrame[] frames, int ownerIndex, IReadOnlyList<CascadingParameterState> cascadingParameters)
	{
		_lifetime            = lifetime;
		_frames              = frames;
		_ownerIndex          = ownerIndex;
		_cascadingParameters = cascadingParameters;
	}

	public static ParameterView Empty { get; } = new(ParameterViewLifetime.Unbound, _emptyFrames, 0, Array.Empty<CascadingParameterState>());

	internal ParameterViewLifetime Lifetime
		=> _lifetime;

	public Enumerator GetEnumerator()
	{
		_lifetime.AssertNotExpired();
		return new Enumerator(_frames, _ownerIndex, _cascadingParameters);
	}

	public bool TryGetValue<TValue>(string parameterName, [MaybeNullWhen(false)] out TValue result)
	{
		foreach (var entry in this)
			if (string.Equals(entry.Name, parameterName))
			{
				result = (TValue)entry.Value;
				return true;
			}

		result = default;
		return false;
	}

	public TValue? GetValueOrDefault<TValue>(string parameterName)
	{
		return GetValueOrDefault<TValue?>(parameterName, default);
	}

	public TValue GetValueOrDefault<TValue>(string parameterName, TValue defaultValue)
	{
		return TryGetValue<TValue>(parameterName, out var result) ? result : defaultValue;
	}

	public IReadOnlyDictionary<string, object> ToDictionary()
	{
		var result = new Dictionary<string, object>();
		foreach (var entry in this)
			result[entry.Name] = entry.Value;
		return result;
	}

	internal ParameterView Clone()
	{
		if (ReferenceEquals(_frames, _emptyFrames))
			return Empty;

		var numEntries  = GetEntryCount();
		var cloneBuffer = new RenderTreeFrame[1 + numEntries];
		cloneBuffer[0] = RenderTreeFrame.PlaceholderChildComponentWithSubtreeLength(1 + numEntries);
		_frames.AsSpan(1, numEntries).CopyTo(cloneBuffer.AsSpan(1));

		return new ParameterView(Lifetime, cloneBuffer, _ownerIndex);
	}

	internal ParameterView WithCascadingParameters(IReadOnlyList<CascadingParameterState> cascadingParameters)
	{
		return new ParameterView(_lifetime, _frames, _ownerIndex, cascadingParameters);
	}

	internal bool DefinitelyEquals(ParameterView oldParameters)
	{
		var oldIndex        = oldParameters._ownerIndex;
		var newIndex        = _ownerIndex;
		var oldEndIndexExcl = oldIndex + oldParameters._frames[oldIndex].ComponentSubtreeLengthField;
		var newEndIndexExcl = newIndex + _frames[newIndex].ComponentSubtreeLengthField;
		while (true)
		{
			// First, stop if we've reached the end of either subtree
			oldIndex++;
			newIndex++;
			var oldFinished = oldIndex == oldEndIndexExcl;
			var newFinished = newIndex == newEndIndexExcl;
			if (oldFinished || newFinished) return oldFinished == newFinished; // Same only if we have same number of parameters

			// Since neither subtree has finished, it's safe to read the next frame from both
			ref var oldFrame = ref oldParameters._frames[oldIndex];
			ref var newFrame = ref _frames[newIndex];

			// Stop if we've reached the end of either subtree's sequence of attributes
			oldFinished = oldFrame.FrameTypeField != RenderTreeFrameType.Attribute;
			newFinished = newFrame.FrameTypeField != RenderTreeFrameType.Attribute;
			if (oldFinished || newFinished) return oldFinished == newFinished; // Same only if we have same number of parameters

			if (!string.Equals(oldFrame.AttributeNameField, newFrame.AttributeNameField, StringComparison.Ordinal))
				return false; // Different names

			var oldValue = oldFrame.AttributeValueField;
			var newValue = newFrame.AttributeValueField;
			if (ChangeDetection.MayHaveChanged(oldValue, newValue))
				return false;
		}
	}

	internal bool HasRemovedDirectParameters(in ParameterView oldParameters)
	{
		var oldDirectParameterFrames = GetDirectParameterFrames(oldParameters);
		if (oldDirectParameterFrames.Length == 0)
			return false;

		var newDirectParameterFrames = GetDirectParameterFrames(this);
		if (newDirectParameterFrames.Length < oldDirectParameterFrames.Length)
			return true;

		// Fall back to comparing each set of direct parameters.
		foreach (var oldFrame in oldDirectParameterFrames)
		{
			var found = false;
			foreach (var newFrame in newDirectParameterFrames)
				if (string.Equals(oldFrame.AttributeNameField, newFrame.AttributeNameField, StringComparison.Ordinal))
				{
					found = true;
					break;
				}

			if (!found)
				return true;
		}

		return false;

		static Span<RenderTreeFrame> GetDirectParameterFrames(in ParameterView parameterView)
		{
			var frames                       = parameterView._frames;
			var ownerIndex                   = parameterView._ownerIndex;
			var ownerDescendantsEndIndexExcl = ownerIndex + frames[ownerIndex].ElementSubtreeLength;
			var attributeFramesStartIndex    = ownerIndex + 1;
			var attributeFramesEndIndexExcl  = attributeFramesStartIndex;

			while (attributeFramesEndIndexExcl < ownerDescendantsEndIndexExcl && frames[attributeFramesEndIndexExcl].FrameType == RenderTreeFrameType.Attribute)
				attributeFramesEndIndexExcl++;

			return frames.AsSpan(attributeFramesStartIndex..attributeFramesEndIndexExcl);
		}
	}

	internal void CaptureSnapshot(ArrayBuilder<RenderTreeFrame> builder)
	{
		builder.Clear();

		var numEntries = GetEntryCount();

		var owner = RenderTreeFrame.PlaceholderChildComponentWithSubtreeLength(1 + numEntries);
		builder.Append(owner);

		if (numEntries > 0)
			builder.Append(_frames, _ownerIndex + 1, numEntries);
	}

	private int GetEntryCount()
	{
		var numEntries = 0;
		foreach (var _ in this)
			numEntries++;

		return numEntries;
	}

	public static ParameterView FromDictionary(IDictionary<string, object?> parameters)
	{
		var builder = new ParameterViewBuilder(parameters.Count);
		foreach (var kvp in parameters) builder.Add(kvp.Key, kvp.Value);

		return builder.ToParameterView();
	}

	public void SetParameterProperties(object target)
	{
		if (target is null)
			throw new ArgumentNullException(nameof(target));

		ComponentProperties.SetProperties(this, target);
	}

	public struct Enumerator
	{
		private RenderTreeFrameParameterEnumerator _directParamsEnumerator;
		private CascadingParameterEnumerator       _cascadingParameterEnumerator;
		private bool                               _isEnumeratingDirectParams;

		internal Enumerator(RenderTreeFrame[] frames, int ownerIndex, IReadOnlyList<CascadingParameterState> cascadingParameters)
		{
			_directParamsEnumerator       = new RenderTreeFrameParameterEnumerator(frames, ownerIndex);
			_cascadingParameterEnumerator = new CascadingParameterEnumerator(cascadingParameters);
			_isEnumeratingDirectParams    = true;
		}

		public ParameterValue Current => _isEnumeratingDirectParams
			                                 ? _directParamsEnumerator.Current
			                                 : _cascadingParameterEnumerator.Current;

		public bool MoveNext()
		{
			if (_isEnumeratingDirectParams)
			{
				if (_directParamsEnumerator.MoveNext())
					return true;
				else
					_isEnumeratingDirectParams = false;
			}

			return _cascadingParameterEnumerator.MoveNext();
		}
	}

	private struct RenderTreeFrameParameterEnumerator
	{
		private readonly RenderTreeFrame[] _frames;
		private readonly int               _ownerIndex;
		private readonly int               _ownerDescendantsEndIndexExcl;
		private          int               _currentIndex;
		private          ParameterValue    _current;

		internal RenderTreeFrameParameterEnumerator(RenderTreeFrame[] frames, int ownerIndex)
		{
			_frames                       = frames;
			_ownerIndex                   = ownerIndex;
			_ownerDescendantsEndIndexExcl = ownerIndex + _frames[ownerIndex].ElementSubtreeLengthField;
			_currentIndex                 = ownerIndex;
			_current                      = default;
		}

		public ParameterValue Current => _current;

		public bool MoveNext()
		{
			var nextIndex = _currentIndex + 1;
			if (nextIndex == _ownerDescendantsEndIndexExcl)
				return false;

			if (_frames[nextIndex].FrameTypeField != RenderTreeFrameType.Attribute)
				return false;

			_currentIndex = nextIndex;

			ref var frame = ref _frames[_currentIndex];
			_current = new ParameterValue(frame.AttributeNameField, frame.AttributeValueField, false);

			return true;
		}
	}

	private struct CascadingParameterEnumerator
	{
		private readonly IReadOnlyList<CascadingParameterState> _cascadingParameters;
		private          int                                    _currentIndex;
		private          ParameterValue                         _current;

		public CascadingParameterEnumerator(IReadOnlyList<CascadingParameterState> cascadingParameters)
		{
			_cascadingParameters = cascadingParameters;
			_currentIndex        = -1;
			_current             = default;
		}

		public ParameterValue Current => _current;

		public bool MoveNext()
		{
			var nextIndex = _currentIndex + 1;
			if (nextIndex < _cascadingParameters.Count)
			{
				_currentIndex = nextIndex;

				var state = _cascadingParameters[_currentIndex];
				_current = new ParameterValue(state.LocalValueName, state.ValueSupplier.CurrentValue!, true);
				return true;
			}

			return false;
		}
	}
}