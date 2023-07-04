// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;

namespace Wangkanai.Federation.Services;

public interface IUserSession
{
	Task<string>          CreateSessionIdAsync(ClaimsPrincipal principal, AuthenticationProperties properties);
	Task<ClaimsPrincipal> GetUserAsync();
	Task<string>          GetSessionIdAsync();

	Task EnsureSessionIdCookieAsync();
	Task RemoveSessionIdCookieAsync();
	Task AddClientIdAsync();

	Task<IEnumerable<string>> GetClientListAsync();
}