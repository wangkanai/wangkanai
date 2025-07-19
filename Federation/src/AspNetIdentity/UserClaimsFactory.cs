// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Federation.AspNetIdentity;

internal class UserClaimsFactory<TUser> : IUserClaimsPrincipalFactory<TUser>
	where TUser : class
{
	public async Task<ClaimsPrincipal> CreateAsync(TUser user)
	{
		var principal = new ClaimsPrincipal();

		return principal;
	}
}
