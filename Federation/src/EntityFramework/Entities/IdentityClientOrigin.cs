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

public class IdentityClientOrigin<TKey>
	where TKey : IEquatable<TKey>
{
	public IdentityClientOrigin() { }

	public IdentityClientOrigin(TKey clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}

	public Guid           Id       { get; set; }
	public string         Origin   { get; set; }
	public TKey           ClientId { get; set; }
	public IdentityClient Client   { get; set; }
}