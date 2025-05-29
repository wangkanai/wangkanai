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
	public IQueryable<Audit<TKey, TUserType, TUserKey>> AuditTrails
		=> AuditTrailsSet;

	private DbSet<Audit<TKey, TUserType, TUserKey>> AuditTrailsSet
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

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> UpdateAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> DeleteAsync(Audit<TKey, TUserType, TUserKey> audit, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, TUserKey userId, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<Audit<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, TKey id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

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
