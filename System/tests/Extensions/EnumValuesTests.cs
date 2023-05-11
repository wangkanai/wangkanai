// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.System.Extensions;

public class EnumValuesTests
{
    [Fact]
    public void GetFlags()
    {
        var one = Fruit.Apple;
        Assert.Single(one.GetFlags());
    }

    [Fact]
    public void GetValues()
    {
        var values = EnumValues<Fruit>.GetValues();
        Assert.Equal(4, values.Length);
    }

    [Fact]
    public void GetValesGeneric()
    {
        Fruit.Apple.GetValues();
    }
}

[Flags]
public enum Fruit
{
    Apple  = 0,
    Orange = 1 << 0,
    Pear   = 1 << 1,
    Banana = 1 << 2
}