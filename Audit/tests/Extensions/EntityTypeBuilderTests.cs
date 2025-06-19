// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Audit;
using Wangkanai.Audit.Configurations;
using Wangkanai.Audit.Models;
using Wangkanai.Domain.Configurations;
using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Extensions;

public class EntityTypeBuilderTests
{
	[Fact]
	public void NewKeyOnAdd_Guid_ShouldHaveId()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<GuidEntity, GuidEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var id      = entity.FindProperty(nameof(GuidEntity.Id));

		// Act
		builder.NewGuidOnAdd();

		// Assert
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}

	[Fact]
	public void NewKeyOnAdd_Int_ShouldHaveId()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<IntEntity, IntEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var id      = entity.FindProperty(nameof(IntEntity.Id));

		// Act
		builder.NewKeyOnAdd();

		// Assert
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}

	[Fact]
	public void NewKeyOnAdd_Generic_ShouldHaveId()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<GuidEntity, GuidEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var id      = entity.FindProperty(nameof(GuidEntity.Id));

		// Act
		builder.NewKeyOnAdd<GuidEntity, Guid>();

		// Assert
		Assert.True(id!.ValueGenerated == ValueGenerated.OnAdd);
	}

	[Fact]
	public void HasDefaultCreated_ShouldConfigureCreatedProperty()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<CreatedEntity, CreatedEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var created = entity.FindProperty(nameof(CreatedEntity.Created));

		// Act
		builder.HasDefaultCreated();

		// Assert
		Assert.True(created!.ValueGenerated == ValueGenerated.OnAdd);
		// Assert.NotNull(created!.GetDefaultValue());
		// Assert.IsType<DateTime>(created!.GetDefaultValue());
	}

	[Fact]
	public void HasDefaultUpdated_ShouldConfigureUpdatedProperty()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<UpdatedEntity, UpdatedEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var updated = entity.FindProperty(nameof(UpdatedEntity.Updated));

		// Act
		builder.HasDefaultUpdated();

		// Assert
		Assert.True(updated!.ValueGenerated == ValueGenerated.OnUpdate);
		// Assert.NotNull(updated!.GetDefaultValue());
		// Assert.IsType<DateTime>(updated!.GetDefaultValue());
	}

	[Fact]
	public void HasDefaultCreatedAndUpdated_ShouldConfigureBothProperties()
	{
		// Arrange
		var builder = MockExtensions.GetEntityTypeBuilder<AuditEntity, AuditEntityTypeConfiguration>();
		var entity  = builder.Metadata;
		var created = entity.FindProperty(nameof(AuditEntity.Created));
		var updated = entity.FindProperty(nameof(AuditEntity.Updated));

		// Act
		builder.HasDefaultCreatedAndUpdated();

		// Assert
		Assert.True(created!.ValueGenerated == ValueGenerated.OnAdd);
		// Assert.NotNull(created!.GetDefaultValue());
		// Assert.IsType<DateTime>(created!.GetDefaultValue());

		Assert.True(updated!.ValueGenerated == ValueGenerated.OnAddOrUpdate);
		// Assert.NotNull(updated!.GetDefaultValue());
		// Assert.IsType<DateTime>(updated!.GetDefaultValue());
	}
}
