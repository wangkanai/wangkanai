// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Models;

/// <summary>
/// Represents an entity that tracks the date and time of its last update.
/// Inherits properties from <see cref="KeyGuidEntity"/> and implements <see cref="IUpdatedEntity"/>.
/// </summary>
public class UpdatedEntity : KeyGuidEntity, IUpdatedEntity
{
	/// <summary>Gets or sets the timestamp indicating when the entity was last updated.</summary>
	/// <remarks>
	/// This property is typically used for tracking changes to the entity over time.
	/// It can be set automatically during an update operation to record
	/// the moment at which the entity was modified.
	/// </remarks>
	public DateTime? Updated { get; set; }
}
