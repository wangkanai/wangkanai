// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain.Configurations;
using Wangkanai.Domain.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wangkanai.Domain.Extensions;

public class EntityTypeBuilderTests
{
	[Fact]
	public void NewKeyOnAdd_Guid_ShouldHaveId()
	{
		var builder = MockExtensions.GetEntityTypeBuilder<GuidEntity, GuidEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var id      = entity.FindProperty(nameof(GuidEntity.Id));
		
		builder.NewGuidOnAdd();
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}

	[Fact]
	public void NewKeyOnAdd_Int_ShouldHaveId()
	{
		var builder = MockExtensions.GetEntityTypeBuilder<IntEntity, IntEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var id      = entity.FindProperty(nameof(GuidEntity.Id));
		
		builder.NewKeyOnAdd();
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}
}