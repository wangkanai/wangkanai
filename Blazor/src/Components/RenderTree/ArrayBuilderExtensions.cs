// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


namespace Wangkanai.Blazor.Components.RenderTree;

internal static class ArrayBuilderExtensions
{
    public static ArrayRange<T> ToRange<T>(this ArrayBuilder<T> builder)
        => new ArrayRange<T>(builder.Buffer, builder.Count);

    public static ArrayBuilderSegment<T> ToSegment<T>(this ArrayBuilder<T> builder, int fromIndexInclusive, int toIndexExclusive)
        => new ArrayBuilderSegment<T>(builder, fromIndexInclusive, toIndexExclusive - fromIndexInclusive);
}