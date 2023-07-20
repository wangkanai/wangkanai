// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Cryptography;
using Wangkanai.Federation.Extensions;

namespace Wangkanai.Federation.Services;

public class FederationUserSession : IUserSession
{
	internal readonly IHttpContextAccessor           HttpContextAccessor;
	internal readonly FederationOptions              Options;
	internal readonly IAuthenticationHandlerProvider HandlerProvider;
	internal readonly IClock                         Clock;
	internal readonly ILogger                        Logger;

	internal ClaimsPrincipal          Principal;
	internal AuthenticationProperties Properties;

	internal HttpContext HttpContext => HttpContextAccessor.HttpContext;

	protected virtual async Task AuthenticateAsync()
	{
		if (Principal == null || Properties == null)
		{
			var scheme  = await HttpContext.GetCookieAuthenticationSchemeAsync();
			var handler = await HandlerProvider.GetHandlerAsync(HttpContext, scheme);
			handler.ThrowIfNull<InvalidOperationException>($"No authentication handler is configured to authenticate for the scheme: {scheme}");
			var result = await handler.AuthenticateAsync();
			if (result != null && result.Succeeded && result.Principal.Identity.IsAuthenticated)
			{
				Principal  = result.Principal;
				Properties = result.Properties;
			}
		}
	}

	public virtual async Task<string> CreateSessionIdAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
	{
		principal.ThrowIfNull();
		properties.ThrowIfNull();

		var currentSubjectId = (await GetUserAsync()).GetSubjectId();
		var newSubjectId     = principal.GetSubjectId();
		if (properties.GetSessionId() == null)
		{
			var currentSid = await GetSessionIdAsync();
			if (newSubjectId == currentSubjectId && currentSid != null)
			{
				properties.SetSessionId(currentSid);
				var clients = Properties.GetClientList();
				if (clients.Any())
					properties.SetClientList(clients);
			}
			else
			{
				properties.SetSessionId(HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Hexadecimal));
			}
		}

		var sid = properties.GetSessionId();

		Principal  = principal;
		Properties = properties;

		return sid!;
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