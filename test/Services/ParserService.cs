using System;

using Xunit;

namespace Wangkanai.Detection.Services
{
    public class ParserService
    {
        [Fact]
        public void Ctor_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ParserService(null));
        }
    }
}