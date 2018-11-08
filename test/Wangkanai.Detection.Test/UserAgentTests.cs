// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Xunit;

namespace Wangkanai.Detection.Test
{
    public class UserAgentTests
    {
        [Fact]
        public void Ctor_Default_Success()
        {
            var userAgent = new UserAgent();

            Assert.Null(userAgent.ToString());
        }

        [Fact]
        public void Ctor_String_Success()
        {
            var name = "Agent";
            var userAgent = new UserAgent(name);

            Assert.Equal(name, userAgent.ToString());
        }

        [Fact]
        public void Ctor_Null_Success()
        {
            var userAgent = new UserAgent(null);

            Assert.NotNull(userAgent.ToString());
        }
    }
}
