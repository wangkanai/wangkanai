// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Models;

public class AuditEntity : KeyGuidEntity, ICreatedEntity, IUpdatedEntity
{
	/// <summary>Gets or sets the date and time when the entity was created.</summary>
	/// <remarks>
	/// This property is used to track the creation timestamp of the entity and is typically
	/// set automatically when the entity is first saved to the database. It may be null if the creation date
	/// has not been initialized or is not applicable.
	/// </remarks>
	public DateTime? Created { get; set; }

	/// <summary>Gets or sets the date and time when the entity was last updated.</summary>
	/// <remarks>
	/// This property is used to track the last modification timestamp of the entity and is typically
	/// updated automatically whenever the entity changes. It may be null if the entity has not been updated
	/// since its creation or if updates are not applicable.
	/// </remarks>
	public DateTime? Updated { get; set; }
}
