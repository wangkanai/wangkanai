// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Validations;

namespace Microsoft.Extensions.DependencyInjection;

public static class ValidatorBuilderExtensions
{
    public static IFederationBuilder AddResourceOwnerValidator<T>(this IFederationBuilder builder)
        where T : class, IResourceOwnerPasswordValidator
    {
        builder.Services.AddTransient<IResourceOwnerPasswordValidator, T>();

        return builder;
    }
}