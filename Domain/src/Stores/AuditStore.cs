// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Primitives;

namespace Wangkanai.Domain.Stores;

/// <summary>Provides an implementation for managing audit trails within a database context.</summary>
/// <typeparam name="TContext">The type of the database context, which must derive from <see cref="DbContext"/>.</typeparam>
/// <typeparam name="TKey">The type of the primary key for the audit trail, which must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
/// <typeparam name="TUserType">The type of the user associated with the audit trail, which must derive from <see cref="IdentityUser{T}"/>.</typeparam>
/// <typeparam name="TUserKey">The type of the key for the user, which must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
/// <param name="context">The database context instance to be used for accessing the audit trails.</param>
public class AuditStore<TContext, TKey, TUserType, TUserKey>(TContext context) : IQueryableAuditStore<TKey, TUserType, TUserKey>
	where TContext : DbContext
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));

	private bool _disposed;

	/// <summary>Provides a queryable collection of audit trails associated with the specified identity user and key types.</summary>
	/// <remarks>This property acts as an interface to the underlying entity set of audit trails, allowing for LINQ-based querying and manipulation of audit trail data. </remarks>
	public IQueryable<Audit<TKey, TUserType, TUserKey>> Audits
		=> AuditsSet;

	private DbSet<Audit<TKey, TUserType, TUserKey>> AuditsSet
		=> _context.Set<Audit<TKey, TUserType, TUserKey>>();

	private Task SaveChangesAsync(CancellationToken cancellationToken)
		=> AutoSaveChanges ? _context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;

	/// <summary>Indicates whether changes to the context are automatically persisted to the database upon certain operations.</summary>
	/// <remarks>When set to true, any modifications to the underlying data store resulting from method calls such as create, update, or delete will automatically trigger a call to save changes on the database context. If set to false, changes must be explicitly saved manually by invoking the appropriate context method.</remarks>
	public bool AutoSaveChanges { get; set; } = true;

	/// <summary>Creates a new audit trail entry in the underlying storage context.</summary>
	/// <param name="audit">The audit entity to be created.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the created audit entity, indicating success or failure with potential error information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>>> CreateAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken)
	{
		_context.Add(audit);
		try
		{
			await SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			var error = new Error(ErrorCodes.Concurrency, ex.Message);
			return new Result<Audit<TKey, TUserType, TUserKey>>(audit, false, error);
		}

		return Result.Success(audit);
	}

	/// <summary>Updates an existing audit entity in the underlying storage context.</summary>
	/// <param name="audit">The audit entity with updated information to be persisted.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the updated audit entity, indicating success or failure with potential error details.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>>> UpdateAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken)
	{
		_context.Attach(audit);
		_context.Update(audit);

		try
		{
			await SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			var error = new Error(ErrorCodes.Concurrency, ex.Message);
			return new Result<Audit<TKey, TUserType, TUserKey>>(audit, false, error);
		}

		return Result.Success(audit);
	}

	/// <summary>Deletes a specified audit entry from the underlying storage context.</summary>
	/// <param name="audit">The audit entity to be deleted.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the deleted audit entity, indicating success or failure with potential error information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>>> DeleteAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken)
	{
		_context.Remove(audit);

		try
		{
			await SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			var error = new Error(ErrorCodes.Concurrency, ex.Message);
			return new Result<Audit<TKey, TUserType, TUserKey>>(audit, false, error);
		}

		return Result.Success(audit);
	}

	/// <summary>Finds an audit entry by its unique identifier.</summary>
	/// <param name="id">The unique identifier of the audit entry to retrieve.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the found audit entry if it exists, or null if not found, along with success or failure information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByIdAsync(TKey id, CancellationToken cancellationToken)
	{
		var audit = await _context.Set<Audit<TKey, TUserType, TUserKey>>().FindAsync(id, cancellationToken);
		return Result.Success(audit);
	}

	/// <summary>Finds an audit entry by its unique identifier and associated user ID.</summary>
	/// <param name="id">The unique identifier of the audit entry to find.</param>
	/// <param name="userId">The unique identifier of the user associated with the audit entry.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the found audit entity or null if not found, indicating success or failure with potential error information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByIdAsync(TKey id, TUserKey userId, CancellationToken cancellationToken)
	{
		var audit = await _context.Set<Audit<TKey, TUserType, TUserKey>>().FirstOrDefaultAsync(a => a.Id.Equals(id) && a.UserId!.Equals(userId), cancellationToken);
		return Result.Success(audit);
	}

	/// <summary>
	/// Retrieves an audit entry associated with the specified user ID from the storage context.
	/// </summary>
	/// <param name="userId">The unique identifier of the user to find the associated audit entry.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the found audit entity or null, indicating the operation's outcome with potential error information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByUserIdAsync(TUserKey userId, CancellationToken cancellationToken)
	{
		var audit = await _context.Set<Audit<TKey, TUserType, TUserKey>>().FirstOrDefaultAsync(a => a.UserId!.Equals(userId), cancellationToken);
		return Result.Success(audit);
	}

	/// <summary>Finds an audit entry based on the specified user ID and audit ID.</summary>
	/// <param name="userId">The ID of the user associated with the audit entry.</param>
	/// <param name="id">The unique identifier of the audit entry to find.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the found audit entry if it exists, or null if not found, along with success or failure status and potential error information.</returns>
	public async Task<Result<Audit<TKey, TUserType, TUserKey>?>> FindByUserIdAsync(TUserKey userId, TKey id, CancellationToken cancellationToken)
	{
		var audit = await _context.Set<Audit<TKey, TUserType, TUserKey>>().FirstOrDefaultAsync(a => a.UserId!.Equals(userId) && a.Id.Equals(id), cancellationToken);
		return Result.Success(audit);
	}

	/// <summary>Releases the resources used by the current instance of the class.</summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}


	private void Dispose(bool disposing)
	{
		if (_disposed)
			return;
		if (disposing)
			_context?.Dispose();
		_disposed = true;
	}
}
