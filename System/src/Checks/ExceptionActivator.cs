// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

internal static class ExceptionActivator
{
	internal static T CreateGenericInstance<T>(string paramName)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), paramName) as T)!;

	internal static T CreateGenericInstance<T>(string paramName, string message)
		where T : Exception
		=> (Activator.CreateInstance(typeof(T), paramName, message) as T)!;

	internal static T CreateArgumentInstance<T>(string paramName)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), paramName) as T)!;

	internal static T CreateArgumentInstance<T>(string paramName, string message)
		where T : ArgumentException
		=> (Activator.CreateInstance(typeof(T), paramName, message) as T)!;
}
