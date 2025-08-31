// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Audit.Models;

namespace Wangkanai.Domain.Configurations;

public class CreatedEntityTypeConfiguration : IEntityTypeConfiguration<CreatedEntity>
{
   public void Configure(EntityTypeBuilder<CreatedEntity> builder) => builder.HasKey(x => x.Id);
}