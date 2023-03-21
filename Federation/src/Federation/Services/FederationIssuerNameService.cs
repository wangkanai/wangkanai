// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Services;

public class FederationIssuerNameService : IIssuerNameService
{
	private readonly FederationOptions _options;

	public FederationIssuerNameService(FederationOptions options)
	{
		_options = options;
	}

	public Task<string> GetCurrentAsync()
	{
		var issuer = _options.Issuer.Name;
		
		return Task.FromResult(issuer);
	}
}