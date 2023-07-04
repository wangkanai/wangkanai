// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;

namespace Wangkanai.Federation.Services;

public sealed class FederationUserSession : IUserSession
{
	public async Task<string> CreateSessionIdAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
	{
		throw new NotImplementedException();
	}

	public async Task<ClaimsPrincipal> GetUserAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<string> GetSessionIdAsync()
	{
		throw new NotImplementedException();
	}

	public async Task EnsureSessionIdCookieAsync()
	{
		throw new NotImplementedException();
	}

	public async Task RemoveSessionIdCookieAsync()
	{
		throw new NotImplementedException();
	}

	public async Task AddClientIdAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<IEnumerable<string>> GetClientListAsync()
	{
		throw new NotImplementedException();
	}
}