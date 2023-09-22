// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Configurations;
using Wangkanai.Domain.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wangkanai.Domain.Extensions;

public class EntityTypeBuilderTests
{
	[Fact]
	public void NewKeyOnAdd_ShouldHaveId()
	{
		var builder = MockExtensions.GetEntityTypeBuilder<GuidEntity, GuidEntityTypeConfiguration>();
		builder.NewKeyOnAdd();
		var entity = builder.Metadata;
		var id     = entity.FindProperty(nameof(GuidEntity.Id));
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}
}