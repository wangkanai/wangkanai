// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

using Wangkanai.Detection.Extensions;

namespace Wangkanai.Detection.Hosting;

internal class ResponsivePageMatcherPolicy : MatcherPolicy, IEndpointComparerPolicy, IEndpointSelectorPolicy
{
    public ResponsivePageMatcherPolicy() => Comparer = EndpointMetadataComparer<IResponsiveMetadata>.Default;

    public IComparer<Endpoint> Comparer { get; }

    public override int Order => 10000;

    public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
    {
        foreach (var endpoint in endpoints)
            if (endpoint?.Metadata.GetMetadata<IResponsiveMetadata>() != null)
                return true;

        return false;
    }

    public Task ApplyAsync(HttpContext httpContext, CandidateSet candidates)
    {
        var device = httpContext.GetDevice();

        for (var i = 0; i < candidates.Count; i++)
        {
            var endpoint = candidates[i].Endpoint;
            var metadata = endpoint.Metadata.GetMetadata<IResponsiveMetadata>();
            if (metadata?.Device != null && device != metadata.Device)
            {
                // This endpoint is not a match for the selected device.
                candidates.SetValidity(i, false);
            }
        }

        return Task.CompletedTask;
    }
}