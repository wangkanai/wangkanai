// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Wangkanai.Federation.Entities;

namespace Wangkanai.Federation.EntityFramework;

public class FederationDbContext : FederationDbContext<IdentityUser, IdentityRole, Guid>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public class FederationDbContext<TUser> : FederationDbContext<TUser, IdentityRole, Guid>
	where TUser : IdentityUser
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public class FederationDbContext<TUser, TRole, TKey>
	: FederationDbContext<TUser, TRole,
		IdentityClient<TKey>,
		IdentityScope<TKey>,
		IdentityResource<TKey>,
		IdentityDirectory<TKey>,
		IdentityGroup<TKey>,
		TKey>
	where TUser : IdentityUser
	where TRole : IdentityRole
	where TKey : IEquatable<TKey>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }
}

public abstract class FederationDbContext<TUser, TRole, TClient, TScope, TResource, TDirectory, TGroup, TKey>
	: IdentityDbContext<TUser, TRole, string>
	where TUser : IdentityUser
	where TRole : IdentityRole
	where TClient : IdentityClient<TKey>
	where TScope : IdentityScope<TKey>
	where TResource : IdentityResource<TKey>
	where TDirectory : IdentityDirectory<TKey>
	where TGroup : IdentityGroup<TKey>
	where TKey : IEquatable<TKey>
{
	public FederationDbContext(DbContextOptions options) : base(options) { }

	protected FederationDbContext() { }

	public virtual DbSet<TClient>    Clients     { get; set; } = default!;
	public virtual DbSet<TGroup>     Groups      { get; set; } = default!;
	public virtual DbSet<TScope>     Scopes      { get; set; } = default!;
	public virtual DbSet<TResource>  Resources   { get; set; } = default!;
	public virtual DbSet<TDirectory> Directories { get; set; } = default!;

	public virtual DbSet<IdentityClientOrigin> ClientCorsOrigins { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		var configOptions = GetConfigurationOptions();
		var maxKeyLength  = configOptions?.MaxLengthForKeys ?? 0;
		var encryptData   = configOptions?.EncryptData      ?? false;

		builder.Entity<TClient>(b => {
			b.ToTable(configOptions.Client);
			b.HasKey(c => c.Id);
			b.HasIndex(c => c.ClientId).HasDatabaseName("ClientIdIndex").IsUnique();

			b.Property(c => c.ClientId).HasMaxLength(256).IsRequired();
			b.Property(c => c.Name).HasMaxLength(256);
			b.Property(c => c.ProtocolType).IsRequired();
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
		});

		builder.Entity<IdentityClientOrigin>(b => {
			b.ToTable(configOptions.ClientOrigin);
			b.HasKey(c => c.Id);
			b.Property(c => c.Origin).HasMaxLength(150).IsRequired();
			b.HasIndex(c => new { c.ClientId, c.Origin }).IsUnique();

			b.HasOne(x => x.Client).WithMany(x => x.Origins).IsRequired();
		});

		builder.Entity<IdentityClientFlowType>(b => {
			b.ToTable(configOptions.ClientFlowType);
			b.HasKey(c => c.Id);

			b.HasOne(c => c.Client).WithMany(c => c.FlowTypes).IsRequired();
		});

		builder.Entity<IdentityClientRedirectUri>(b => {
			b.ToTable(configOptions.ClientRedirectUri);
			b.HasKey(c => c.Id);

			b.HasOne(c => c.Client).WithMany(x => x.RedirectUris).IsRequired();
		});

		builder.Entity<TScope>(b => {
			b.ToTable(configOptions.Scope);
			b.HasKey(s => s.Id);
			b.HasIndex(s => s.Name).HasDatabaseName("ScopeIndex").IsUnique();

			b.Property(s => s.Name).HasMaxLength(256).IsRequired();
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
		});

		builder.Entity<TResource>(b => {
			b.ToTable(configOptions.Resource);
			b.HasKey(r => r.Id);
			b.HasIndex(r => r.Name).HasDatabaseName("ResourceIndex").IsUnique();

			b.Property(r => r.Name).HasMaxLength(256).IsRequired();
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
		});

		builder.Entity<TDirectory>(b => {
			b.ToTable(configOptions.Directory);
			b.HasKey(d => d.Id);
			b.HasIndex(d => d.Name).HasDatabaseName("DirectoryIndex").IsUnique();

			b.Property(d => d.Name).HasMaxLength(256).IsRequired();
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
		});

		builder.Entity<TGroup>(b => {
			b.ToTable(configOptions.Group);
			b.HasKey(g => g.Id);
			b.HasIndex(g => g.Name).HasDatabaseName("GroupIndex").IsUnique();

			b.Property(g => g.Name).HasMaxLength(256).IsRequired();
			b.Property(c => c.ConcurrencyStamp).IsConcurrencyToken();
		});

		base.OnModelCreating(builder);
	}

	private ConfigurationOptions? GetConfigurationOptions()
		=> this.GetService<IDbContextOptions>()
		       .Extensions.OfType<CoreOptionsExtension>()
		       .FirstOrDefault()
		       ?.ApplicationServiceProvider?.GetService<IOptions<FederationOptions>>()
		       ?.Value?.Stores;

	private sealed class FederationDataConverter : ValueConverter<string, string>
	{
		public FederationDataConverter(IPersonalDataProtector protector)
			: base(x => protector.Protect(x), x => protector.Unprotect(x), default) { }
	}
}