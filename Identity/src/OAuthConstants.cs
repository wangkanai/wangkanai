// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

public static class OAuthConstants
{
	public static class AuthorizeRequest
	{
		public const string ResponseType = "response_type";
	}

	public static class AuthorizeResponse
	{
		public const string Code = "code";
	}

	public static class AuthorizeErrors
	{
		public const string AccessDenied = "access_denied";
	}
}
