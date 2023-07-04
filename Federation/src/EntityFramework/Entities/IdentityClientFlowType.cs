// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Entities;

public class IdentityClientFlowType : IdentityClientGrantType<Guid>
{
	public IdentityClientFlowType()
	{
		Id = Guid.NewGuid();
	}

	public IdentityClientFlowType(Guid clientId, FlowTypes flowType) : this()
	{
		ClientId = clientId;
		FlowType = flowType;
	}
}

public class IdentityClientGrantType<TKey> where TKey : IEquatable<TKey>
{
	public IdentityClientGrantType() { }

	public IdentityClientGrantType(TKey clientId, FlowTypes flowType) : this()
	{
		ClientId = clientId;
		FlowType = flowType;
	}

	public virtual TKey           Id       { get; set; } = default!;
	public virtual TKey           ClientId { get; set; }
	public virtual IdentityClient Client   { get; set; }
	public virtual FlowTypes      FlowType { get; set; }
}