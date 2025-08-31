// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Audit.Models;

namespace Wangkanai.Domain.Configurations;

public class IntEntityTypeConfiguration : IEntityTypeConfiguration<IntEntity>
{
   public void Configure(EntityTypeBuilder<IntEntity> builder) => builder.HasKey(c => c.Id);
}