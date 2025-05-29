// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Primitives;

namespace Wangkanai.Domain.Stores;

/// <summary>Defines the contract for an audit trail store, capable of managing audit trail entities, including creation operations.</summary>
/// <typeparam name="TKey">The type of the primary key for the audit trail entity.</typeparam>
/// <typeparam name="TUserType">The type representing the user related to the audit trail entity.</typeparam>
/// <typeparam name="TUserKey">The type of the primary key for the user entity.</typeparam>
public interface IAuditStore<TKey, TUserType, TUserKey> : IDisposable
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	/// <summary>Creates a new audit entry asynchronously.</summary>
	/// <param name="audit">The audit entity to be created.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the created audit.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>>> CreateAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken);

	/// <summary>Updates an existing audit entry asynchronously.</summary>
	/// <param name="auditTrail">The audit entity to be updated.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the updated audit.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>>> UpdateAsync(Audit<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken);

	/// <summary>Deletes an existing audit entry asynchronously.</summary>
	/// <param name="auditTrail">The audit entity to be deleted.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the deletion operation.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>>> DeleteAsync(Audit<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken);

	/// <summary>Finds an audit entry by its identifier asynchronously.</summary>
	/// <param name="id">The unique identifier of the audit entry to be retrieved.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the audit entry if it exists.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByIdAsync(TKey id, CancellationToken cancellationToken);

	/// <summary>Finds an audit entry by its identifier and associated user identifier asynchronously.</summary>
	/// <param name="id">The unique identifier of the audit entry to be retrieved.</param>
	/// <param name="userId">The unique identifier of the user associated with the audit entry.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the requested audit entry.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByIdAsync(TKey id, TUserKey userId, CancellationToken cancellationToken);

	/// <summary>Finds an audit entry by the specified user ID asynchronously.</summary>
	/// <param name="userId">The unique identifier of the user to search for.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the result of the audit entry associated with the specified user ID.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByUserIdAsync(TUserKey userId, CancellationToken cancellationToken);

	/// <summary>Finds an audit entry based on the user ID and audit ID asynchronously.</summary>
	/// <param name="userId">The ID of the user associated with the audit entry.</param>
	/// <param name="id">The unique identifier of the audit entry.</param>
	/// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
	/// <returns>A task representing the asynchronous operation, containing the result of the found audit entry.</returns>
	Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByUserIdAsync(TUserKey userId, TKey id, CancellationToken cancellationToken);
}
