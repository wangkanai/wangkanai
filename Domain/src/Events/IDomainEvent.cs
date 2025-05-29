// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Messages;

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a domain event that contains key information and is part of
/// the domain-driven design framework. This interface serves as a base
/// for all domain events with an integer key type.
/// </summary>
public interface IDomainEvent : IKeyIntEntity, IDomainEvent<int>;

/// <summary>
/// Defines a contract for a domain event that encapsulates significant state changes or behaviors
/// within the domain model. This serves as the base interface for implementing domain events
/// with an integer identifier and integrates with domain-driven design concepts.
/// </summary>
public interface IDomainEvent<T> : IEntity<T>, IDomainMessage where T : IComparable<T>, IEquatable<T>
{
	/// <summary>
	/// Gets or sets the version of the event. This property is used to track
	/// the versioning of the domain event, facilitating compatibility and
	/// change management within the domain model.
	/// </summary>
	int Version { get; set; }

	/// <summary>
	/// Gets or sets the timestamp of the domain event. This property records the exact
	/// moment when the event occurred, enabling precise tracking and ordering of events
	/// within the domain model.
	/// </summary>
	DateTimeOffset TimeStamp { get; set; }
}
