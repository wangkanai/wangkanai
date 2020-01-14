// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Collections
{
    public class EdgeTests
    {
        [Fact]
        public void First()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393";
            // act
            var edge = new Edge(agent);
            // assert
            Assert.Equal(Browser.Edge, edge.Type);
            Assert.Equal("14.14393", edge.Version.ToString());
        }

        [Fact]
        public void Invalid()
        {
            // arrange
            var agent = "Mozilla/5.0 (Windows NT x.y; rv:10.0) Gecko/20100101 Firefox/10.0";
            // act
            var edge = new Edge(agent);
            // assert
            Assert.NotEqual(Browser.Edge, edge.Type);
            Assert.Null(edge.Version);
        }
    }
}
