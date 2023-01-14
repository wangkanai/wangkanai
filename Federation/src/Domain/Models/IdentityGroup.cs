// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class IdentityGroup : IdentityGroup<string> { }

public class IdentityGroup<TKey> where TKey : IEquatable<TKey>
{
	public virtual string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}