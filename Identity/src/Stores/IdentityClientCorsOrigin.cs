// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Identity;

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

public class IdentityClientCorsOrigin<TKey, TClient>
	where TKey : IEquatable<TKey>
	where TClient : IEquatable<TClient>
{
	public IdentityClientCorsOrigin() { }

	public IdentityClientCorsOrigin(TClient clientId, string origin) : this()
	{
		ClientId = clientId;
		Origin   = origin;
	}

	public TKey                    Id       { get; set; }
	public string                  Origin   { get; set; }
	public TClient                 ClientId { get; set; }
	public IdentityClient<TClient> Client   { get; set; }
}