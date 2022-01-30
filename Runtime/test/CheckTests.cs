// // Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.
// // The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai;

public class CheckTests
{
    [Fact]
    public void IntegralIsNull()
    {
        byte?  byte1  = null;
        short? short2 = null;
        int?   int4   = null;
        long?  long8  = null;

        Assert.Throws<ArgumentNullException>(() => Check.NotNull(byte1));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(short2));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(int4));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(long8));
    }

    [Fact]
    public void FloatingIsNull()
    {
        float?   float16   = null;
        double?  double32  = null;
        decimal? decimal32 = null;
        
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(float16));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(double32));
        Assert.Throws<ArgumentNullException>(() => Check.NotNull(decimal32));
    }

    [Fact]
    public void ListIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(null));
    }

    [Fact]
    public void ListIsEmpty()
    {
        Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(new List<int>()));
    }

    [Fact]
    public void ListIsExist()
    {
        var list = new List<int>();
        for (int i = 0; i <= 9; i++) list.Add(i);

        Assert.True(Check.NotNullOrEmpty(list));
    }
}