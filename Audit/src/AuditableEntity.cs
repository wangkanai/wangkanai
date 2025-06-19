// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Audit;

/// <summary>
/// Represents an auditable entity with properties for tracking creation and modification timestamps.
/// </summary>
/// <typeparam name="T">
/// The type of the identifier for the entity. Must implement <see cref="IComparable{T}"/> and <see cref="IEquatable{T}"/>.
/// </typeparam>
public abstract class AuditableEntity<T> : Entity<T>, IAuditable where T : IComparable<T>, IEquatable<T>
{
	#region IAuditable Members

	/// <summary>Gets or sets the date and time when the entity was created.</summary>
	/// <remarks>
	/// This property is nullable to account for scenarios where the creation timestamp might not
	/// be immediately available or applicable. It is primarily used to track when the entity
	/// was first instantiated or persisted into the system.
	/// </remarks>
	public DateTime? Created { get; set; }

	/// <summary>Gets or sets the date and time when the entity was last updated.</summary>
	/// <remarks>
	/// This property is nullable to accommodate scenarios where the last updated timestamp may not
	/// be available or applicable. It is typically used to track modifications made to the entity
	/// after its initial creation.
	/// </remarks>
	public DateTime? Updated { get; set; }

	#endregion

	/// <summary>Determines whether the auditable properties of the entity should be serialized.</summary>
	/// <remarks>
	/// This property acts as a central flag to control the serialization of all auditable timestamp
	/// properties, such as creation and modification dates. It can be overridden in derived classes
	/// to customize the serialization behavior based on specific requirements.
	/// </remarks>
	public virtual bool ShouldSerializeAuditableProperties => true;

	/// <summary>Determines whether the Created date of the entity should be serialized.</summary>
	/// <returns>A boolean value indicating whether the Created date should be included in serialization.</returns>
	public virtual bool ShouldSerializeCreatedDate() => ShouldSerializeAuditableProperties;

	/// <summary>Determines whether the Updated date of the entity should be serialized.</summary>
	/// <returns>A boolean value indicating whether the Updated date should be included in serialization.</returns>
	public virtual bool ShouldSerializeUpdatedDate() => ShouldSerializeAuditableProperties;
}
