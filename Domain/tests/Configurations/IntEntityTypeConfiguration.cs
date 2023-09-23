// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Configurations;

public class IntEntityTypeConfiguration : IEntityTypeConfiguration<IntEntity>
{
	public void Configure(EntityTypeBuilder<IntEntity> builder)
	{
		builder.HasKey(c => c.Id);
	}
}