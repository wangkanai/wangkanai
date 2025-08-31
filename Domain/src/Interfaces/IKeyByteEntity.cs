// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an entity interface with a unique identifier of type byte. Extends the <see cref="IEntity{T}"/> interface, where the identifier type is a byte. This interface serves as a contract for entities that use a byte as their primary key.</summary>
public interface IKeyByteEntity : IEntity<byte>;