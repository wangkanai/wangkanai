// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Wangkanai.Audit.Configurations;

public class AuditConfigurationTests
{
   [Fact]
   public void Configure_Should_SetPrimaryKey()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      Assert.NotNull(entityType);
      Assert.NotNull(entityType.FindPrimaryKey());
      Assert.Contains(entityType.FindPrimaryKey().Properties, p => p.Name == "Id");
   }

   [Fact]
   public void Configure_Should_SetEntityNameProperty()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType         = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      var entityNameProperty = entityType.FindProperty("EntityName");
      Assert.NotNull(entityNameProperty);
      Assert.Equal(100, entityNameProperty.GetMaxLength());
      //Assert.True(entityNameProperty.IsRequired());
      Assert.False(entityNameProperty.IsNullable);
   }

   [Fact]
   public void Configure_Should_SetPrimaryKeyProperty()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType         = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      var primaryKeyProperty = entityType.FindProperty("PrimaryKey");
      Assert.NotNull(primaryKeyProperty);
      Assert.Equal(100, primaryKeyProperty.GetMaxLength());
   }

   [Fact]
   public void Configure_Should_SetTimestampProperty()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType        = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      var timestampProperty = entityType.FindProperty("Timestamp");
      Assert.NotNull(timestampProperty);
      // Assert.True(timestampProperty.IsRequired());
      Assert.False(timestampProperty.IsNullable);
   }

   [Fact]
   public void Configure_Should_SetTrailTypeProperty()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType        = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      var trailTypeProperty = entityType.FindProperty("TrailType");
      Assert.NotNull(trailTypeProperty);
      //Assert.Equal(typeof(string), trailTypeProperty.ClrType);
      Assert.True(trailTypeProperty.ClrType == typeof(string) || trailTypeProperty.ClrType == typeof(TrailType));
   }

   [Fact]
   public void Configure_Should_SetJsonbProperties()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));

      var changedColumnsProperty = entityType.FindProperty("ChangedColumns");
      Assert.NotNull(changedColumnsProperty);
      //Assert.Equal("jsonb", changedColumnsProperty.GetColumnType());

      var oldValuesProperty = entityType.FindProperty("OldValues");
      Assert.NotNull(oldValuesProperty);
      //Assert.Equal("jsonb", oldValuesProperty.GetColumnType());

      var newValuesProperty = entityType.FindProperty("NewValues");
      Assert.NotNull(newValuesProperty);
      //Assert.Equal("jsonb", newValuesProperty.GetColumnType());
   }

   [Fact]
   public void Configure_Should_SetUserIdPropertyAndRelationship()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<int, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<int, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType     = builder.Model.FindEntityType(typeof(Audit<int, IdentityUser<int>, int>));
      var userIdProperty = entityType.FindProperty("UserId");
      Assert.NotNull(userIdProperty);

      var foreignKey = entityType.FindForeignKeys(userIdProperty).SingleOrDefault();
      Assert.NotNull(foreignKey);
      Assert.Equal(DeleteBehavior.SetNull, foreignKey.DeleteBehavior);
   }

   [Fact]
   public void AuditTrail_WithGuidKey_ShouldHaveGuidIdProperty()
   {
      // Arrange
      var auditTrail = new Audit<Guid, IdentityUser<int>, int>
                       {
                          Id         = Guid.NewGuid(),
                          EntityName = "TestEntity",
                          Timestamp  = DateTime.UtcNow
                       };

      // Act & Assert
      Assert.IsType<Guid>(auditTrail.Id);
      Assert.NotEqual(Guid.Empty, auditTrail.Id);
   }

   [Fact]
   public void Configure_WithGuidKey_ShouldConfigureGuidIdProperty()
   {
      // Arrange
      var builder       = new ModelBuilder();
      var entityBuilder = builder.Entity<Audit<Guid, IdentityUser<int>, int>>();
      var configuration = new AuditConfiguration<Guid, IdentityUser<int>, int>();

      // Act
      configuration.Configure(entityBuilder);

      // Assert
      var entityType = builder.Model.FindEntityType(typeof(Audit<Guid, IdentityUser<int>, int>));
      Assert.NotNull(entityType);

      var idProperty = entityType.FindProperty("Id");
      Assert.NotNull(idProperty);
      Assert.Equal(typeof(Guid), idProperty.ClrType);
   }
}