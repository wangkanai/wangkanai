// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Wangkanai.Blazor.Components.RenderTree;
using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components.Rendering;

public sealed class RenderTreeBuilder : IDisposable
{
	private const           string ChildContent = nameof(ChildContent);
	private static readonly object BoxedTrue    = true;
	private static readonly object BoxedFalse   = false;

	private static readonly string ComponentReferenceCaptureInvalidParentMessage = $"Component reference captures may only be added as children of frames of type {RenderTreeFrameType.Component}";

	private readonly RenderTreeFrameArrayBuilder _entries            = new();
	private readonly Stack<int>                  _openElementIndices = new();
	private          bool                        _hasSeenAddMultipleAttributes;
	private          RenderTreeFrameType?        _lastNonAttributeFrameType;
	private          Dictionary<string, int>?    _seenAttributeNames;

	public void Dispose()
	{
		_entries.Dispose();
	}

	public void OpenElement(int sequence, string elementName)
	{
		if (_hasSeenAddMultipleAttributes)
		{
			var indexOfLastElementOrComponent = _openElementIndices.Peek();
			ProcessDuplicateAttributes(indexOfLastElementOrComponent + 1);
		}

		_openElementIndices.Push(_entries.Count);
		_entries.AppendElement(sequence, elementName);
		_lastNonAttributeFrameType = RenderTreeFrameType.Element;
	}

	public void CloseElement()
	{
		var indexOfEntryBeingClosed = _openElementIndices.Pop();

		if (_hasSeenAddMultipleAttributes)
			ProcessDuplicateAttributes(indexOfEntryBeingClosed + 1);

		_entries.Buffer[indexOfEntryBeingClosed].ElementSubtreeLengthField = _entries.Count - indexOfEntryBeingClosed;
	}

	public void AddMarkupContent(int sequence, string? markupContent)
	{
		_entries.AppendMarkup(sequence, markupContent ?? string.Empty);
		_lastNonAttributeFrameType = RenderTreeFrameType.Markup;
	}

	public void AddContent(int sequence, string? textContent)
	{
		_entries.AppendText(sequence, textContent ?? string.Empty);
		_lastNonAttributeFrameType = RenderTreeFrameType.Text;
	}

	public void AddContent(int sequence, RenderFragment? fragment)
	{
		if (fragment != null)
		{
			OpenRegion(sequence);
			fragment(this);
			CloseRegion();
		}
	}

	public void AddContent<TValue>(int sequence, RenderFragment<TValue>? fragment, TValue value)
	{
		if (fragment != null)
			AddContent(sequence, fragment(value));
	}

	public void AddContent(int sequence, MarkupString? markupContent)
	{
		AddMarkupContent(sequence, markupContent?.Value);
	}

	public void AddContent(int sequence, MarkupString markupContent)
	{
		AddMarkupContent(sequence, markupContent.Value);
	}

	public void AddContent(int sequence, object? textContent)
	{
		AddContent(sequence, textContent?.ToString());
	}

	public void AddAttribute(int sequence, string name)
	{
		if (_lastNonAttributeFrameType != RenderTreeFrameType.Element) throw new InvalidOperationException($"Valueless attributes may only be added immediately after frames of type {RenderTreeFrameType.Element}");

		_entries.AppendAttribute(sequence, name, BoxedTrue);
	}

	public void AddAttribute(int sequence, string name, bool value)
	{
		AssertCanAddAttribute();
		if (_lastNonAttributeFrameType == RenderTreeFrameType.Component)
			_entries.AppendAttribute(sequence, name, value ? BoxedTrue : BoxedFalse);
		else if (value)
			_entries.AppendAttribute(sequence, name, BoxedTrue);
		else
			TrackAttributeName(name);
	}

	public void AddAttribute(int sequence, string name, string? value)
	{
		AssertCanAddAttribute();
		if (value != null || _lastNonAttributeFrameType == RenderTreeFrameType.Component)
			_entries.AppendAttribute(sequence, name, value);
		else
			TrackAttributeName(name);
	}

	public void AddAttribute(int sequence, string name, MulticastDelegate? value)
	{
		AssertCanAddAttribute();
		if (value != null || _lastNonAttributeFrameType == RenderTreeFrameType.Component)
			_entries.AppendAttribute(sequence, name, value);
		else
			TrackAttributeName(name);
	}

	public void AddAttribute(int sequence, string name, EventCallback value)
	{
		AssertCanAddAttribute();
		if (_lastNonAttributeFrameType == RenderTreeFrameType.Component)
			_entries.AppendAttribute(sequence, name, value);
		else if (value.RequiresExplicitReceiver)
			_entries.AppendAttribute(sequence, name, value);
		else if (value.HasDelegate)
			_entries.AppendAttribute(sequence, name, value.Delegate);
		else
			TrackAttributeName(name);
	}

	public void AddAttribute<TArgument>(int sequence, string name, EventCallback<TArgument> value)
	{
		AssertCanAddAttribute();
		if (_lastNonAttributeFrameType == RenderTreeFrameType.Component)
			_entries.AppendAttribute(sequence, name, value);
		else if (value.RequiresExplicitReceiver)
			_entries.AppendAttribute(sequence, name, value.AsUntyped());
		else if (value.HasDelegate)
			_entries.AppendAttribute(sequence, name, value.Delegate);
		else
			TrackAttributeName(name);
	}

	public void AddAttribute(int sequence, string name, object? value)
	{
		if (_lastNonAttributeFrameType == RenderTreeFrameType.Element)
		{
			if (value == null)
				TrackAttributeName(name);
			else if (value is bool boolValue)
				if (boolValue)
					_entries.AppendAttribute(sequence, name, BoxedTrue);
				else
					TrackAttributeName(name);
			else if (value is IEventCallback callbackValue)
				if (callbackValue.HasDelegate)
					_entries.AppendAttribute(sequence, name, callbackValue.UnpackForRenderTree());
				else
					TrackAttributeName(name);
			else if (value is MulticastDelegate)
				_entries.AppendAttribute(sequence, name, value);
			else
				_entries.AppendAttribute(sequence, name, value.ToString());
		}
		else if (_lastNonAttributeFrameType == RenderTreeFrameType.Component)
		{
			_entries.AppendAttribute(sequence, name, value);
		}
		else
		{
			AssertCanAddAttribute();
		}
	}

	public void AddAttribute(int sequence, RenderTreeFrame frame)
	{
		if (frame.FrameTypeField != RenderTreeFrameType.Attribute)
			throw new ArgumentException($"The {nameof(frame.FrameType)} must be {RenderTreeFrameType.Attribute}.");

		AssertCanAddAttribute();
		frame.SequenceField = sequence;
		_entries.Append(frame);
	}

	public void AddMultipleAttributes(int sequence, IEnumerable<KeyValuePair<string, object>>? attributes)
	{
		AssertCanAddAttribute();

		if (attributes != null)
		{
			_hasSeenAddMultipleAttributes = true;

			foreach (var attribute in attributes)
				AddAttribute(sequence, attribute.Key, attribute.Value);
		}
	}

	public void SetUpdatesAttributeName(string updatesAttributeName)
	{
		if (_entries.Count == 0)
			throw new InvalidOperationException("No preceding attribute frame exists.");

		ref var prevFrame = ref _entries.Buffer[_entries.Count - 1];
		if (prevFrame.FrameTypeField != RenderTreeFrameType.Attribute)
			throw new InvalidOperationException($"Incorrect frame type: '{prevFrame.FrameTypeField}'");

		prevFrame.AttributeEventUpdatesAttributeNameField = updatesAttributeName;
	}

	public void OpenComponent<[DynamicallyAccessedMembers(LinkerFlags.Component)] TComponent>(int sequence) where TComponent : notnull, IComponent
	{
		OpenComponentUnchecked(sequence, typeof(TComponent));
	}

	public void OpenComponent(int sequence, [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
	{
		if (!typeof(IComponent).IsAssignableFrom(componentType))
			throw new ArgumentException($"The component type must implement {typeof(IComponent).FullName}.");

		OpenComponentUnchecked(sequence, componentType);
	}

	public void SetKey(object? value)
	{
		if (value == null)
			return;

		var parentFrameIndex = GetCurrentParentFrameIndex();
		if (!parentFrameIndex.HasValue)
			throw new InvalidOperationException("Cannot set a key outside the scope of a component or element.");

		var     parentFrameIndexValue = parentFrameIndex.Value;
		ref var parentFrame           = ref _entries.Buffer[parentFrameIndexValue];
		switch (parentFrame.FrameTypeField)
		{
			case RenderTreeFrameType.Element:
				parentFrame.ElementKeyField = value; // It's a ref var, so this writes to the array
				break;
			case RenderTreeFrameType.Component:
				parentFrame.ComponentKeyField = value; // It's a ref var, so this writes to the array
				break;
			default:
				throw new InvalidOperationException($"Cannot set a key on a frame of type {parentFrame.FrameTypeField}.");
		}
	}

	private void OpenComponentUnchecked(int sequence, [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
	{
		if (_hasSeenAddMultipleAttributes)
		{
			var indexOfLastElementOrComponent = _openElementIndices.Peek();
			ProcessDuplicateAttributes(indexOfLastElementOrComponent + 1);
		}

		_openElementIndices.Push(_entries.Count);
		_entries.AppendComponent(sequence, componentType);
		_lastNonAttributeFrameType = RenderTreeFrameType.Component;
	}

	public void CloseComponent()
	{
		var indexOfEntryBeingClosed = _openElementIndices.Pop();

		if (_hasSeenAddMultipleAttributes)
			ProcessDuplicateAttributes(indexOfEntryBeingClosed + 1);

		_entries.Buffer[indexOfEntryBeingClosed].ComponentSubtreeLengthField = _entries.Count - indexOfEntryBeingClosed;
	}

	public void AddElementReferenceCapture(int sequence, Action<ElementReference> elementReferenceCaptureAction)
	{
		if (GetCurrentParentFrameType() != RenderTreeFrameType.Element)
			throw new InvalidOperationException($"Element reference captures may only be added as children of frames of type {RenderTreeFrameType.Element}");

		_entries.AppendElementReferenceCapture(sequence, elementReferenceCaptureAction);
		_lastNonAttributeFrameType = RenderTreeFrameType.ElementReferenceCapture;
	}

	public void AddComponentReferenceCapture(int sequence, Action<object> componentReferenceCaptureAction)
	{
		var parentFrameIndex = GetCurrentParentFrameIndex();
		if (!parentFrameIndex.HasValue)
			throw new InvalidOperationException(ComponentReferenceCaptureInvalidParentMessage);

		var parentFrameIndexValue = parentFrameIndex.Value;
		if (_entries.Buffer[parentFrameIndexValue].FrameTypeField != RenderTreeFrameType.Component)
			throw new InvalidOperationException(ComponentReferenceCaptureInvalidParentMessage);

		_entries.AppendComponentReferenceCapture(sequence, componentReferenceCaptureAction, parentFrameIndexValue);
		_lastNonAttributeFrameType = RenderTreeFrameType.ComponentReferenceCapture;
	}

	public void OpenRegion(int sequence)
	{
		if (_hasSeenAddMultipleAttributes)
		{
			var indexOfLastElementOrComponent = _openElementIndices.Peek();
			ProcessDuplicateAttributes(indexOfLastElementOrComponent + 1);
		}

		_openElementIndices.Push(_entries.Count);
		_entries.AppendRegion(sequence);
		_lastNonAttributeFrameType = RenderTreeFrameType.Region;
	}

	public void CloseRegion()
	{
		var indexOfEntryBeingClosed = _openElementIndices.Pop();
		_entries.Buffer[indexOfEntryBeingClosed].RegionSubtreeLengthField = _entries.Count - indexOfEntryBeingClosed;
	}

	private void AssertCanAddAttribute()
	{
		if (_lastNonAttributeFrameType    != RenderTreeFrameType.Element
		    && _lastNonAttributeFrameType != RenderTreeFrameType.Component)
			throw new InvalidOperationException($"Attributes may only be added immediately after frames of type {RenderTreeFrameType.Element} or {RenderTreeFrameType.Component}");
	}

	private int? GetCurrentParentFrameIndex()
	{
		return _openElementIndices.Count == 0 ? null : _openElementIndices.Peek();
	}

	private RenderTreeFrameType? GetCurrentParentFrameType()
	{
		var parentIndex = GetCurrentParentFrameIndex();
		return parentIndex.HasValue
			       ? _entries.Buffer[parentIndex.Value].FrameTypeField
			       : null;
	}

	public void Clear()
	{
		_entries.Clear();
		_openElementIndices.Clear();
		_lastNonAttributeFrameType    = null;
		_hasSeenAddMultipleAttributes = false;
		_seenAttributeNames?.Clear();
	}

	internal bool InsertAttributeExpensive(int insertAtIndex, int sequence, string attributeName, object? attributeValue)
	{
		// Replicate the same attribute omission logic as used elsewhere
		if (attributeValue == null || attributeValue is bool boolValue && !boolValue)
			return false;

		_entries.InsertExpensive(insertAtIndex, RenderTreeFrame.Attribute(sequence, attributeName, attributeValue));
		return true;
	}

	public ArrayRange<RenderTreeFrame> GetFrames()
	{
		return _entries.ToRange();
	}

	internal void AssertTreeIsValid(IComponent component)
	{
		if (_openElementIndices.Count > 0)
		{
			// It's never valid to leave an element/component/region unclosed. Doing so
			// could cause undefined behavior in diffing.
			ref var invalidFrame = ref _entries.Buffer[_openElementIndices.Peek()];
			throw new InvalidOperationException($"Render output is invalid for component of type '{component.GetType().FullName}'. A frame of type '{invalidFrame.FrameType}' was left unclosed. Do not use try/catch inside rendering logic, because partial output cannot be undone.");
		}
	}

	internal void ProcessDuplicateAttributes(int first)
	{
		Debug.Assert(_hasSeenAddMultipleAttributes);

		var buffer = _entries.Buffer;
		var last   = _entries.Count - 1;

		for (var i = first; i <= last; i++)
			if (buffer[i].FrameTypeField != RenderTreeFrameType.Attribute)
			{
				last = i - 1;
				break;
			}

		// Now that we've found the last attribute, we can iterate backwards and process duplicates.
		var seenAttributeNames = _seenAttributeNames ??= new Dictionary<string, int>(SimplifiedStringHashComparer.Instance);
		for (var i = last; i >= first; i--)
		{
			ref var frame = ref buffer[i];
			Debug.Assert(frame.FrameTypeField == RenderTreeFrameType.Attribute, $"Frame type is {frame.FrameTypeField} at {i}");

			if (!seenAttributeNames.TryAdd(frame.AttributeNameField, i))
			{
				var index = seenAttributeNames[frame.AttributeNameField];
				if (index < i)
					seenAttributeNames[frame.AttributeNameField] = i;
				else if (index > i)
					frame = default;
			}
		}

		var offset = first;
		for (var i = first; i < _entries.Count; i++)
		{
			ref var frame = ref buffer[i];
			if (frame.FrameTypeField != RenderTreeFrameType.None)
				buffer[offset++] = frame;
		}

		var residue = _entries.Count - offset;
		for (var i = 0; i < residue; i++)
			_entries.RemoveLast();

		seenAttributeNames.Clear();
		_hasSeenAddMultipleAttributes = false;
	}

	internal void TrackAttributeName(string name)
	{
		if (!_hasSeenAddMultipleAttributes)
			return;

		var seenAttributeNames = _seenAttributeNames ??= new Dictionary<string, int>(SimplifiedStringHashComparer.Instance);
		seenAttributeNames[name] = _entries.Count; // See comment in ProcessAttributes for why this is OK.
	}
}