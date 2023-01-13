// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class IdentityScope : IdentityScope<string> { }

public class IdentityScope<TKey> where TKey : IEquatable<TKey>
{
	public virtual TKey   Id          { get; set; } = default!;
	public virtual string Name        { get; set; }
	public virtual string DisplayName { get; set; }
	public virtual string Description { get; set; }
}