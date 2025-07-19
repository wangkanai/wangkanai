// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Events;

/// <summary>Represents a domain event with a default identifier type of int. This class serves as a specialization of the generic DomainEvent class with int as the identifier type.</summary>
public class DomainEvent : DomainEvent<int>;

/// <summary>Represents a general domain event which captures significant events in the domain model.</summary>
/// <typeparam name="T">The type of the identifier for the domain event, which must implement both IComparable and IEquatable interfaces.</typeparam>
public class DomainEvent<T> : Entity<T>, IDomainEvent<T>
	where T : IComparable<T>, IEquatable<T>
{
	/// <summary>
	/// Gets or sets the version of the domain event. This property is used to
	/// track the version of the event, which can help with concurrency control
	/// and managing changes in the domain model over time.
	/// </summary>
	public int Version { get; set; }

	/// <summary>
	/// Gets or sets the timestamp of the domain event. This property records the
	/// precise date and time when the event occurred, stored as a UTC value, and is
	/// vital for event tracking and chronological ordering of domain activities.
	/// </summary>
	public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
}
