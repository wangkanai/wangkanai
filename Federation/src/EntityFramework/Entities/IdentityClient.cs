// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Federation.Entities;

public class IdentityClient : IdentityClient<Guid>
{
	public IdentityClient()
	{
		Id = Guid.NewGuid();
	}

	public IdentityClient(string clientId) : this()
	{
		ClientId = clientId;
	}
}

public class IdentityClient<TKey> : IAuditable where TKey : IEquatable<TKey>
{
	public IdentityClient() { }

	public IdentityClient(string clientId) : this()
	{
		ClientId = clientId;
	}

	public virtual TKey      Id               { get; set; } = default!;
	public virtual string    ClientId         { get; set; }
	public virtual string    Name             { get; set; }
	public virtual string    Description      { get; set; }
	public virtual string    ProtocolType     { get; set; } = FederationConstants.ProtocolTypes.OpenIdConnect;
	public virtual DateTime? Created          { get; set; } = DateTime.UtcNow;
	public virtual DateTime? Updated          { get; set; }
	public virtual DateTime LastAccessed     { get; set; }
	public virtual string    ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

	public virtual List<IdentityClientOrigin>      Origins      { get; set; } = new();
	public virtual List<IdentityClientFlowType>    FlowTypes    { get; set; } = new();
	public virtual List<IdentityClientRedirectUri> RedirectUris { get; set; } = new();
}
