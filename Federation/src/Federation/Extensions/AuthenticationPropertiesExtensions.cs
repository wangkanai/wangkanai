// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Authentication;

namespace Wangkanai.Federation.Extensions;

public static class AuthenticationPropertiesExtensions
{
	internal const string SessionIdKey  = "session_id";
	internal const string ClientListKey = "client_list";

	public static string? GetSessionId(this AuthenticationProperties properties)
		=> properties.Items.ContainsKey(SessionIdKey)
			   ? properties.Items[SessionIdKey]
			   : null;

	public static void SetSessionId(this AuthenticationProperties properties, string sid)
		=> properties.Items[SessionIdKey] = sid;

public static IEnumerable<string> GetClientList(this AuthenticationProperties properties)
{
	if (properties?.Items.ContainsKey(ClientListKey) == true)
	{
		var value = properties.Items[ClientListKey];
		return DecodeList(value);
	}

	return Enumerable.Empty<string>();
}

private static IEnumerable<string> DecodeList(string value)
{
	var bytes = Base64Url.Decode(value);
	var list  = Encoding.UTF8.GetString(bytes);
	return list.Split(',');
}