// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Wangkanai.Graph.Extensions;

namespace Wangkanai.Graph.Handlers;

public class GraphUserAccountFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    private readonly ILogger<GraphUserAccountFactory> logger;
    private readonly IServiceProvider                 serviceProvider;

    public GraphUserAccountFactory(
        IAccessTokenProviderAccessor     accessor,
        IServiceProvider                 serviceProvider,
        ILogger<GraphUserAccountFactory> logger)
        : base(accessor)
    {
        this.serviceProvider = serviceProvider;
        this.logger          = logger;
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);

        if (initialUser.Identity.IfNullThen())
            return initialUser;
        if (!initialUser.Identity.IsAuthenticated)
            return initialUser;

        var identity = initialUser.Identity as ClaimsIdentity;

        try
        {
            var graphClient = ActivatorUtilities.CreateInstance<GraphServiceClient>(serviceProvider);
            var user        = await graphClient.Me.Request().GetAsync();

            var photo = await graphClient.Me.Photos["48x48"].Content.Request().GetAsync();

            if (user != null)
            {
                identity.AddUserGraphInfo(user);
                identity.AddUserGraphPhoto(photo);
            }
        }
        catch (ServiceException ex)
        {
            logger.LogError($"Graph API service failure: {ex.Message}");
        }

        return initialUser;
    }
}