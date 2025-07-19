// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain;

namespace Wangkanai.Federation.Entities;

public class IdentityGroup : IdentityGroup<Guid>
{
	public IdentityGroup()
	{
		Id = Guid.NewGuid();
	}

	public IdentityGroup(string name) : this()
	{
		Name = name;
	}
}

public class IdentityGroup<TKey> : IAuditable
	where TKey : IEquatable<TKey>
{
	public IdentityGroup() { }

	public IdentityGroup(string name) : this()
	{
		Name = name;
	}

	public virtual TKey Id { get; set; } = default!;
	public virtual string Name { get; set; }
	public virtual DateTime? Created { get; set; } = DateTime.UtcNow;
	public virtual DateTime? Updated { get; set; }
	public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
