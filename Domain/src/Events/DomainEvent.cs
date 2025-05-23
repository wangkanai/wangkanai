// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

public class DomainEvent : DomainEvent<int>;

public class DomainEvent<T> : Entity<T>, IDomainEvent<T>
	where T : IComparable<T>, IEquatable<T>
{
	public int            Version   { get; set; }
	public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
}
