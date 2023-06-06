// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public class CustomNullException : ArgumentNullException
{
	public CustomNullException()
		: base() { }

	public CustomNullException(string message)
		: base(message) { }

	public CustomNullException(string message, Exception innerException)
		: base(message, innerException) { }

	public CustomNullException(string paramName, string message)
		: base(paramName, message) { }


	public static CustomNullException CreateInstance()
		=> new CustomNullException();

	public static CustomNullException CreateInstance(string message)
		=> new CustomNullException(message);

	public static CustomNullException CreateInstance(string message, Exception innerException)
		=> new CustomNullException(message, innerException);

	public static CustomNullException CreateInstance(string paramName, string message)
		=> new CustomNullException(paramName, message);
}