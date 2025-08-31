// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentMoreThanException : ArgumentException
{
   private ArgumentMoreThanException(SerializationInfo info, StreamingContext context) { }
   public ArgumentMoreThanException() : base(SystemResources.ArgumentMoreThanGeneric) { }
   public ArgumentMoreThanException(string paramName) : base(SystemResources.ArgumentMoreThanGeneric, paramName) { }
   public ArgumentMoreThanException(string message,   Exception innerException) : base(message, innerException) { }
   public ArgumentMoreThanException(string paramName, string    message) : base(message, paramName) { }
}