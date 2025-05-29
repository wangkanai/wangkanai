// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Moq;

using Wangkanai.Domain.Primitives;
using Wangkanai.Domain.Stores;

namespace Wangkanai.Domain.Tests.Stores;

public class AuditStoreTests
{
	private class TestUser : IdentityUser<Guid>
	{
		public TestUser(string userName) : base(userName) { }
	}

	private class TestAudit : Audit<Guid, TestUser, Guid>
	{
		public TestAudit()
		{
			Id = Guid.NewGuid();
		}
	}

	private class TestDbContext : DbContext
	{
		public TestDbContext(DbContextOptions<TestDbContext> options)
			: base(options) { }

		public DbSet<TestAudit> Audits { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyAuditConfiguration<int, IdentityUser<int>, int>();
		}
	}

	private DbContextOptions<TestDbContext> CreateNewContextOptions()
	{
		return new DbContextOptionsBuilder<TestDbContext>()
		       .UseInMemoryDatabase(databaseName: $"AuditStoreTest_{Guid.NewGuid()}")
		       .Options;
	}

	[Fact]
	public void Constructor_WithNullContext_ThrowsException()
	{
		// Arrange & Act & Assert
		Assert.Throws<ArgumentNullException>(() => new AuditStore<TestDbContext, Guid, TestUser, Guid>(null!));
	}

	[Fact]
	public void Audits_ReturnsQueryableCollection()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		// Act
		var audits = store.Audits;

		// Assert
		Assert.NotNull(audits);
		Assert.IsAssignableFrom<IQueryable<Audit<Guid, TestUser, Guid>>>(audits);
	}

	[Fact]
	public async Task CreateAsync_SavesToDbContext()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);
		var       audit   = new TestAudit();

		// Act
		var result = await store.CreateAsync(audit, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(audit, result.Value);
		Assert.Equal(1, await context.Audits.CountAsync());
		Assert.Equal(audit.Id, (await context.Audits.FirstOrDefaultAsync())?.Id);
	}

	[Fact]
	public async Task CreateAsync_WithoutAutoSave_DoesNotSaveChanges()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var store = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context)
		            {
			            AutoSaveChanges = false
		            };
		var audit = new TestAudit();

		// Act
		var result = await store.CreateAsync(audit, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(0, await context.Audits.CountAsync());
	}

	[Fact]
	public async Task CreateAsync_WithConcurrencyException_ReturnsError()
	{
		// Arrange
		var mockContext = new Mock<TestDbContext>(CreateNewContextOptions());
		mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
		           .ThrowsAsync(new DbUpdateConcurrencyException());

		var store = new AuditStore<TestDbContext, Guid, TestUser, Guid>(mockContext.Object);
		var audit = new TestAudit();

		// Act
		var result = await store.CreateAsync(audit, CancellationToken.None);

		// Assert
		Assert.False(result.IsSuccess);
		Assert.Equal(ErrorCodes.Concurrency, result.Error?.Code);
	}

	[Fact]
	public async Task UpdateAsync_UpdatesExistingEntity()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var audit = new TestAudit();
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Clear tracker to simulate detached entity
		context.ChangeTracker.Clear();

		// Act
		audit.TrailType = TrailType.Create;
		var result = await store.UpdateAsync(audit, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		var updatedAudit = await context.Audits.FindAsync(audit.Id);
		Assert.Equal(TrailType.Create, updatedAudit?.TrailType);
	}

	[Fact]
	public async Task UpdateAsync_WithConcurrencyException_ReturnsError()
	{
		// Arrange
		var mockContext = new Mock<TestDbContext>(CreateNewContextOptions());
		mockContext.Setup(c => c.Attach(It.IsAny<TestAudit>())).Returns((TestAudit e) => mockContext.Object.Entry(e));
		mockContext.Setup(c => c.Update(It.IsAny<TestAudit>()));
		mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
		           .ThrowsAsync(new DbUpdateConcurrencyException());

		var store = new AuditStore<TestDbContext, Guid, TestUser, Guid>(mockContext.Object);
		var audit = new TestAudit();

		// Act
		var result = await store.UpdateAsync(audit, CancellationToken.None);

		// Assert
		Assert.False(result.IsSuccess);
		Assert.Equal(ErrorCodes.Concurrency, result.Error?.Code);
	}

	[Fact]
	public async Task DeleteAsync_RemovesEntityFromContext()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var audit = new TestAudit();
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.DeleteAsync(audit, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(0, await context.Audits.CountAsync());
	}

	[Fact]
	public async Task DeleteAsync_WithConcurrencyException_ReturnsError()
	{
		// Arrange
		var mockContext = new Mock<TestDbContext>(CreateNewContextOptions());
		mockContext.Setup(c => c.Remove(It.IsAny<TestAudit>()));
		mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
		           .ThrowsAsync(new DbUpdateConcurrencyException());

		var store = new AuditStore<TestDbContext, Guid, TestUser, Guid>(mockContext.Object);
		var audit = new TestAudit();

		// Act
		var result = await store.DeleteAsync(audit, CancellationToken.None);

		// Assert
		Assert.False(result.IsSuccess);
		Assert.Equal(ErrorCodes.Concurrency, result.Error?.Code);
	}

	[Fact]
	public async Task FindByIdAsync_ReturnsMatchingEntity()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var audit = new TestAudit();
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByIdAsync(audit.Id, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
		Assert.Equal(audit.Id, result.Value!.Id);
	}

	[Fact]
	public async Task FindByIdAsync_WithNonexistentId_ReturnsNull()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		// Act
		var result = await store.FindByIdAsync(Guid.NewGuid(), CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Null(result.Value);
	}

	[Fact]
	public async Task FindByIdAsync_WithIdAndUserId_ReturnsMatchingEntity()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var userId = Guid.NewGuid();
		var audit  = new TestAudit { UserId = userId };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByIdAsync(audit.Id, userId, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
		Assert.Equal(audit.Id, result.Value!.Id);
	}

	[Fact]
	public async Task FindByIdAsync_WithNonMatchingUserId_ReturnsNull()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var audit = new TestAudit { UserId = Guid.NewGuid() };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByIdAsync(audit.Id, Guid.NewGuid(), CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Null(result.Value);
	}

	[Fact]
	public async Task FindByUserIdAsync_ReturnsMatchingEntity()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var userId = Guid.NewGuid();
		var audit  = new TestAudit { UserId = userId };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByUserIdAsync(userId, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
		Assert.Equal(userId, result.Value!.UserId);
	}

	[Fact]
	public async Task FindByUserIdAsync_WithNonexistentUserId_ReturnsNull()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var audit = new TestAudit { UserId = Guid.NewGuid() };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByUserIdAsync(Guid.NewGuid(), CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Null(result.Value);
	}

	[Fact]
	public async Task FindByUserIdAsync_WithUserIdAndId_ReturnsMatchingEntity()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var userId = Guid.NewGuid();
		var audit  = new TestAudit { UserId = userId };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByUserIdAsync(userId, audit.Id, CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.NotNull(result.Value);
		Assert.Equal(audit.Id, result.Value!.Id);
		Assert.Equal(userId, result.Value!.UserId);
	}

	[Fact]
	public async Task FindByUserIdAsync_WithNonMatchingId_ReturnsNull()
	{
		// Arrange
		var       options = CreateNewContextOptions();
		using var context = new TestDbContext(options);
		var       store   = new AuditStore<TestDbContext, Guid, TestUser, Guid>(context);

		var userId = Guid.NewGuid();
		var audit  = new TestAudit { UserId = userId };
		context.Audits.Add(audit);
		await context.SaveChangesAsync();

		// Act
		var result = await store.FindByUserIdAsync(userId, Guid.NewGuid(), CancellationToken.None);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Null(result.Value);
	}

	[Fact]
	public void Dispose_ReleasesResources()
	{
		// Arrange
		var mockContext = new Mock<TestDbContext>(CreateNewContextOptions());
		var store       = new AuditStore<TestDbContext, Guid, TestUser, Guid>(mockContext.Object);

		// Act
		store.Dispose();
		store.Dispose(); // Should be safe to call multiple times

		// Assert
		mockContext.Verify(m => m.Dispose(), Times.Once);
	}
}
