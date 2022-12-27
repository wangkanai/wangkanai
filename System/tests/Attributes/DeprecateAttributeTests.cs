// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Wangkanai.Attributes;

using Xunit;

namespace Wangkanai;

public class DeprecateAttributeTests
{
    [Fact]
    public void DeprecateAttributeTest()
    {
        var attribute = new DeprecateAttribute("1.0.0");
        Assert.Equal("1.0.0", attribute.Message);
    }

    [Fact]
    public void ReplaceWithNewClass()
    {
        var attribute = typeof(OldClass).GetCustomAttribute<DeprecateAttribute<NewClass>>();
        Assert.Equal(nameof(NewClass), attribute.Replacement);
    }
    
    [Fact]
    public void ReplaceWithNewRecord()
    {
        var attribute = typeof(OldRecord).GetCustomAttribute<DeprecateAttribute<NewRecord>>();
        Assert.Equal(nameof(NewRecord), attribute.Replacement);
    }
    
    [Fact]
    public void ReplaceWithNewInterface()
    {
        var attribute = typeof(IOldInterface).GetCustomAttribute<DeprecateAttribute<INewInterface>>();
        Assert.Equal(nameof(INewInterface), attribute.Replacement);
    }
    
    [Fact]
    public void ReplaceWithNewStruct()
    {
        var attribute = typeof(OldStruct).GetCustomAttribute<DeprecateAttribute<NewStruct>>();
        Assert.Equal(nameof(NewStruct), attribute.Replacement);
    }
    
    [Fact]
    public void ReplaceWithNewEnum()
    {
        var attribute = typeof(OldEnum).GetCustomAttribute<DeprecateAttribute<NewEnum>>();
        Assert.Equal(nameof(NewEnum), attribute.Replacement);
    }
}

