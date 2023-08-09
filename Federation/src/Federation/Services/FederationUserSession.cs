// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Cryptography;
using Wangkanai.Federation.Extensions;
using Wangkanai.Hosting;

namespace Wangkanai.Federation.Services;

public class FederationUserSession : IUserSession
{
	internal readonly IHttpContextAccessor           HttpContextAccessor;
	internal readonly FederationOptions              Options;
	internal readonly IAuthenticationHandlerProvider HandlerProvider;
	internal readonly IClock                         Clock;
	internal readonly IServerUrls                    Urls;
	internal readonly ILogger                        Logger;

	internal ClaimsPrincipal          Principal;
	internal AuthenticationProperties Properties;

	internal HttpContext  HttpContext               => HttpContextAccessor.HttpContext;
	internal string       SessionCookieName         => Options.Authentication.SessionCookieName;
	internal string       SessionCookieDomain       => Options.Authentication.SessionCookieDomain;
	internal SameSiteMode SessionCookieSameSiteMode => Options.Authentication.SessionCookieSameSiteMode;

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
				properties.SetSessionId(HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Hex));
		}

		var sid = properties.GetSessionId();
		IssueSessionIdCookie(sid);

		Principal  = principal;
		Properties = properties;

		return sid!;
	}

	public virtual async Task<ClaimsPrincipal> GetUserAsync()
	{
		await AuthenticateAsync();

		return Principal;
	}

	public virtual async Task<string> GetSessionIdAsync()
	{
		await AuthenticateAsync();

		return Properties.GetSessionId()!;
	}

	public virtual async Task EnsureSessionIdCookieAsync()
	{
		var sid = await GetSessionIdAsync();
		if (sid != null)
			IssueSessionIdCookie(sid);
		else
			await RemoveSessionIdCookieAsync();
	}

	public virtual Task RemoveSessionIdCookieAsync()
	{
		if (HttpContext.Request.Cookies.ContainsKey(SessionCookieName))
		{
			var options = CreateSessionIdCookieOption();
			options.Expires = Clock.UtcNow.UtcDateTime.AddYears(-1);
			HttpContext.Response.Cookies.Append(SessionCookieName, ".", options);
		}

		return Task.CompletedTask;
	}

	public virtual async Task AddClientIdAsync(string clientId)
	{
		clientId.ThrowIfNull();

		await AuthenticateAsync();
		if (Properties != null)
		{
			var clientIds = Properties.GetClientList();
			if (!clientIds.Contains(clientId))
			{
				Properties.AddClientId(clientId);
				await UpdateSessionCookie();
			}
		}
	}

	public async Task<IEnumerable<string>> GetClientListAsync()
	{
		await AuthenticateAsync();

		if (Properties.TrueIfNull())
			return Enumerable.Empty<string>();

		try
		{
			return Properties.GetClientList();
		}
		catch (Exception exception)
		{
			Logger.LogError(exception, "Error decoding client list");
			Properties.RemoveClientList();
			await UpdateSessionCookie();
		}

		return Enumerable.Empty<string>();
	}

	public virtual void IssueSessionIdCookie(string sid)
	{
		if (!Options.Endpoints.EnableDiscoveryEndpoint)
			return;

		if (HttpContext.Request.Cookies[SessionCookieName] != sid)
			HttpContext.Response.Cookies.Append(
				Options.Authentication.SessionCookieName,
				sid,
				CreateSessionIdCookieOption());
	}

	public virtual CookieOptions CreateSessionIdCookieOption()
	{
		var secure = HttpContext.Request.IsHttps;
		var path   = Urls.BasePath.CleanUrlPath();

		return new CookieOptions
		{
			HttpOnly    = false,
			Secure      = secure,
			Path        = path,
			IsEssential = true,
			Domain      = SessionCookieDomain,
			SameSite    = SessionCookieSameSiteMode,
		};
	}

	private async Task UpdateSessionCookie()
	{
		await AuthenticateAsync();

		Principal.ThrowIfNull<InvalidOperationException>("User is not currently authenticated");
		Properties.ThrowIfNull<InvalidOperationException>("User is not currently authenticated");

		var scheme = await HttpContext.GetCookieAuthenticationSchemeAsync();
		await HttpContext.SignInAsync(scheme, Principal, Properties);
	}
}