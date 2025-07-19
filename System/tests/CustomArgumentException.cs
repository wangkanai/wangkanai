// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

public class CustomArgumentException : ArgumentException
{
	public CustomArgumentException()
		: base() { }

	public CustomArgumentException(string message)
		: base(message) { }

	public CustomArgumentException(string message, Exception innerException)
		: base(message, innerException) { }

	public CustomArgumentException(string paramName, string message)
		: base(paramName, message) { }


	public static CustomArgumentException CreateInstance()
		=> new CustomArgumentException();

	public static CustomArgumentException CreateInstance(string message)
		=> new CustomArgumentException(message);

	public static CustomArgumentException CreateInstance(string message, Exception innerException)
		=> new CustomArgumentException(message, innerException);

	public static CustomArgumentException CreateInstance(string paramName, string message)
		=> new CustomArgumentException(paramName, message);
}
