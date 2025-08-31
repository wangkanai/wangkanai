// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wangkanai.Nation.Urbans;

public sealed class UrbanConfiguration : IEntityTypeConfiguration<Urban>
{
   public void Configure(EntityTypeBuilder<Urban> builder)
   {
      builder.HasIndex(u => u.DivisionId);
      builder.Property(u => u.DivisionId)
             .IsRequired();

      builder.Property(u => u.Name)
             .HasMaxLength(100)
             .IsRequired();

      builder.Property(u => u.Native)
             .HasMaxLength(100)
             .IsUnicode()
             .IsRequired();

      builder.HasIndex(u => u.Iso);
      builder.Property(u => u.Iso)
             .HasMaxLength(5)
             .IsRequired();
   }
}