// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Moq;

using Wangkanai.Domain.Configurations;

namespace Wangkanai.Domain.Extensions;

public class AuditContextExtensionsTests
{
	[Fact]
	public void ApplyAuditTrailConfiguration_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder   = new Mock<ModelBuilder>();
		var configuration = It.IsAny<IEntityTypeConfiguration<Audit<Guid, IdentityUser<Guid>, Guid>>>();
		mockBuilder.Setup(x => x.ApplyConfiguration(configuration))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<Guid, Guid, IdentityUser<Guid>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditConfiguration<Guid, Guid, IdentityUser<Guid>>>()), Times.Once);
	}

	[Fact]
	public void ApplyAuditTrailConfiguration_WithIntKeys_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder   = new Mock<ModelBuilder>();
		var configuration = It.IsAny<IEntityTypeConfiguration<Audit<int, IdentityUser<int>, int>>>();
		mockBuilder.Setup(x => x.ApplyConfiguration(configuration))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<int, int, IdentityUser<int>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditConfiguration<int, int, IdentityUser<int>>>()), Times.Once);
	}

	[Fact]
	public void ApplyAuditTrailConfiguration_WithDifferentKeyTypes_ShouldApplyConfiguration()
	{
		// Arrange
		var mockBuilder   = new Mock<ModelBuilder>();
		var configuration = It.IsAny<IEntityTypeConfiguration<Audit<Guid, IdentityUser<string>, string>>>();
		mockBuilder.Setup(x => x.ApplyConfiguration(configuration))
		           .Verifiable();

		// Act
		mockBuilder.Object.ApplyAuditTrailConfiguration<Guid, string, IdentityUser<string>>();

		// Assert
		mockBuilder.Verify(x => x.ApplyConfiguration(It.IsAny<AuditConfiguration<Guid, string, IdentityUser<string>>>()), Times.Once);
	}
}
