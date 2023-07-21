// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

using Wangkanai.Identity;

namespace Wangkanai.Federation.Extensions;

[DebuggerStepThrough]
public static class PrincipalExtensions
{
	public static string GetSubjectId(this IPrincipal principal)
		=> principal.Identity.GetSubjectId();

	public static string GetSubjectId(this IIdentity identity)
	{
		var id    = identity as ClaimsIdentity;
		var claim = id.FindFirst(JwtClaimTypes.Subject);
		claim.ThrowIfNull<InvalidOperationException>("sub claim is missing");
		return claim.Value;
	}
}