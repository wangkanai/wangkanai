// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an entity interface with a unique identifier of type <see cref="long"/>. Extends the base <see cref="IEntity{T}"/>
/// interface, specifically utilizing a <see cref="long"/> type for the unique identifier. This interface is typically used for entities within a domain-driven design context that require long-type keys.</summary>
public interface IKeyLongEntity : IEntity<long>;