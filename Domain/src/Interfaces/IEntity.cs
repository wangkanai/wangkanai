// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

public interface IEntity<T> where T : IComparable<T>, IEquatable<T>
{
	T    Id { get; set; }
	bool IsTransient();
}
