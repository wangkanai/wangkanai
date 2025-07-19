// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.IdentityModel.Tokens;

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Services;

public interface IKeyMaterialService
{
	Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync();

	Task<SigningCredentials> GetSigningCredentialsAsync(IEnumerable<string> allowedAlgorithms);

	Task<IEnumerable<SigningCredentials>> GetSigningCredentialsAsync();
}
