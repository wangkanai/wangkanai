// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Domain;

namespace Wangkanai.Audit.Configurations;

/// <summary>
/// Provides the configuration for the <see cref="Audit{TKey, TUserType, TUserKey}"/> entity type.
/// This class is responsible for defining the model and entity relationship mappings for use with Entity Framework.
/// </summary>
/// <typeparam name="TKey">The type of the primary key for the audit entity. Must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
/// <typeparam name="TUserType">The user type associated with the audit entity. Must derive from <see cref="IdentityUser"/>.</typeparam>
/// <typeparam name="TUserKey">The type of the primary key for the associated user. Must implement <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/>.</typeparam>
public class AuditConfiguration<TKey, TUserType, TUserKey>
	: IEntityTypeConfiguration<Audit<TKey, TUserType, TUserKey>>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	/// <summary>Configures the entity type for the <see cref="Audit{TKey, TUserType, TUserKey}"/> class.</summary>
	/// <param name="builder">An object that provides a simple API for configuring an entity type.</param>
	public void Configure(EntityTypeBuilder<Audit<TKey, TUserType, TUserKey>> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id);

		builder.HasIndex(x => x.EntityName);
		builder.Property(x => x.EntityName)
			   .HasMaxLength(100)
			   .IsRequired();

		builder.Property(x => x.PrimaryKey)
			   .HasMaxLength(100);

		builder.Property(x => x.Timestamp)
			   .IsRequired()
			   .HasConversion(to => DateTime.SpecifyKind(to, DateTimeKind.Utc), value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

		builder.Property(x => x.TrailType)
			   .HasConversion<string>();

		builder.Property(x => x.EntityName)
			   .IsRequired();

		builder.Ignore(x => x.ChangedColumns)
			   .UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Property(x => x.ChangedColumns)
			   .HasColumnType("jsonb")
			   .HasConversion(
				   c => JsonSerializer.Serialize(c, (JsonSerializerOptions?)null),
				   c => JsonSerializer.Deserialize<List<string>>(c, (JsonSerializerOptions?)null)
			   );

		builder.Ignore(x => x.OldValues)
			   .UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Property(x => x.OldValues)
			   .HasColumnType("jsonb")
			   .HasConversion(
				   c => JsonSerializer.Serialize(c, (JsonSerializerOptions?)null),
				   c => JsonSerializer.Deserialize<Dictionary<string, object?>>(c, (JsonSerializerOptions?)null)
			   );

		builder.Ignore(x => x.NewValues)
			   .UsePropertyAccessMode(PropertyAccessMode.Field);
		builder.Property(x => x.NewValues)
			   .HasColumnType("jsonb")
			   .HasConversion(
				   c => JsonSerializer.Serialize(c, (JsonSerializerOptions?)null),
				   c => JsonSerializer.Deserialize<Dictionary<string, object?>>(c, (JsonSerializerOptions?)null)
			   );

		builder.Property(x => x.UserId);
		builder.HasOne(x => x.User)
			   .WithMany()
			   .HasForeignKey(x => x.UserId)
			   .IsRequired(false)
			   .OnDelete(DeleteBehavior.SetNull);
	}
}
