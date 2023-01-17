// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Entities;

public class IdentityClientOrigin : IdentityClientOrigin<Guid>
{
	public IdentityClientOrigin()
	{
		Id = Guid.NewGuid();
	}

	public IdentityClientOrigin(Guid clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}
}

public class IdentityClientOrigin<TKey> where TKey : IEquatable<TKey>
{
	public IdentityClientOrigin() { }

	public IdentityClientOrigin(TKey clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}

	public virtual TKey           Id       { get; set; }
	public virtual string         Origin   { get; set; }
	public virtual TKey           ClientId { get; set; }
	public virtual IdentityClient Client   { get; set; }
}