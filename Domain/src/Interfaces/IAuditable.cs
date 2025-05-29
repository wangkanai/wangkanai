// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Represents an entity that is auditable, tracking creation and update timestamps.</summary>
/// <remarks>
/// This interface combines the functionality of <see cref="ICreatedEntity"/> and <see cref="IUpdatedEntity"/>
/// to provide a standard structure for auditing entities.
/// </remarks>
public interface IAuditable : ICreatedEntity, IUpdatedEntity;
