// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.IdentityModel.Tokens;

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Services;

public interface IKeyMaterialService
{
	Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync();

	Task<SigningCredentials> GetSigningCredentialsAsync(IEnumerable<string> allowedAlgorithms);

	Task<IEnumerable<SigningCredentials>> GetSigningCredentialsAsync();
}
