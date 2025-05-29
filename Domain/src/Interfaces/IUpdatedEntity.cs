// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Defines an entity that tracks the timestamp of its last update.</summary>
/// <remarks>This interface is typically implemented by entities that require an updated audit field, allowing tracking of changes over time. The `Updated` property stores the date and time of the most recent modification. </remarks>
public interface IUpdatedEntity
{
	/// <summary>Gets or sets the timestamp of the most recent update to the entity.</summary>
	/// <remarks>
	/// This property allows tracking when an entity was last modified.
	/// It is useful for auditing purposes, providing visibility into
	/// the update history of the implementing entity.
	/// </remarks>
	DateTime? Updated { get; set; }
}
