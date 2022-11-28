// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Moq;

using Xunit;

namespace Wangkanai.Collections;

public class ComparisonComparerTests
{
    [Fact]
    public void ConstructorWithNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => new ComparisonComparer<string>(null)
        );
    }

    [Fact]
    public void CreateComparisonWithNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => ComparisonComparer<string>.CreateComparison(null)
        );
    }

    [Fact]
    public void CreateAndCall()
    {
        var mock = new Mock<Comparison<string>>();
        mock.Setup(x => x("hello", "there"))
            .Returns(5);
        mock.Setup(x => x("x", "y"))
            .Returns(-3);
        mock.Setup(x => x("throw", "exception"))
            .Throws<Exception>();

        var comparer = new ComparisonComparer<string>(mock.Object);
        Assert.Equal(5, comparer.Compare("hello", "there"));
        Assert.Equal(-3, comparer.Compare("x", "y"));
        Assert.Throws<Exception>(() => comparer.Compare("throw", "exception"));
    }

    [Fact]
    public void CreateAndCallComparison()
    {
        var mock = new Mock<IComparer<string>>();
        mock.Setup(x => x.Compare("hello", "there"))
            .Returns(5);
        mock.Setup(x => x.Compare("x", "y"))
            .Returns(-3);
        mock.Setup(x => x.Compare("throw", "exception"))
            .Throws<Exception>();

        var comparison = ComparisonComparer<string>.CreateComparison(mock.Object);
        Assert.Equal(5, comparison("hello", "there"));
        Assert.Equal(-3, comparison("x", "y"));
        Assert.Throws<Exception>(() => comparison("throw", "exception"));
    }
}