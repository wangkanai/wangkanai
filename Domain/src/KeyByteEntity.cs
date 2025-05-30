// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>
/// Represents a base class for entities with a unique identifier of type <see cref="byte"/>.
/// This abstract class extends the <see cref="Entity{T}"/> class where T is <see cref="byte"/>.
/// </summary>
/// <remarks>
/// The unique identifier of the entity is of type <see cref="byte"/>, making it suitable for scenarios
/// where a small numeric identifier is used. Instances of this class or its derived classes can determine
/// whether they are transient (not yet persisted) through the inherited <see cref="IsTransient"/> method.
/// </remarks>
public abstract class KeyByteEntity : Entity<byte>;
