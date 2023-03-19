// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Webserver.Services;

namespace Wangkanai.Federation.Hosting;

public sealed class FederationAuthenticationService : IAuthenticationService
{
	private readonly IAuthenticationService                   _inner;
	private readonly IAuthenticationSchemeProvider            _schemes;
	private readonly ISystemClock                             _clock;
	private readonly IUserSession                             _session;
	private readonly ILogger<FederationAuthenticationService> _logger;

	public FederationAuthenticationService(
		Decorator<IAuthenticationService>        decorator,
		IAuthenticationSchemeProvider            schemes,
		ISystemClock                             clock,
		IUserSession                             session,
		ILogger<FederationAuthenticationService> logger)
	{
		_inner   = decorator.Instance;
		_clock   = clock;
		_session = session;
		_logger  = logger;
		_schemes = schemes;
	}

	public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
	{
		throw new NotImplementedException();
	}

	public Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}
}