// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.IdentityModel.Tokens;

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Services;

public class FederationKeyMaterialService : IKeyMaterialService
{
	public FederationKeyMaterialService() { }

	public async Task<SigningCredentials> GetSigningCredentialsAsync(IEnumerable<string> allowedAlgorithms)
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<SigningCredentials>> GetSigningCredentialsAsync()
	{
		throw new NotImplementedException();
	}
}
