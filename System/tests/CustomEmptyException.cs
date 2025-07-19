// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

public class CustomEmptyException : ArgumentException
{
	public CustomEmptyException()
		: base() { }

	public CustomEmptyException(string message)
		: base(message) { }

	public CustomEmptyException(string message, Exception innerException)
		: base(message, innerException) { }

	public CustomEmptyException(string paramName, string message)
		: base(paramName, message) { }
}
