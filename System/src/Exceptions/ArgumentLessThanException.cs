// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentLessThanException : ArgumentException
{
   private ArgumentLessThanException(SerializationInfo info, StreamingContext context) { }
   public ArgumentLessThanException() : base(SystemResources.ArgumentLessThanGeneric) { }
   public ArgumentLessThanException(string paramName) : base(SystemResources.ArgumentLessThanGeneric, paramName) { }
   public ArgumentLessThanException(string message,   Exception innerException) : base(message, innerException) { }
   public ArgumentLessThanException(string paramName, string    message) : base(message, paramName) { }
}