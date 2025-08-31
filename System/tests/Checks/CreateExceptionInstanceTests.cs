// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Checks;

public class CreateExceptionInstanceTests
{
   [Fact]
   public void CreateExceptionInstance()
   {
      const int value     = 1;
      var       exception = ExceptionActivator.CreateGenericInstance<ArgumentNullException>(nameof(value));
      Assert.Equal(1,                             value);
      Assert.Equal(typeof(ArgumentNullException), exception.GetType());
   }

   [Fact]
   public void CreateExceptionInstanceWithMessage()
   {
      const int value     = 1;
      var       exception = ExceptionActivator.CreateGenericInstance<ArgumentNullException>(nameof(value), "message");
      Assert.Equal(1,                             value);
      Assert.Equal(typeof(ArgumentNullException), exception.GetType());
      Assert.Equal("message (Parameter 'value')", exception.Message);
   }
}