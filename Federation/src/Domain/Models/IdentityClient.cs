// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Federation.Models;

public class IdentityClient : IdentityClient<string>
{
	public IdentityClient()
	{
		Id = Guid.NewGuid().ToString();
	}

	public IdentityClient(string clientId) : this()
	{
		ClientId = clientId;
	}
}

public class IdentityClient<TKey> : IAuditable
	where TKey : IEquatable<TKey>
{
	public IdentityClient() { }

	public IdentityClient(string clientId) : this()
	{
		ClientId = clientId;
	}

	public virtual TKey                           Id               { get; set; } = default!;
	public virtual string                         ClientId         { get; set; }
	public virtual string                         ProtocolType     { get; set; } = FederationConstants.ProtocolTypes.OpenIdConnect;
	public virtual string                         ClientName       { get; set; }
	public virtual DateTime                       Created          { get; set; } = DateTime.UtcNow;
	public virtual DateTime?                      Updated          { get; set; }
	public virtual List<IdentityClientCorsOrigin> CorsOrigins      { get; set; } = new();
	public virtual string?                        ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}