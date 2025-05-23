// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wangkanai.Domain.Configurations;

public class AuditTrailConfiguration<TKey, TUserKey, TUserType>
	: IEntityTypeConfiguration<AuditTrail<TKey, TUserType, TUserKey>>
	where TKey : IEquatable<TKey>, IComparable<TKey>
	where TUserType : IdentityUser<TUserKey>
	where TUserKey : IEquatable<TUserKey>, IComparable<TUserKey>
{
	public void Configure(EntityTypeBuilder<AuditTrail<TKey, TUserType, TUserKey>> builder)
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
		       .HasConversion(to => to, value => DateTime.SpecifyKind(value, DateTimeKind.Utc));

		builder.Property(x => x.TrailType)
		       .HasConversion<string>();

		builder.Property(x => x.EntityName)
		       .IsRequired();

		builder.Property(x => x.ChangedColumns)
		       .HasColumnType("jsonb");

		builder.Property(x => x.OldValues)
		       .HasColumnType("jsonb");

		builder.Property(x => x.NewValues)
		       .HasColumnType("jsonb");

		builder.Property(x => x.UserId);
		builder.HasOne(x => x.User)
		       .WithMany()
		       .HasForeignKey(x => x.UserId)
		       .IsRequired(false)
		       .OnDelete(DeleteBehavior.SetNull);
	}
}
