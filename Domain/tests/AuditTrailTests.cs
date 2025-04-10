using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Wangkanai.Domain.Tests;

public class AuditTrailTests
{
    [Fact]
    public void AuditTrail_ChangedColumns_ShouldBeInitialized()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(auditTrail.ChangedColumns);
        Assert.Empty(auditTrail.ChangedColumns);
    }

    [Fact]
    public void AuditTrail_OldValues_ShouldBeInitialized()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(auditTrail.OldValues);
        Assert.Empty(auditTrail.OldValues);
    }

    [Fact]
    public void AuditTrail_NewValues_ShouldBeInitialized()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(auditTrail.NewValues);
        Assert.Empty(auditTrail.NewValues);
    }

    [Fact]
    public void AuditTrail_SetTrailType_ShouldRetainValue()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            TrailType = TrailType.Create
        };

        // Act & Assert
        Assert.Equal(TrailType.Create, auditTrail.TrailType);
    }

    [Fact]
    public void AuditTrail_SetUserId_ShouldRetainValue()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            UserId = 1
        };

        // Act & Assert
        Assert.Equal(1, auditTrail.UserId);
    }

    [Fact]
    public void AuditTrail_SetUser_ShouldRetainValue()
    {
        // Arrange
        var user = new IdentityUser<int> { Id = 1, UserName = "TestUser" };
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            User = user
        };

        // Act & Assert
        Assert.NotNull(auditTrail.User);
        Assert.Equal("TestUser", auditTrail.User.UserName);
    }

    [Fact]
    public void AuditTrail_SetTimestamp_ShouldRetainValue()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.Equal(timestamp, auditTrail.Timestamp);
    }

    [Fact]
    public void AuditTrail_SetPrimaryKey_ShouldRetainValue()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            PrimaryKey = "123"
        };

        // Act & Assert
        Assert.Equal("123", auditTrail.PrimaryKey);
    }

    [Fact]
    public void AuditTrail_SetEntityName_ShouldRetainValue()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            EntityName = "TestEntity"
        };

        // Act & Assert
        Assert.Equal("TestEntity", auditTrail.EntityName);
    }

    [Fact]
    public void AuditTrail_SetChangedColumns_ShouldRetainValues()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            ChangedColumns = ["Column1", "Column2"]
        };

        // Act & Assert
        Assert.NotEmpty(auditTrail.ChangedColumns);
        Assert.Contains("Column1", auditTrail.ChangedColumns);
        Assert.Contains("Column2", auditTrail.ChangedColumns);
        Assert.Equal(2, auditTrail.ChangedColumns.Count);
    }

    [Fact]
    public void AuditTrail_SetOldValues_ShouldRetainValues()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            OldValues = new Dictionary<string, object> { { "Column1", "OldValue1" } }
        };

        // Act & Assert
        Assert.NotEmpty(auditTrail.OldValues);
        Assert.Equal("OldValue1", auditTrail.OldValues["Column1"]);
        Assert.Single(auditTrail.OldValues);
    }

    [Fact]
    public void AuditTrail_SetNewValues_ShouldRetainValues()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            NewValues = new Dictionary<string, object> { { "Column1", "NewValue1" } }
        };

        // Act & Assert
        Assert.NotEmpty(auditTrail.NewValues);
        Assert.Equal("NewValue1", auditTrail.NewValues["Column1"]);
        Assert.Single(auditTrail.NewValues);
    }
}
