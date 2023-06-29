// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Federation.Entities;

public class IdentityResource : IdentityResource<Guid>
{
	public IdentityResource()
	{
		Id = Guid.NewGuid();
	}

	public IdentityResource(string name) : this()
	{
		Name = name;
	}
}

public class IdentityResource<TKey> : IAuditable
	where TKey : IEquatable<TKey>
{
	public IdentityResource() { }

	public IdentityResource(string name) : this()
	{
		Name = name;
	}

	public virtual TKey      Id               { get; set; } = default!;
	public virtual string    Name             { get; set; }
	public virtual string    DisplayName      { get; set; }
	public virtual string    Description      { get; set; }
	public virtual DateTime? Created          { get; set; } = DateTime.UtcNow;
	public virtual DateTime? Updated          { get; set; }
	public virtual string   ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}