// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai;

public class MathTests
{
    [Fact]
    public void DivideWithZero()
    {
        Assert.Equal(0, Math.Divider(1, 0));
    }

    [Fact]
    public void DivideWithOne()
    {
        Assert.Equal(1, Math.Divider(1, 1));
    }
}