// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


using Microsoft.AspNetCore.Identity;

using Wangkanai.Domain;

namespace Wangkanai.Federation.Models;

public class IdentityDirectory : IdentityDirectory<string>
{
	public IdentityDirectory()
	{
		Id = Guid.NewGuid().ToString();
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

	[PersonalData]
	public virtual TKey Id { get; set; } = default!;

	public virtual string    Name             { get; set; }
	public virtual string    DisplayName      { get; set; }
	public virtual string    Description      { get; set; }
	public virtual DateTime  Created          { get; set; } = DateTime.UtcNow;
	public virtual DateTime? Updated          { get; set; }
	public virtual string?   ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}