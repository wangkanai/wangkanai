// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.Domain;

public abstract class Entity : Entity<Guid> { }

public abstract class Entity<T> : IEntity<T> // where T : IComparable<T> //, Nullable<T>
{
	public T Id { get; set; }

	public bool IsTransient() => Id == null || Id.Equals(default(T));

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

	public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);

	public static bool operator !=(Entity<T> left, Entity<T> right) => !Equals(left, right);

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(this, obj))
			return true;
		if (ReferenceEquals(null, obj))
			return false;
		if (GetRealObjectType(this) != GetRealObjectType(obj))
			return false;

		var other = obj as Entity<T>;

		return other != null && Operator.Equal(Id, other.Id);
	}

	#endregion
}