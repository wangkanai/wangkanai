// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class IdentityClientCorsOrigin : IdentityClientCorsOrigin<string, string>
{
	public IdentityClientCorsOrigin()
	{
		Id = Guid.NewGuid().ToString();
	}

	public IdentityClientCorsOrigin(string clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}
}

public class IdentityClientCorsOrigin<TKey, TKeyClient>
	where TKey : IEquatable<TKey>
	where TKeyClient : IEquatable<TKeyClient>
{
	public IdentityClientCorsOrigin() { }

	public IdentityClientCorsOrigin(string origin) : this()
	{
		Origin   = origin;
	}

	public TKey           Id       { get; set; }
	public string         Origin   { get; set; }
	public TKeyClient     ClientId { get; set; }
	public IdentityClient Client   { get; set; }
}