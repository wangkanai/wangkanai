// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

public class CustomArgumentException : ArgumentException
{
   public CustomArgumentException() { }

   public CustomArgumentException(string message)
      : base(message) { }

   public CustomArgumentException(string message, Exception innerException)
      : base(message, innerException) { }

   public CustomArgumentException(string paramName, string message)
      : base(paramName, message) { }


   public static CustomArgumentException CreateInstance() => new();

   public static CustomArgumentException CreateInstance(string message) => new(message);

   public static CustomArgumentException CreateInstance(string message, Exception innerException) => new(message, innerException);

   public static CustomArgumentException CreateInstance(string paramName, string message) => new(paramName, message);
}