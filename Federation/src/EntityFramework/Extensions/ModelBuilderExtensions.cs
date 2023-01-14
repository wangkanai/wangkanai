// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.EntityFramework.Extensions;

internal static class ModelBuilderExtensions
{
	public static void ConfigureClientContext<TClient, TKey>(this ModelBuilder builder)
		where TClient : IdentityClient<TKey>
		where TKey : IEquatable<TKey>
	{
		builder.Entity<TClient>(b => {
			b.HasKey(c => c.Id);
			b.HasIndex(c => c.ClientId).HasDatabaseName("ClientIdIndex").IsUnique();
			b.ToTable("AspNetClients");
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();

			b.Property(c => c.ClientId).HasMaxLength(256).IsRequired();
			b.Property(c => c.ClientName).HasMaxLength(256);
			
			b.Property(c => c.ProtocolType).IsRequired();
		});
	}

	public static void ConfigureClientCorsOriginContext<TClientCorsOrigin, TKey>(this ModelBuilder builder)
		where TClientCorsOrigin : IdentityClientCorsOrigin<TKey, TKey>
		where TKey : IEquatable<TKey>
	{
		builder.Entity<TClientCorsOrigin>(b => {
			b.HasKey(c => c.Id);
			b.HasIndex(c => c.ClientId).HasDatabaseName("ClientIdIndex").IsUnique();
			b.ToTable("AspNetClientCorsOrigins");

			b.Property(c => c.Origin).HasMaxLength(150).IsRequired();
		});
	}

	public static void ConfigureScope<TScope, TKey>(this ModelBuilder builder)
		where TScope : IdentityScope<TKey>
		where TKey : IEquatable<TKey>
	{
		builder.Entity<TScope>(b => {
			b.HasKey(s => s.Id);
			b.HasIndex(s => s.Name).HasDatabaseName("ScopeIndex").IsUnique();
			b.ToTable("AspNetScopes");
			
			b.Property(s => s.Name).HasMaxLength(256).IsRequired();
		});
	}
}