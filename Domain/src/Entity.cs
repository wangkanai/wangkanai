// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.System.Domain;

public abstract class Entity : Entity<Guid> { }

public abstract class Entity<T> : IEntity<T> // where T : IComparable<T> //, Nullable<T>
{
	public T Id { get; set; }

	public bool IsTransient()
		=> Id == null;

	private Type GetRealObjectType(object obj)
	{
		var retValue = obj.GetType();
		// bacause can ne compared two object with same id and 'types' but own of it is EF dynamics proxy type)
		if (retValue.BaseType != null && retValue.Namespace == "System.Data.Entity.DynamicProxies")
			retValue = retValue.BaseType;
		return retValue;
	}

	#region Overrides Methods

	[SuppressMessage("ReSharper", "HeapView.PossibleBoxingAllocation")]
	public override int GetHashCode()
	{
		unchecked
		{
#pragma warning disable S3249 // Classes directly extending 'object' should not call 'base' in 'GetHashCode' or 'Equals'
			// For Entities without Id we want to use object GetHashCode
			return IsTransient() ? base.GetHashCode() : Id.GetHashCode();
#pragma warning restore S3249 // Classes directly extending 'object' should not call 'base' in 'GetHashCode' or 'Equals'
		}
	}

	public static bool operator ==(Entity<T> left, Entity<T> right)
		=> Equals(left, right);

	public static bool operator !=(Entity<T> left, Entity<T> right)
		=> !Equals(left, right);


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