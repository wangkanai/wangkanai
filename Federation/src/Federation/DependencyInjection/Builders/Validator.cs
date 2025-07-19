// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Federation.Validations;

namespace Microsoft.Extensions.DependencyInjection;

public static class FederationValidatorBuilderExtensions
{
	public static IFederationBuilder AddResourceOwnerValidator<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(this IFederationBuilder builder)
		where T : class, IResourceOwnerPasswordValidator
	{
		builder.Services.AddTransient<IResourceOwnerPasswordValidator, T>();

		return builder;
	}
}
