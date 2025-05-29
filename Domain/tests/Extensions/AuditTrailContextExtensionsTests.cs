// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Moq;

using Wangkanai.Domain.Configurations;

namespace Wangkanai.Domain.Extensions;

public class AuditTrailContextExtensionsTests
{
	[Fact]
	public void ApplyAuditTrailConfiguration_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder = new Mock<ModelBuilder>();
		mockBuilder.Setup(x => x.ApplyConfiguration(It.IsAny<IEntityTypeConfiguration<AuditTrail<Guid, IdentityUser<Guid>, Guid>>>()))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<Guid, Guid, IdentityUser<Guid>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditTrailConfiguration<Guid, Guid, IdentityUser<Guid>>>()), Times.Once);
	}

	[Fact]
	public void ApplyAuditTrailConfiguration_WithIntKeys_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder = new Mock<ModelBuilder>();
		mockBuilder.Setup(x => x.ApplyConfiguration(It.IsAny<IEntityTypeConfiguration<AuditTrail<int, IdentityUser<int>, int>>>()))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<int, int, IdentityUser<int>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditTrailConfiguration<int, int, IdentityUser<int>>>()), Times.Once);
	}

	[Fact]
	public void ApplyAuditTrailConfiguration_WithDifferentKeyTypes_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder = new Mock<ModelBuilder>();
		mockBuilder.Setup(x => x.ApplyConfiguration(It.IsAny<IEntityTypeConfiguration<AuditTrail<Guid, IdentityUser<string>, string>>>()))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<Guid, string, IdentityUser<string>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditTrailConfiguration<Guid, string, IdentityUser<string>>>()), Times.Once);
	}
}
