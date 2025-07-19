// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Wangkanai.Federation.Entities;

public class IdentityScope : IdentityScope<Guid>
{
	public IdentityScope()
	{
		Id = Guid.NewGuid();
	}

	public IdentityScope(string name) : this()
	{
		Name = name;
	}
}

public class IdentityScope<TKey> : IAuditable
	where TKey : IEquatable<TKey>
{
	public IdentityScope() { }

	public IdentityScope(string name) : this()
	{
		Name = name;
	}

	public virtual TKey Id { get; set; } = default!;
	public virtual string Name { get; set; }
	public virtual string DisplayName { get; set; }
	public virtual string Description { get; set; }
	public virtual DateTime? Created { get; set; } = DateTime.UtcNow;
	public virtual DateTime? Updated { get; set; }
	public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
