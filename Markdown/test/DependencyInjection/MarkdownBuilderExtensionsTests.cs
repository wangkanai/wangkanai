using System;
using Xunit;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MarkdownBuilderExtensionsTests
    {
        [Fact]
        public void AddMarkdown_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection) null).AddMarkdown());
        }
        
        [Fact]
        public void AddMarkdownBuilder_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => ((IServiceCollection) null).AddMarkdownBuilder());
        }
    }
}