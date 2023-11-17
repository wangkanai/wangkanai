// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

namespace Wangkanai;

internal static class ExceptionActivator
{
	internal static T CreateGenericInstance<T>([InvokerParameterName] string paramName)
		where T : Exception
	{
		return (Activator.CreateInstance(typeof(T), paramName) as T)!;
	}

	internal static T CreateGenericInstance<T>([InvokerParameterName] string paramName, string message)
		where T : Exception
	{
		return (Activator.CreateInstance(typeof(T), paramName, message) as T)!;
	}

	internal static T CreateArgumentInstance<T>([InvokerParameterName] string paramName)
		where T : ArgumentException
	{
		return (Activator.CreateInstance(typeof(T), paramName) as T)!;
	}

	internal static T CreateArgumentInstance<T>([InvokerParameterName] string paramName, string message)
		where T : ArgumentException
	{
		return (Activator.CreateInstance(typeof(T), paramName, message) as T)!;
	}
}
