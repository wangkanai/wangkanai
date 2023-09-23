// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using icrosoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

namespace Wangkanai.Security.ApplicationModels;

public sealed class PrivateNetworkApplicationModelProvider : IApplicationModelProvider
{
    private readonly IAuthorizationPolicyProvider _policyProvider;
    private readonly MvcOptions                   _mvcOptions;
    private readonly SecurityOptions              _securityOptions;

    public int Order => -1000 + 10;

    public PrivateNetworkApplicationModelProvider(
        IAuthorizationPolicyProvider policyProvider,
        IOptions<MvcOptions>         mvcOptions,
        IOptions<SecurityOptions>    securityOptions)
    {
        _policyProvider  = policyProvider;
        _mvcOptions      = mvcOptions.Value;
        _securityOptions = securityOptions.Value;
    }

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
        // Intentionally empty
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        context.ThrowIfNull();

        if (_mvcOptions.EnableEndpointRouting)
            return;

        foreach (var controllerModel in context.Result.Controllers)
        {
            var controllerModelAuthData = controllerModel.Attributes.OfType<IAuthorizeData>().ToArray();
            if (controllerModelAuthData.Length > 0)
                controllerModel.Filters.Add(GetFilter(_policyProvider, controllerModelAuthData));

            foreach (var _ in controllerModel.Attributes.OfType<IAllowPrivateNetwork>())
            {
                controllerModel.Filters.Add(new AllowPrivateNetworkFilter());
            }

            foreach (var actionModel in controllerModel.Actions)
            {
                var actionModelAuthData = actionModel.Attributes.OfType<IAuthorizeData>().ToArray();
                if (actionModelAuthData.Length > 0)
                    actionModel.Filters.Add(GetFilter(_policyProvider, actionModelAuthData));

                foreach (var _ in actionModel.Attributes.OfType<IAllowPrivateNetwork>())
                    actionModel.Filters.Add(new AllowPrivateNetworkFilter());
            }
        }
    }

    // The default policy provider will make the same policy for given input, so make it only once.
    // This will always execute synchronously.
    public static AuthorizeFilter GetFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authData)
        => policyProvider.GetType() == typeof(DefaultAuthorizationPolicyProvider)
               ? new AuthorizeFilter(AuthorizationPolicy.CombineAsync(policyProvider, authData).GetAwaiter().GetResult()!)
               : new AuthorizeFilter(policyProvider, authData);
}