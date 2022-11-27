// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Runtime;

public class OperatorTests
{
    [Fact]
    public void ConvertInt32ToDouble()
    {
        int    from = 300;
        double d    = Operator.Convert<int, double>(from);
        int    i    = Operator.Convert<double, int>(d);
        Assert.Equal(i, from);
        Assert.Equal(d,(double)i);
    }

    [Fact]
    public void XorInt32()
    {
        Assert.Equal(270 ^ 54, Operator.Xor(270,54));
    }
}