// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Wangkanai.Internal;

public readonly struct QueryStringEnumerable
{
	private readonly ReadOnlyMemory<char> _queryString;

	public QueryStringEnumerable(string? queryString)
		: this(queryString.AsMemory()) { }

	public QueryStringEnumerable(ReadOnlyMemory<char> queryString)
	{
		_queryString = queryString;
	}

	public Enumerator GetEnumerator()
	{
		return new Enumerator(_queryString);
	}

	public readonly struct EncodedNameValuePair
	{
		public readonly ReadOnlyMemory<char> EncodedName  { get; }
		public readonly ReadOnlyMemory<char> EncodedValue { get; }

		internal EncodedNameValuePair(ReadOnlyMemory<char> encodedName, ReadOnlyMemory<char> encodedValue)
		{
			EncodedName  = encodedName;
			EncodedValue = encodedValue;
		}

		public ReadOnlyMemory<char> DecodeName()
		{
			return Decode(EncodedName);
		}

		public ReadOnlyMemory<char> DecodeValue()
		{
			return Decode(EncodedValue);
		}

		private static ReadOnlyMemory<char> Decode(ReadOnlyMemory<char> chars)
		{
			return chars.Length < 16 && chars.Span.IndexOfAny('%', '+') < 0
				       ? chars
				       : Uri.UnescapeDataString(SpanHelper.ReplacePlusWithSpace(chars.Span)).AsMemory();
		}
	}

	public struct Enumerator
	{
		private ReadOnlyMemory<char> _query;

		internal Enumerator(ReadOnlyMemory<char> query)
		{
			Current = default;
			_query = query.IsEmpty || query.Span[0] != '?'
				         ? query
				         : query.Slice(1);
		}

		public EncodedNameValuePair Current { get; private set; }

		public bool MoveNext()
		{
			while (!_query.IsEmpty)
			{
				// Chomp off the next segment
				ReadOnlyMemory<char> segment;
				var                  delimiterIndex = _query.Span.IndexOf('&');
				if (delimiterIndex >= 0)
				{
					segment = _query.Slice(0, delimiterIndex);
					_query  = _query.Slice(delimiterIndex + 1);
				}
				else
				{
					segment = _query;
					_query  = default;
				}

				// If it's nonempty, emit it
				var equalIndex = segment.Span.IndexOf('=');
				if (equalIndex >= 0)
				{
					Current = new EncodedNameValuePair(
						segment.Slice(0, equalIndex),
						segment.Slice(equalIndex + 1));
					return true;
				}

				if (!segment.IsEmpty)
				{
					Current = new EncodedNameValuePair(segment, default);
					return true;
				}
			}

			Current = default;
			return false;
		}
	}

	private static class SpanHelper
	{
		private static readonly SpanAction<char, IntPtr> s_replacePlusWithSpace = ReplacePlusWithSpaceCore;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe string ReplacePlusWithSpace(ReadOnlySpan<char> span)
		{
			fixed (char* ptr = &MemoryMarshal.GetReference(span))
			{
				return string.Create(span.Length, (IntPtr)ptr, s_replacePlusWithSpace);
			}
		}

		private static unsafe void ReplacePlusWithSpaceCore(Span<char> buffer, IntPtr state)
		{
			fixed (char* ptr = &MemoryMarshal.GetReference(buffer))
			{
				var input  = (ushort*)state.ToPointer();
				var output = (ushort*)ptr;

				var i = (nint)0;
				var n = (nint)(uint)buffer.Length;

				if (Vector256.IsHardwareAccelerated && n >= Vector256<ushort>.Count)
				{
					var vecPlus  = Vector256.Create((ushort)'+');
					var vecSpace = Vector256.Create((ushort)' ');

					do
					{
						var vec  = Vector256.Load(input + i);
						var mask = Vector256.Equals(vec, vecPlus);
						var res  = Vector256.ConditionalSelect(mask, vecSpace, vec);
						res.Store(output + i);
						i += Vector256<ushort>.Count;
					} while (i <= n - Vector256<ushort>.Count);
				}

				if (Vector128.IsHardwareAccelerated && n - i >= Vector128<ushort>.Count)
				{
					var vecPlus  = Vector128.Create((ushort)'+');
					var vecSpace = Vector128.Create((ushort)' ');

					do
					{
						var vec  = Vector128.Load(input + i);
						var mask = Vector128.Equals(vec, vecPlus);
						var res  = Vector128.ConditionalSelect(mask, vecSpace, vec);
						res.Store(output + i);
						i += Vector128<ushort>.Count;
					} while (i <= n - Vector128<ushort>.Count);
				}

				for (; i < n; ++i)
					if (input[i] != '+')
						output[i] = input[i];
					else
						output[i] = ' ';
			}
		}
	}
}