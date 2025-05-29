// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Configurations;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class AuditTrailContextExtensions
{
	public static void ApplyAuditTrailConfiguration<TKey, TUserKey, TUserType>(this ModelBuilder builder)
		where TKey : IEquatable<TKey>, IComparable<TKey>
		where TUserType : IdentityUser<TUserKey>
		where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
	{
		builder.ApplyConfiguration(new AuditTrailConfiguration<TKey, TUserKey, TUserType>());
	}
}
