// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

using Xunit;

namespace Wangkanai;

public class OperatorTests
{
    [Fact]
    public void ConvertInt32ToDouble()
    {
        int    from = 300;
        double d    = Operator.Convert<int, double>(from);
        int    i    = Operator.Convert<double, int>(d);
        Assert.Equal(i, from);
        Assert.Equal(d, (double)i);
    }

    #region Zero Operators

    [Fact]
    public void Zero()
    {
        Assert.Equal((int)0, Operator<int>.Zero);
        Assert.Equal((float)0, Operator<float>.Zero);
        Assert.Equal(0, Operator<decimal>.Zero);
        Assert.Equal(null, Operator<string>.Zero);
    }

    #endregion

    #region Comparison Operators

    [Fact]
    public void EqualInt32()
    {
        Assert.False(Operator.Equal(54, 270));
        Assert.True(Operator.Equal(54, 54));
    }

    [Fact]
    public void NotEqualInt32()
    {
        Assert.True(Operator.NotEqual(270, 54));
        Assert.False(Operator.NotEqual(230, 230));
    }

    [Fact]
    public void LessThanInt32()
    {
        Assert.True(Operator.LessThan(43, 230));
        Assert.False(Operator.LessThan(230, 43));
        Assert.False(Operator.LessThan(230, 43));
    }

    [Fact]
    public void LessThanOrEqualInt32()
    {
        Assert.True(Operator.LessThanOrEqual(43, 230));
        Assert.True(Operator.LessThanOrEqual(230, 230));
        Assert.False(Operator.LessThanOrEqual(230, 43));
    }

    [Fact]
    public void GreaterThanInt32()
    {
        Assert.False(Operator.GreaterThan(43, 230));
        Assert.False(Operator.GreaterThan(230, 230));
        Assert.True(Operator.GreaterThan(230, 43));
    }

    [Fact]
    public void GreaterThanOrEqualInt32()
    {
        Assert.False(Operator.GreaterThanOrEqual(43, 230));
        Assert.True(Operator.GreaterThanOrEqual(230, 230));
        Assert.True(Operator.GreaterThanOrEqual(230, 43));
    }

    #endregion

    #region Arithmetic Operators

    [Fact]
    public void AddInt32()
    {
        Assert.Equal(43 + 230, Operator.Add(43, 230));
    }

    [Fact]
    public void AddDateTimeTimeSpan()
    {
        DateTime from  = DateTime.Today;
        TimeSpan delta = TimeSpan.FromHours(73.5);
        Assert.Equal(from + delta, Operator.AddAlternative(from, delta));
    }

    [Fact]
    public void AddTestComplex()
    {
        var a = new Complex(12, 3);
        var b = new Complex(2, 5);

        Assert.Equal(a + b, Operator.Add(a, b));
    }

    [Fact]
    public void SubtractInt32()
    {
        Assert.Equal(270 - 54, Operator.Subtract(270, 54));
    }

    [Fact]
    public void SubtractDateTimeTimeSpan()
    {
        DateTime from  = DateTime.Today;
        TimeSpan delta = TimeSpan.FromHours(73.5);
        Assert.Equal(from - delta, Operator.SubtractAlternative(from, delta));
    }

    [Fact]
    public void SubtractTestComplex()
    {
        var a = new Complex(12, 3);
        var b = new Complex(2, 5);

        Assert.Equal(a - b, Operator.Subtract(a, b));
    }

    [Fact]
    public void MultiplyInt32()
    {
        Assert.Equal(270 * 54, Operator.Multiply(270, 54));
    }

    [Fact]
    public void MultiplyString()
    {
        Assert.Throws<InvalidOperationException>(
            () => Operator.Multiply("abc", "def")
        );
    }

    [Fact]
    public void MultiplyFloatInt32()
    {
        float from   = 123.45F;
        int   factor = 12;
        Assert.Equal(from * factor, Operator.Multiply(from, factor));
        Assert.Throws<InvalidOperationException>(
            () => Operator.MultiplyAlternative(from, factor)
        );
    }

    [Fact]
    public void DivideInt32()
    {
        Assert.Equal(270 / 54, Operator.Divide(270, 54));
    }

    [Fact]
    public void DivideDouble()
    {
        Assert.Equal(14514.7 / 45.2, Operator.Divide(14514.7, 45.2));
    }

    [Fact]
    public void DivideInt32DoubleTest()
    {
        Assert.Equal(14514 / 45, Operator.DivideInt32(14514, 45));
    }

    [Fact]
    public void DivideFloatInt32()
    {
        float from    = 123.45F;
        int   divisor = 12;
        //Assert.Equal(from / divisor, Operator.DivideAlternative(from, divisor));
        //Assert.Equal(from / divisor, Operator.DivideInt32(from, divisor));
        Assert.Throws<InvalidOperationException>(
            () => Operator.DivideInt32(from, divisor)
        );
        Assert.Throws<InvalidOperationException>(
            () => Operator.DivideAlternative(from, divisor)
        );
    }

    #endregion

    #region Logic Operators

    [Fact]
    public void NegateInt32()
    {
        Assert.Equal(-270, Operator.Negate(270));
    }


    [Fact]
    public void NegateString()
    {
        Assert.Throws<InvalidOperationException>(
            () => Operator.Negate("abc")
        );
    }

    [Fact]
    public void NotInt32()
    {
        Assert.Equal(~270, Operator.Not(270));
    }

    [Fact]
    public void OrInt32()
    {
        Assert.Equal(270 | 54, Operator.Or(270, 54));
    }

    [Fact]
    public void AndInt32()
    {
        Assert.Equal(230 & 43, Operator.And(230, 43));
    }

    [Fact]
    public void XorInt32()
    {
        Assert.Equal(270 ^ 54, Operator.Xor(270, 54));
    }

    #endregion

    #region Complex Operators

    #endregion
}