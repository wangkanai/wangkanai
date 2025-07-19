// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Services;

public class FederationServerUrls : IServerUrls
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public FederationServerUrls(IHttpContextAccessor httpContextAccessor)
		=> _httpContextAccessor = httpContextAccessor;

	public string Origin
	{
		get
		{
			var request = _httpContextAccessor.HttpContext.Request;
			return request.Scheme + "://" + request.Host.ToUriComponent();
		}
		set
		{
			var split = value.Split(new[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
			var request = _httpContextAccessor.HttpContext.Request;
			request.Scheme = split.First();
			request.Host = new HostString(split.Last());
		}
	}

	public string BasePath
	{
		get => _httpContextAccessor.HttpContext.Items[Constants.EnvironmentKeys.BasePath] as string;
		set => _httpContextAccessor.HttpContext.Items[Constants.EnvironmentKeys.BasePath] = value.RemoveTrailingSlash();
	}
}
