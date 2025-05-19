// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Domain.Stores;

/// <summary>
/// Represents an abstraction for a queryable audit trail store, providing access
/// to audit trail records for tracking changes in the system.
/// </summary>
/// <typeparam name="TKey">
/// The type of the unique identifier for the audit trail.
/// It must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.
/// </typeparam>
/// <typeparam name="TUserType">
/// The type of the user associated with the audit trail.
/// It must inherit from <see cref="IdentityUser{TUserKey}"/>.
/// </typeparam>
/// <typeparam name="TUserKey">
/// The type of the unique identifier for the user.
/// It must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.
/// </typeparam>
public interface IQueryableAuditTrailStore<TKey, TUserType, TUserKey> : IAuditTrailStore<TKey, TUserType, TUserKey>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	IQueryable<AuditTrail<TKey, TUserType, TUserKey>> AuditTrails { get; }
}
