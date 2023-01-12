// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Identity;

public class IdentityTenant : IdentityTenant<string>
{
	public IdentityTenant()
	{
		Id = Guid.NewGuid().ToString();
	}

	public IdentityTenant(string tenantName) : this()
	{
		Name = tenantName;
	}
}

public class IdentityTenant<TKey> where TKey : IEquatable<TKey>
{
	public IdentityTenant() { }

	public IdentityTenant(string tenantName) : this()
	{
		Name = tenantName;
	}

	public virtual TKey    Id               { get; set; } = default!;
	public virtual string? Name             { get; set; }
	public virtual string? NormalizedName   { get; set; }
	public virtual string? ConcurrencyStamp { get; set; }

	public override string ToString()
		=> Name ?? string.Empty;
}