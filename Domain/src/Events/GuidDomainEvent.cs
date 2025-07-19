// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a domain event identified by a unique <see cref="Guid"/>.
/// This class provides a default implementation for generating a new unique identifier upon instantiation.
/// </summary>
public class GuidDomainEvent : DomainEvent<Guid>
{
	/// <summary>
	/// Represents a domain event identified by a unique <see cref="Guid"/>.
	/// Provides a default implementation for generating a new unique identifier upon instantiation.
	/// </summary>
	public GuidDomainEvent() => Id = Guid.NewGuid();
}
