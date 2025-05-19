// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Wangkanai.Domain.Primitives;

namespace Wangkanai.Domain.Stores;

/// <summary>Provides an implementation for managing audit trails within a database context.</summary>
/// <typeparam name="TContext">The type of the database context, which must derive from <see cref="DbContext"/>.</typeparam>
/// <typeparam name="TKey">The type of the primary key for the audit trail, which must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
/// <typeparam name="TUserType">The type of the user associated with the audit trail, which must derive from <see cref="IdentityUser{T}"/>.</typeparam>
/// <typeparam name="TUserKey">The type of the key for the user, which must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
/// <param name="context">The database context instance to be used for accessing the audit trails.</param>
public class AuditTrailStore<TContext, TKey, TUserType, TUserKey>(TContext context) : IQueryableAuditTrailStore<TKey, TUserType, TUserKey>
	where TContext : DbContext
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	private readonly TContext _context = context ?? throw new ArgumentNullException(nameof(context));

	private bool _disposed;

	/// <summary>Provides a queryable collection of audit trails associated with the specified identity user and key types.</summary>
	/// <remarks>This property acts as an interface to the underlying entity set of audit trails, allowing for LINQ-based querying and manipulation of audit trail data. </remarks>
	public IQueryable<AuditTrail<TKey, TUserType, TUserKey>> AuditTrails
		=> AuditTrailsSet;

	private DbSet<AuditTrail<TKey, TUserType, TUserKey>> AuditTrailsSet
		=> _context.Set<AuditTrail<TKey, TUserType, TUserKey>>();

	private Task SaveChangesAsync(CancellationToken cancellationToken)
		=> AutoSaveChanges ? _context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;

	public bool AutoSaveChanges { get; set; } = true;

	public async Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> CreateAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken)
	{
		_context.Add(auditTrail);
		try
		{
			await SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			var error = new Error("ConcurrencyError", ex.Message);
			return new Result<AuditTrail<TKey, TUserType, TUserKey>>(auditTrail, false, error);
		}

		return Result.Success(auditTrail);
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> UpdateAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> DeleteAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, TUserKey userId, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, TKey id, CancellationToken cancellationToken)
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
