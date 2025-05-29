// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Represents an audit trail record for tracking entity changes in the system.</summary>
/// <typeparam name="TKey">The type of the unique identifier for the audit trail.</typeparam>
/// <typeparam name="TUserType">The type of the user associated with the audit action.</typeparam>
/// <typeparam name="TUserKey">The type of the user's unique identifier.</typeparam>
public class Audit<TKey, TUserType, TUserKey> : Entity<TKey>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	/// <summary>Gets or sets the type of trail associated with an audit action.</summary>
	/// <remarks>
	/// The <see cref="TrailType"/> property indicates the nature of the change that occurred
	/// in an entity. It reflects whether the entity was created, updated, or deleted, or if
	/// no changes were made.
	/// </remarks>
	public TrailType TrailType { get; set; }

	/// <summary>Gets or sets the unique identifier of the user associated with the audit action.</summary>
	/// <remarks>
	/// The <see cref="UserId"/> property records the primary key of the user who performed
	/// the action being tracked. This property may be null if the action was performed
	/// in a context where a user is not available or applicable.
	/// </remarks>
	public TUserKey? UserId { get; set; }

	/// <summary>Gets or sets the user associated with the audit action.</summary>
	/// <remarks>
	/// The User property represents the user who performed the action being tracked in the audit trail.
	/// It is of a generic type derived from <see cref="IdentityUser{TKey}"/> to allow flexibility in user representation.
	/// </remarks>
	public TUserType? User { get; set; }

	/// <summary>Gets or sets the timestamp for the audit trail entry.</summary>
	/// <remarks>
	/// The <c>Timestamp</c> property represents the date and time when the audit action occurred.
	/// It is used for tracking the moment an entity change was recorded in the system.
	/// </remarks>
	public DateTime Timestamp { get; set; }

	/// <summary>Gets or sets the primary key value of the audited entity.</summary>
	/// <remarks>
	/// The <see cref="PrimaryKey"/> property holds the unique identifier of the entity being
	/// audited. This value helps in identifying the specific entity record subject to the
	/// audit trail entry.
	/// </remarks>
	public string? PrimaryKey { get; set; }

	/// <summary>Gets or sets the name of the entity associated with the audit trail.</summary>
	/// <remarks>
	/// The <see cref="EntityName"/> property identifies the entity affected by the audit action.
	/// This property is typically used to associate the audit record with a specific entity type
	/// in the system, such as a database table or domain object.
	/// </remarks>
	public string EntityName { get; set; }

	/// <summary>Gets or sets the list of column names that were changed during an audit action.</summary>
	/// <remarks>
	/// The <see cref="ChangedColumns"/> property contains the names of the specific columns in an entity
	/// that were modified as part of the audit record. This can be used to identify and track which fields
	/// have been updated in a system, providing valuable context for changes made.
	/// </remarks>
	public List<string> ChangedColumns { get; set; } = [];

	/// <summary>Gets or sets the collection of old values of the changed entity properties.</summary>
	/// <remarks>
	/// The <see cref="OldValues"/> property contains a dictionary where keys are the names of the entity's
	/// properties and values are their respective values before the change occurred. This is used to track
	/// the original state of the entity before modifications were applied.
	/// </remarks>
	public Dictionary<string, object> OldValues { get; set; } = [];

	/// <summary>Gets or sets the new values of the changed properties in the audited entity.</summary>
	/// <remarks>
	/// The <see cref="NewValues"/> property stores a collection of key-value pairs where keys represent
	/// the names of the changed properties, and values represent their new values after the change.
	/// This property is used to capture the state of the entity after modifications during an audit trail.
	/// </remarks>
	public Dictionary<string, object> NewValues { get; set; } = [];
}
