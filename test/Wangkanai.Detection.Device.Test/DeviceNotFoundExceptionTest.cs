using Xunit;

namespace Wangkanai.Detection.Test
{
    public class DeviceNotFoundExceptionTest
    {
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
    }
}