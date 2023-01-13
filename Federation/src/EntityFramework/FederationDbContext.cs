// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Identity;

namespace Wangkanai.Federation.EntityFramework;

public class FederationDbContext : FederationDbContext<IdentityUser, IdentityRole, string>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public class FederationDbContext<TUser> : FederationDbContext<TUser, IdentityRole, string>
	where TUser : IdentityUser
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public class FederationDbContext<TUser, TRole, TKey> : IdentityDbContext<TUser, TRole, TKey>
	where TUser : IdentityUser<TKey>
	where TRole : IdentityRole<TKey>
	where TKey : IEquatable<TKey>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public abstract class FederationDbContext<TUser, TRole, TKey, TClient, TClientOrigin, TScope, TResource, TDirectory>
	: IdentityDbContext<TUser, TRole, TKey>
	where TUser : IdentityUser<TKey>
	where TRole : IdentityRole<TKey>
	where TKey : IEquatable<TKey>
	where TClient : IdentityClient<TKey>
	where TClientOrigin : IdentityClientCorsOrigin<TKey, string>
	where TScope : IdentityScope<TKey>
	where TResource : IdentityResource<TKey>
	where TDirectory : IdentityDirectory<TKey>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }

	public virtual DbSet<TClient>       Clients       { get; set; } = default!;
	public virtual DbSet<TClientOrigin> ClientOrigins { get; set; } = default!;
	public virtual DbSet<TScope>        Scopes        { get; set; } = default!;
	public virtual DbSet<TResource>     Resources     { get; set; } = default!;
	public virtual DbSet<TDirectory>    Directories   { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		var                     storeOptions = GetStoreOptions();
		var                     maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
		var                     encryptData  = storeOptions?.EncryptData ?? false;
		FederationDataConverter converter    = null;

		builder.Entity<TClient>(b => {
			b.HasKey(c => c.Id);
			b.HasIndex(c => c.ClientId).HasDatabaseName("ClientIdIndex").IsUnique();
			b.ToTable("AspNetClients");

			b.Property(c => c.ClientId).HasMaxLength(256).IsRequired();
		});

		builder.Entity<TScope>(b => {
			b.HasKey(s => s.Id);
			b.HasIndex(s => s.Name).HasDatabaseName("ScopeIndex").IsUnique();
		});
	}

	private OperationStoreOptions? GetStoreOptions()
	{
		return this.GetService<IDbContextOptions>()
		           .Extensions.OfType<CoreOptionsExtension>()
		           .FirstOrDefault()
		           ?.ApplicationServiceProvider
		           ?.GetService<IOptions<FederationOptions>>()
		           ?.Value?.Stores;
	}

	private sealed class FederationDataConverter : ValueConverter<string, string>
	{
		public FederationDataConverter(IPersonalDataProtector protector)
			: base(x => protector.Protect(x), x => protector.Unprotect(x), default) { }
	}
}