// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Wangkanai.Domain;

public abstract class Entity<T> : IEntity<T> where T : IEquatable<T>, IComparable<T>
{
	public T Id { get; set; }

	public bool IsTransient() => Id.Equals(default(T));

	public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);
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

	[SuppressMessage("ReSharper", "HeapView.PossibleBoxingAllocation")]
	public override int GetHashCode()
		=> IsTransient()
			   ? base.GetHashCode()
			   : Id.GetHashCode();

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
