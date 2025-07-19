// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Audit.Models;

namespace Wangkanai.Audit.Configurations;

public class GuidEntityTypeConfiguration : IEntityTypeConfiguration<GuidEntity>
{
	public void Configure(EntityTypeBuilder<GuidEntity> builder)
	{
		builder.HasKey(c => c.Id);
	}
}
