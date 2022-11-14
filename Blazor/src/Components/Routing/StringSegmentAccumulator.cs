// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Routing;

internal struct StringSegmentAccumulator
{
    private int                         count;
    private ReadOnlyMemory<char>        _single;
    private List<ReadOnlyMemory<char>>? _multiple;

    public ReadOnlyMemory<char> this[int index]
    {
        get
        {
            if (index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            return count == 1 ? _single : _multiple![index];
        }
    }

    public int Count => count;

    public void SetSingle(ReadOnlyMemory<char> value)
    {
        _single = value;

        if (count != 1)
        {
            if (count > 1)
            {
                _multiple = null;
            }

            count = 1;
        }
    }

    public void Add(ReadOnlyMemory<char> value)
    {
        switch (count++)
        {
            case 0:
                _single = value;
                break;
            case 1:
                _multiple = new();
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
