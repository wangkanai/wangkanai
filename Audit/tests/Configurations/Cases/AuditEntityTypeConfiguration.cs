// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Configurations;

public class AuditEntityTypeConfiguration : IEntityTypeConfiguration<AuditEntity>
{
	public void Configure(EntityTypeBuilder<AuditEntity> builder)
	{
		builder.HasKey(x => x.Id);
	}
}
