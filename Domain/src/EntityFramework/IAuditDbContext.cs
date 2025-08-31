// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.EntityFramework;

/// <summary>Defines the contract for an audit-aware database context. Provides access to the underlying database context for audit-related operations.</summary>
public interface IAuditDbContext
{
   /// <summary>Represents the database context associated with the audit-aware operations. Provides access to the database context for performing data-related tasks within the implementation of the IAuditDbContext interface.</summary>
   DbContext DbContext { get; }
}