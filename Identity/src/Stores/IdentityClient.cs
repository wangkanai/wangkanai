// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Identity;

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

public class IdentityClient<TKey> where TKey : IEquatable<TKey>
{
	public IdentityClient() { }

	public IdentityClient(string clientId) : this()
	{
		ClientId = clientId;
	}

	[PersonalData]
	public virtual TKey Id { get; set; } = default!;

	public virtual string ClientId     { get; set; }
	public virtual string ProtocolType { get; set; } = IdentityConstants.ProtocolTypes.OpenIdConnect;
}