// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Audit.Configurations;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>Provides extension methods for configuring audit-related entity configurations in the Entity Framework model.</summary>
public static class AuditContextExtensions
{
	/// <summary>Applies audit-related configurations to the Entity Framework model.</summary>
	/// <typeparam name="TKey">The type of the primary key for the audit entity.</typeparam>
	/// <typeparam name="TUserType">The type of the user entity associated with the audit entity.</typeparam>
	/// <typeparam name="TUserKey">The type of the primary key for the user entity.</typeparam>
	/// <param name="builder">The Entity Framework <see cref="ModelBuilder"/> used to configure entity mappings.</param>
	public static void ApplyAuditConfiguration<TKey, TUserType, TUserKey>(this ModelBuilder builder)
		where TKey : IEquatable<TKey>, IComparable<TKey>
		where TUserType : IdentityUser<TUserKey>
		where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
	{
		builder.ApplyConfiguration(new AuditConfiguration<TKey, TUserType, TUserKey>());
	}
}
