// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class BrowserNotFoundExceptionTest
    {
        [Fact]
        public void ExceptionDefaultBehavior()
        {
            // arrange
            var exception = new BrowserNotFoundException();
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("Browser Not Supported", message);
        }

        [Fact]
        public void ExceptionNotNull()
        {
            // arrange
            var exception = new BrowserNotFoundException("test");
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("test", message);
        }

        [Fact]
        public void ExceptionInvalidBrowserName()
        {
            // arrange
            var exception = new BrowserNotFoundException("param", "watch", "test");
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("test\r\nParameter name: param\r\nwatch", message);
        }

        [Fact]
        public void ExceptionWithInnerException()
        {
            // arrange
            var inner = new Exception("inner");
            var exception = new BrowserNotFoundException("test", inner);
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("test", message);
        }
    }
}
