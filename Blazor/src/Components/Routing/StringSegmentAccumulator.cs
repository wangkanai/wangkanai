// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Routing;

internal struct StringSegmentAccumulator
{
	private ReadOnlyMemory<char>        _single;
	private List<ReadOnlyMemory<char>>? _multiple;

	public ReadOnlyMemory<char> this[int index]
	{
		get
		{
			if (index >= Count) throw new IndexOutOfRangeException();

			return Count == 1 ? _single : _multiple![index];
		}
	}

	public int Count { get; private set; }

	public void SetSingle(ReadOnlyMemory<char> value)
	{
		_single = value;

		if (Count != 1)
		{
			if (Count > 1) _multiple = null;

			Count = 1;
		}
	}

	public void Add(ReadOnlyMemory<char> value)
	{
		switch (Count++)
		{
			case 0:
				_single = value;
				break;
			case 1:
				_multiple = new List<ReadOnlyMemory<char>>();
				_multiple.Add(_single);
				_multiple.Add(value);
				_single = default;
				break;
			default:
				_multiple!.Add(value);
				break;
		}
	}
}