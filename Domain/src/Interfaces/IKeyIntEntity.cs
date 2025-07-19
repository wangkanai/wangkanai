// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>
/// Defines an interface for entities with a unique integer key.
/// This interface serves as a specialization of the generic
/// <see cref="IEntity{T}"/> for entities whose identifier is an integer.
/// </summary>
public interface IKeyIntEntity : IEntity<int>;
