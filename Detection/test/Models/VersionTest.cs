// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

public class VersionTests
{
    [Theory]
    //[InlineData("1")]
    [InlineData("1.1")]
    [InlineData("1.1.0")]
    [InlineData("1.1.1.1")]
    public void VersionString_To_VersionObj(string value)
    {
        var version = new Version(value);

        Assert.Equal(value, version.ToString());
    }

    [Fact]
    public void Ctor_StringString_Success()
    {
        int major = 1;
        int minor = 0;
        var version = new Version(major, minor);

        AssertVersion(major, minor, version);
        Assert.Equal("1.0", version.ToString());
    }

    [Fact]
    public void Ctor_StringStringStringString_Success()
    {
        var major = 1;
        var minor = 0;
        var build = 1;
        var revision = 0;
        var version = new Version(major, minor, build, revision);

        AssertVersion(major, minor, build, revision, version);
        Assert.Equal("1.0.1.0", version.ToString());
    }

    private void AssertVersion(int major, int minor, Version version)
    {
        Assert.Equal(major, version.Major);
        Assert.Equal(minor, version.Minor);
    }

    private void AssertVersion(int major, int minor, int build, Version version)
    {
        Assert.Equal(major, version.Major);
        Assert.Equal(minor, version.Minor);
        Assert.Equal(build, version.Build);
    }

    private void AssertVersion(int major, int minor, int build, int revision, Version version)
    {
        Assert.Equal(major, version.Major);
        Assert.Equal(minor, version.Minor);
        Assert.Equal(build, version.Build);
        Assert.Equal(revision, version.Revision);
    }
}