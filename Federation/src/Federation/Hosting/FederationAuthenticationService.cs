// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Identity;
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

	public async Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public async Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public async Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
	{
		throw new NotImplementedException();
	}

	public async Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	public async Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
	{
		throw new NotImplementedException();
	}

	private void AugmentPrincipal(ClaimsPrincipal principal)
	{
		_logger.LogDebug("Augmenting SignInContext");

		AssertRequiredClaims(principal);
		AugmentMissingClaims(principal, _clock.UtcNow.UtcDateTime);
	}

	private void AssertRequiredClaims(ClaimsPrincipal principal)
	{
		if (principal.Identities.Count() != 1)
			throw new InvalidOperationException("Only one identity is supported");
		if (principal.FindFirst(JwtClaimTypes.Subject) == null)
			throw new InvalidOperationException("Sub claim is missing");
	}

	private void AugmentMissingClaims(ClaimsPrincipal principal, DateTime now)
	{
		var identity = principal.Identities.First();

		if (identity.FindFirst(JwtClaimTypes.IdentityProvider) == null)
		{
			_logger.LogDebug("Adding idp claim with value: {idp}", FederationConstants.LocalIdentityProvider);
			identity.AddClaim(new Claim(JwtClaimTypes.IdentityProvider, FederationConstants.LocalIdentityProvider));
		}

		if (identity.FindFirst(JwtClaimTypes.AuthenticationMethod) == null)
		{
			if (identity.FindFirst(JwtClaimTypes.IdentityProvider).Value == FederationConstants.LocalIdentityProvider)
			{
				_logger.LogDebug("Adding amr claim with value: {amr}", OidcConstants.AuthenticateMethods.Password);
				identity.AddClaim(new Claim(JwtClaimTypes.AuthenticationMethod, OidcConstants.AuthenticateMethods.Password));
			}
			else
			{
				_logger.LogDebug("Adding amr claim with value: {amr}", FederationConstants.ExternalAuthenticationMethod);
				identity.AddClaim(new Claim(JwtClaimTypes.AuthenticationMethod, FederationConstants.ExternalAuthenticationMethod));
			}
		}

		if (identity.FindFirst(JwtClaimTypes.AuthenticationTime) == null)
		{
			var time = new DateTimeOffset(now).ToUnixTimeSeconds().ToString();

			_logger.LogDebug("Adding auth_time claim with value: {time}", time);
			identity.AddClaim(new Claim(JwtClaimTypes.AuthenticationTime, time, ClaimValueTypes.Integer64));
		}
	}
}