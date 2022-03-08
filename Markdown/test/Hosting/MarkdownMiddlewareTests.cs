using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Markdown.Hosting;
using Xunit;

namespace Wangkanai.Markdown.Test.Hosting
{
    public class MarkdownMiddlewareTests
    {
        private static Task Next(HttpContext context)
            => Task.Factory.StartNew(() => context);
        
        [Fact]
        public void If_Null_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new MarkdownMiddleware(null));
        }

        [Fact]
        public async Task If_Null_Invoke_Throw_Exception()
        {
            var middleware = new MarkdownMiddleware(Next);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await middleware.InvokeAsync((null)));
        }
    }
}