// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
