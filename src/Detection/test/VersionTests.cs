// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Xunit;

namespace Wangkanai.Detection.Test
{
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

        //[Theory]
        //[InlineData(null, "0")]
        //[InlineData("9", null)]
        //public void Ctor_Null_ThrowsArgumentNullException(string value1, string value2)
        //{
        //    Assert.Throws<ArgumentNullException>(() => new Version(int.Parse(value1), int.Parse(value2)));
        //    Assert.Throws<ArgumentNullException>(() => new Version(1, 0, int.Parse(value1), int.Parse(value2)));
        //}

        //[Theory]
        //[InlineData("", "0")]
        //[InlineData("9", "")]
        //public void Ctor_EmptyString_ThrowsArgumentNullException(string value1, string value2)
        //{
        //    Assert.Throws<ArgumentNullException>(() => new Version(int.Parse(value1), int.Parse(value2)));
        //    Assert.Throws<ArgumentNullException>(() => new Version(1, 0, int.Parse(value1), int.Parse(value2)));
        //}

        [Fact]
        public void Ctor_StringStringStringString_Success()
        {
            int major = 1;
            int minor = 0;
            int build = 1;
            int revision = 0;
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
}
