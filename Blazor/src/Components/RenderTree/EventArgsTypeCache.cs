// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;
using System.Reflection;

namespace Wangkanai.Blazor.Components.RenderTree;

internal static class EventArgsTypeCache
{
	private static readonly ConcurrentDictionary<MethodInfo, Type> Cache = new();

	public static Type GetEventArgsType(MethodInfo methodInfo)
	{
		return Cache.GetOrAdd(methodInfo, methodInfo => {
			var parameterInfos = methodInfo.GetParameters();
			if (parameterInfos.Length == 0) return typeof(EventArgs);

			if (parameterInfos.Length > 1) throw new InvalidOperationException($"The method {methodInfo} cannot be used as an event handler because it declares more than one parameter.");

			var declaredType = parameterInfos[0].ParameterType;
			if (typeof(EventArgs).IsAssignableFrom(declaredType))
				return declaredType;
			throw new InvalidOperationException($"The event handler parameter type {declaredType.FullName} for event must inherit from {typeof(EventArgs).FullName}.");
		});
	}
}