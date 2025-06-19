// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Audit.Stores;

/// <summary>Represents an abstraction for a queryable audit trail store, providing access to audit trail records for tracking changes in the system.</summary>
/// <typeparam name="TKey">The type of the unique identifier for the audit trail. It must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>. </typeparam>
/// <typeparam name="TUserType">The type of the user associated with the audit trail. It must inherit from <see cref="IdentityUser"/>.</typeparam>
/// <typeparam name="TUserKey">The type of the unique identifier for the user. It must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>. </typeparam>
public interface IQueryableAuditStore<TKey, TUserType, TUserKey> : IAuditStore<TKey, TUserType, TUserKey>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	/// <summary>Gets an <see cref="IQueryable"/> collection of <see cref="Audit{TKey,TUserType,TUserKey}"/>. This property provides access to query audit trail records stored in the underlying data store. </summary>
	/// <typeparam name="TKey">The type of the primary key for the <see cref="Audit{TKey,TUserType,TUserKey}"/> entity.</typeparam>
	/// <typeparam name="TUserType">The type representing the user associated with the audit trail.</typeparam>
	/// <typeparam name="TUserKey">The type of the primary key for the user entity.</typeparam>
	IQueryable<Audit<TKey, TUserType, TUserKey>> Audits { get; }
}
