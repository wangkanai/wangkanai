// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>
/// Represents an abstract entity with a primary key of type <see cref="Guid"/>.
/// This class serves as a base type for entities requiring a unique identifier
/// of type <see cref="Guid"/> within the domain.
/// </summary>
/// <remarks>
/// The <see cref="KeyGuidEntity"/> class inherits from <see cref="Entity{T}"/>,
/// where "T" is a <see cref="Guid"/>. It provides functionality related to
/// identity-based operations and equality comparisons for domain models.
/// </remarks>
public abstract class KeyGuidEntity : Entity<Guid>;
