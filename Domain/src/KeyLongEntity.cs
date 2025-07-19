// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>
/// Represents an abstract entity with a unique identifier of type <see cref="long"/>.
/// This class provides a foundational model for entities in a domain-driven design context
/// where the identifier is a 64-bit signed integer.
/// </summary>
/// <remarks>
/// Inherits from <see cref="Entity{T}"/> with a generic parameter of type <see cref="long"/>.
/// Provides default implementations for equality comparison, transient state detection,
/// and hash code generation.
/// </remarks>
public abstract class KeyLongEntity : Entity<long>;
