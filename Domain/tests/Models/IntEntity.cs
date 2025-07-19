// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Models;

/// <summary>Represents an entity with an integer-based unique identifier.</summary>
/// <remarks>
/// IntEntity is derived from KeyIntEntity, which provides integer key functionality.
/// The unique identifier <see cref="Id"/> is assigned a default value of 1 in the constructor.
/// </remarks>
public class IntEntity : KeyIntEntity
{
	/// <summary>Represents an entity with an integer-based unique identifier.</summary>
	/// <remarks>
	/// Inherits from <see cref="KeyIntEntity"/>, which provides the integer key property.
	/// The unique identifier is initialized to a default value of 1 upon object creation.
	/// </remarks>
	public IntEntity() => Id = 1;
}

/// <summary>Represents a transient entity with an integer-based unique identifier.</summary>
/// <remarks>
/// TransientIntEntity inherits from KeyIntEntity, providing an integer key functionality and transient behavior.
/// This class is designed to be used where the entity does not persist and is considered transient.
/// </remarks>
public class TransientIntEntity : KeyIntEntity { }
