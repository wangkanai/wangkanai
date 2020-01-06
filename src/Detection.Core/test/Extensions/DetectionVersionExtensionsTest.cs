// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Extensions
{
    public class DetectionVersionExtensionsTest
    {
        [Fact]
        public void RemoveWritespaceTest()
        {
            // arrange
            var temp = "1.0.0 ";
            // act
            var version = temp.ToVersion();

            var result = version.ToString();
            // asset
            Assert.Equal("1.0.0", result);
        }
    }
}
