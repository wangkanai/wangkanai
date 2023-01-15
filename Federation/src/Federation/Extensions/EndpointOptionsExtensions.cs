// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http.Extensions;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Extensions;

public static class EndpointOptionsExtensions
{
	public static bool IsEnabled(this EndpointsOptions options, Endpoint endpoint)
	{
		if (endpoint.Name == Constants.EndpointNames.Discovery)
			return options.EnableDiscoveryEndpoint;
		
		return true;
	}
}