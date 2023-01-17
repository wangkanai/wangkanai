// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Entities;

public class IdentityClientGrantType : IdentityClientGrantType<Guid>
{
	public IdentityClientGrantType()
	{
		Id = Guid.NewGuid();
	}

	public IdentityClientGrantType(Guid clientId, GrantTypes grantType) : this()
	{
		ClientId  = clientId;
		GrantType = grantType;
	}
}

public class IdentityClientGrantType<TKey> where TKey : IEquatable<TKey>
{
	public IdentityClientGrantType() { }

	public IdentityClientGrantType(TKey clientId, GrantTypes grantType) : this()
	{
		ClientId  = clientId;
		GrantType = grantType;
	}

	public virtual TKey           Id        { get; set; } = default!;
	public virtual TKey           ClientId  { get; set; }
	public virtual IdentityClient Client    { get; set; }
	public virtual GrantTypes     GrantType { get; set; }
}