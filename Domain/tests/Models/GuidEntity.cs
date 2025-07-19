// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Models;

/// <summary>Represents an entity with a globally unique identifier (GUID) as its primary key.</summary>
public class GuidEntity : KeyGuidEntity
{
	/// <summary>Represents an entity with a globally unique identifier (GUID) as its primary key.</summary>
	public GuidEntity() => Id = Guid.NewGuid();
}

/// <summary>
/// Represents a transient entity with a globally unique identifier (GUID) as its primary key.
/// This entity is initialized with an empty <see cref="Guid"/> value, indicating its transient state.
/// </summary>
public class TransientGuidEntity : KeyGuidEntity
{
	/// <summary>
	/// Represents a transient entity with a globally unique identifier (GUID) as its primary key,
	/// initialized to an empty GUID value, indicating a transient or non-persistent state.
	/// </summary>
	public TransientGuidEntity() => Id = Guid.Empty;
}
