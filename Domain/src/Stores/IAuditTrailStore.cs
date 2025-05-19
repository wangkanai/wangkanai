// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Primitives;

namespace Wangkanai.Domain.Stores;

public interface IAuditTrailStore<TKey, TUserType, TUserKey> : IDisposable
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> CreateAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> UpdateAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> DeleteAsync(AuditTrail<TKey, TUserType, TUserKey> auditTrail, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByIdAsync(TKey id, TUserKey userId, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, CancellationToken cancellationToken);
	// Task<Result<AuditTrail<TKey, TUserType, TUserKey>>> FindByUserIdAsync(TUserKey userId, TKey id, CancellationToken cancellationToken);
}
