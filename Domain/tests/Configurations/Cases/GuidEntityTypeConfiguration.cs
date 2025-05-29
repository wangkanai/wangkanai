// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Configurations;

public class GuidEntityTypeConfiguration : IEntityTypeConfiguration<GuidEntity>
{
	public void Configure(EntityTypeBuilder<GuidEntity> builder)
	{
		builder.HasKey(c => c.Id);
	}
}