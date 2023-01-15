// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class IdentityClientCorsOrigin : IdentityClientCorsOrigin<string>
{
	public IdentityClientCorsOrigin()
	{
		Id = Guid.NewGuid();
	}

	public IdentityClientCorsOrigin(string clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}
}

public class IdentityClientCorsOrigin<TKey>
	where TKey : IEquatable<TKey>
{
	public IdentityClientCorsOrigin() { }

	public IdentityClientCorsOrigin(TKey clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}

	public Guid           Id       { get; set; }
	public string         Origin   { get; set; }
	public TKey           ClientId { get; set; }
	public IdentityClient Client   { get; set; }
}