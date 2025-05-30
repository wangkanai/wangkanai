// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

// ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
// ReSharper disable NonReadonlyMemberInGetHashCode

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Wangkanai.Domain;

/// <summary>
/// Abstract base class representing an entity with a unique identifier.
/// Provides functionality to check if the entity is transient (not yet persisted).
/// Supports equality operations based on the ID and overrides equality-related methods.
/// Entities inheriting from this class must specify a generic type parameter <typeparamref name="T"/>,
/// which represents the type of the unique identifier. The identifier should implement
/// the <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
/// </summary>
/// <typeparam name="T">
/// The type of the unique identifier for the entity.
/// Must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.
/// </typeparam>
public abstract class Entity<T> : IEntity<T> where T : IEquatable<T>, IComparable<T>
{
	/// <summary>
	/// Gets or sets the unique identifier for the entity.
	/// This property is used to uniquely identify an instance of the entity
	/// within the domain. The type of the identifier is defined by the generic
	/// type parameter of the entity.
	/// </summary>
	public T Id { get; set; }

	/// <summary>
	/// Determines whether the entity is transient, meaning it has not been assigned a valid identifier.
	/// An entity is considered transient if its identifier equals the default value for its type.
	/// </summary>
	/// <returns>true if the entity is transient; otherwise, false.</returns>
	public bool IsTransient() => Id.Equals(default(T));

	/// <summary>
	/// Defines the equality operator for comparing two entities of the same type.
	/// This operator evaluates whether two entities are equal based on their unique identifiers.
	/// If both entities are null, it returns true. If only one entity is null, it returns false.
	/// If neither is null, their IDs are compared to determine equality.
	/// </summary>
	/// <param name="left">The first entity to compare.</param>
	/// <param name="right">The second entity to compare.</param>
	/// <returns>true if both entities are equal based on their IDs; otherwise, false.</returns>
	public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);

	/// <summary>
	/// Defines the inequality operator for comparing two entities of the same type.
	/// This operator evaluates whether two entities are not equal based on their unique identifiers.
	/// If both entities are null, it returns false. If only one entity is null, it returns true.
	/// If neither is null, their IDs are compared to determine inequality.
	/// </summary>
	/// <param name="left">The first entity to compare.</param>
	/// <param name="right">The second entity to compare.</param>
	/// <returns>true if both entities are not equal based on their IDs; otherwise, false.</returns>
	public static bool operator !=(Entity<T> left, Entity<T> right) => !Equals(left, right);

	private static Type GetRealObjectType(object obj)
	{
		var retValue = obj.GetType();
		// because can ne compared two object with same id and 'types' but own of it is EF dynamics proxy type)
		if (retValue.BaseType != null && retValue.Namespace == "System.Data.Entity.DynamicProxies")
			retValue = retValue.BaseType;
		return retValue;
	}

	#region Overrides Methods

	/// <summary>
	/// Returns the hash code for the entity.
	/// The hash code is derived from the entity's unique identifier if it exists.
	/// If the entity is transient (does not have a valid identifier), the base hash code is used.
	/// </summary>
	/// <returns>An integer representing the hash code of the entity.</returns>
	[SuppressMessage("ReSharper", "HeapView.PossibleBoxingAllocation")]
	public override int GetHashCode()
		=> IsTransient()
			   ? base.GetHashCode()
			   : Id.GetHashCode();

	/// <summary>
	/// Determines whether the current entity is equal to another object.
	/// The equality comparison is based on the unique identifier of the entity.
	/// </summary>
	/// <param name="obj">The object to compare with the current entity.</param>
	/// <returns>true if the specified object is equal to the current entity; otherwise, false.</returns>
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(this, obj))
			return true;
		if (ReferenceEquals(null, obj))
			return false;
		if (GetRealObjectType(this) != GetRealObjectType(obj))
			return false;

		var other = obj as Entity<T>;

		return other is not null && Operator.Equal(Id, other.Id);
	}

	#endregion
}
