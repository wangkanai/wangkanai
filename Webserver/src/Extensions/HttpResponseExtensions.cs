// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq;

using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpResponseExtensions
{
	public static void SetCache(this HttpResponse response, int maxAge, params string[] varys)
	{
		if (maxAge == 0)
			SetNoCache(response);
		else if (maxAge > 0)
		{
			if (!response.Headers.ContainsKey(WebserverConstants.CacheControl.ControlKey))
				response.Headers.Append(WebserverConstants.CacheControl.ControlKey, WebserverConstants.CacheControl.ControlMaxAge(maxAge));

			if (varys?.Any() != true)
				return;

			var vary = varys.Aggregate((x, y) => x + "," + y);
			if (response.Headers.TryGetValue(WebserverConstants.Vary.Key, out var header))
				vary = header + "," + vary;

			response.Headers[WebserverConstants.Vary.Key] = vary;
		}
	}

	public static void SetNoCache(this HttpResponse response)
	{
		if (!response.Headers.ContainsKey(WebserverConstants.CacheControl.ControlKey))
			response.Headers.Append(WebserverConstants.CacheControl.ControlKey, WebserverConstants.CacheControl.ControlValue);
		else
			response.Headers[WebserverConstants.CacheControl.ControlKey] = WebserverConstants.CacheControl.ControlValue;

		if (!response.Headers.ContainsKey(WebserverConstants.CacheControl.PragmaKey))
			response.Headers.Append(WebserverConstants.CacheControl.PragmaKey, WebserverConstants.CacheControl.PragmaValue);
	}
}
