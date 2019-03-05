using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Wangkanai.Detection.Test
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
