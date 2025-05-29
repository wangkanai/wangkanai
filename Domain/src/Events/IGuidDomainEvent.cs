// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a domain event interface that uses a globally unique identifier (GUID) as its primary key.
/// This interface combines the characteristics of an entity identified by a GUID and the behavior of a domain event.
/// It enables the encapsulation of domain-specific state changes or business rules triggered by a specific action.
/// </summary>
public interface IGuidDomainEvent : IKeyGuidEntity, IDomainEvent<Guid>;
