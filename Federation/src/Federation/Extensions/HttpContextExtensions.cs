// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation.Extensions;

public static class HttpContextExtensions
{
	internal static void SetSignOutCalled(this HttpContext context)
	{
		context.ThrowIfNull();
		context.Items[Constants.EnvironmentKeys.SignOutCalled] = "true";
	} 
}