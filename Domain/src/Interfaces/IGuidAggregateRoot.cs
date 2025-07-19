// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an aggregate root within a domain-driven design context that uses a <see cref="Guid"/> as its unique identifier. Combines the functionalities of <see cref="IAggregateRoot{T}"/> and <see cref="IKeyGuidEntity"/>.</summary>
/// <remarks>
/// This interface ensures the encapsulation and consistency of business logic
/// across complex domain entities that share a common aggregate root.
/// By using a <see cref="Guid"/> key, it provides a universally unique identifier for entities.
/// </remarks>
public interface IGuidAggregateRoot : IAggregateRoot<Guid>, IKeyGuidEntity;
