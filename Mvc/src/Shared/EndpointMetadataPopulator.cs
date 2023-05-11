// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Metadata;

using Wangkanai.System.Extensions.Internal;

namespace Wangkanai.Mvc.Shared;

[UnconditionalSuppressMessage("Trimmer", "IL2060", Justification = "Trimmer warnings are presented in RequestDelegateFactory.")]
internal static class EndpointMetadataPopulator
{
	private static readonly MethodInfo PopulateMetadataForParameterMethod = typeof(EndpointMetadataPopulator).GetMethod(nameof(PopulateMetadataForParameter), BindingFlags.NonPublic | BindingFlags.Static)!;
	private static readonly MethodInfo PopulateMetadataForEndpointMethod  = typeof(EndpointMetadataPopulator).GetMethod(nameof(PopulateMetadataForEndpoint), BindingFlags.NonPublic  | BindingFlags.Static)!;

	public static void PopulateMetadata(MethodInfo methodInfo, EndpointBuilder builder, IEnumerable<ParameterInfo>? parameters = null)
	{
		object?[]? invokeArgs = null;
		parameters ??= methodInfo.GetParameters();

		// Get metadata from parameter types
		foreach (var parameter in parameters)
		{
			if (typeof(IEndpointParameterMetadataProvider).IsAssignableFrom(parameter.ParameterType))
			{
				// Parameter type implements IEndpointParameterMetadataProvider
				invokeArgs    ??= new object[2];
				invokeArgs[0] =   parameter;
				invokeArgs[1] =   builder;
				PopulateMetadataForParameterMethod.MakeGenericMethod(parameter.ParameterType).Invoke(null, invokeArgs);
			}

			if (typeof(IEndpointMetadataProvider).IsAssignableFrom(parameter.ParameterType))
			{
				// Parameter type implements IEndpointMetadataProvider
				invokeArgs    ??= new object[2];
				invokeArgs[0] =   methodInfo;
				invokeArgs[1] =   builder;
				PopulateMetadataForEndpointMethod.MakeGenericMethod(parameter.ParameterType).Invoke(null, invokeArgs);
			}
		}

		// Get metadata from return type
		var returnType = methodInfo.ReturnType;
		if (AwaitableInfo.IsTypeAwaitable(returnType, out var awaitableInfo))
			returnType = awaitableInfo.ResultType;

		if (returnType is not null && typeof(IEndpointMetadataProvider).IsAssignableFrom(returnType))
		{
			// Return type implements IEndpointMetadataProvider
			invokeArgs    ??= new object[2];
			invokeArgs[0] =   methodInfo;
			invokeArgs[1] =   builder;
			PopulateMetadataForEndpointMethod.MakeGenericMethod(returnType).Invoke(null, invokeArgs);
		}
	}

	private static void PopulateMetadataForParameter<T>(ParameterInfo parameter, EndpointBuilder builder)
		where T : IEndpointParameterMetadataProvider
		=> T.PopulateMetadata(parameter, builder);

	private static void PopulateMetadataForEndpoint<T>(MethodInfo method, EndpointBuilder builder)
		where T : IEndpointMetadataProvider
		=> T.PopulateMetadata(method, builder);
}