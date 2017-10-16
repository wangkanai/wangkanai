// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceNotFoundExceptionTest
    {
        [Fact]
        public void ExceptionDefaultBehavuor()
        {
            // arrange
            var exception = new DeviceNotFoundException();
            // act 
            var message = exception.Message;
            // assert
            Assert.Equal("Device Not Supported", message);
        }

        [Fact]
        public void ExceptionNotNull()
        {
            // arrange 
            var exception = new DeviceNotFoundException("test");
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("test", message);
        }

        [Fact]
        public void ExceptionInvalidDeviceName()
        {
            // arrange 
            var exception = new DeviceNotFoundException("param", "watch", "test");
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
            var exception = new DeviceNotFoundException("test", inner);
            // act
            var message = exception.Message;
            // assert
            Assert.Equal("test",message);
        }
    }
}