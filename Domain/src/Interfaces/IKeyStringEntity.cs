// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Represents an entity interface with a string-based key, inheriting from <see cref="IEntity{T}"/>. This interface is intended for entities that use a string as their unique identifier.</summary>
public interface IKeyStringEntity : IEntity<string>;
