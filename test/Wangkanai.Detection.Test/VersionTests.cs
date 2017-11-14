using System;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class VersionTests
    {
        [Fact]
        public void Ctor_StringString_Success()
        {
            string major = "1";
            string minor = "0";
            var version = new Version(major, minor);

            AssertVersion(major, minor, null, null, version);
        }

        [Theory]//(Skip = "It might be good to have null check?")]
        [InlineData(null, "0")]
        [InlineData("9", null)]
        public void Ctor_Null_ThrowsArgumentNullException(string major, string minor)
        {
            Assert.Throws<ArgumentNullException>(() => new Version(major, minor));
        }

        [Theory(Skip = "It might be good to have empty string check?")]
        [InlineData("", "0")]
        [InlineData("9", "")]
        public void Ctor_EmptyString_ThrowsArgumentNullException(string major, string minor)
        {
            Assert.Throws<ArgumentNullException>(() => new Version(major, minor));
        }

        [Theory]
        [InlineData("9", "0", null, null)]
        [InlineData("9", "0", "12", null)]
        [InlineData("9", "0", null, "35")]
        [InlineData("9", "0", "", "")]
        [InlineData("9", "0", "12", "")]
        [InlineData("9", "0", "", "35")]
        [InlineData("9", "0", "12", "34")]
        public void Ctor_StringStringStringString_Success(string major, string minor, string patch, string build)
        {
            var version = new Version(major, minor, patch, build);

            AssertVersion(major, minor, patch, build, version);
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
