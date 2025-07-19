// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain;

namespace Wangkanai.Federation.Entities;

public class IdentityDirectory : IdentityDirectory<Guid>
{
	public IdentityDirectory()
	{
		Id = Guid.NewGuid();
	}

	public IdentityDirectory(string name) : this()
	{
		Name = name;
	}
}

public class IdentityDirectory<TKey> : IAuditable
	where TKey : IEquatable<TKey>
{
	public IdentityDirectory() { }

	public IdentityDirectory(string name) : this()
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
