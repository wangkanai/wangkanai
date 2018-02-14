// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class VersionTests
    {
        [Theory]
        [InlineData("1")]
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
            string major = "1";
            string minor = "0";
            var version = new Version(major, minor);

            AssertVersion(major, minor, null, null, version);
            Assert.Equal("1.0", version.ToString());
        }

        [Theory]
        [InlineData(null, "0")]
        [InlineData("9", null)]
        public void Ctor_Null_ThrowsArgumentNullException(string value1, string value2)
        {
            Assert.Throws<ArgumentNullException>(() => new Version(value1, value2));
            Assert.Throws<ArgumentNullException>(() => new Version("1", "0", value1, value2));
        }

        [Theory]
        [InlineData("", "0")]
        [InlineData("9", "")]
        public void Ctor_EmptyString_ThrowsArgumentNullException(string value1, string value2)
        {
            Assert.Throws<ArgumentNullException>(() => new Version(value1, value2));
            Assert.Throws<ArgumentNullException>(() => new Version("1", "0", value1, value2));
        }

        [Fact]
        public void Ctor_StringStringStringString_Success()
        {
            string major = "1";
            string minor = "0";
            string patch = "1";
            string build = "0";
            var version = new Version(major, minor, patch, build);

            AssertVersion(major, minor, patch, build, version);
            Assert.Equal("1.0.1.0", version.ToString());
        }

        private void AssertVersion(string major, string minor, string patch, string build, Version version)
        {
            Assert.Equal(major, version.Major);
            Assert.Equal(minor, version.Minor);
            Assert.Equal(patch, version.Patch);
            Assert.Equal(build, version.Build);
        }
    }
}
