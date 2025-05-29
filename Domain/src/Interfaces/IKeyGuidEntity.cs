// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>
/// Represents a domain entity with a <see cref="Guid"/> as its unique identifier.
/// This interface extends the <see cref="IEntity{T}"/> interface,
/// utilizing <see cref="Guid"/> as the type for the entity's identifier.
/// </summary>
/// <remarks>
/// Implementing this interface ensures consistent usage of <see cref="Guid"/> identifiers
/// across entities within a domain-driven design context. It is commonly used
/// for entities where a globally unique identifier is required.
/// </remarks>
public interface IKeyGuidEntity : IEntity<Guid>;
