// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;

namespace Wangkanai.Audit.Tests;

public class AuditTests
{
    [Fact]
    public void Audit_ChangedColumns_ShouldBeInitialized()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(audit.ChangedColumns);
        Assert.Empty(audit.ChangedColumns);
    }

    [Fact]
    public void Audit_OldValues_ShouldBeInitialized()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(audit.OldValues);
        Assert.Empty(audit.OldValues);
    }

    [Fact]
    public void Audit_NewValues_ShouldBeInitialized()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(audit.NewValues);
        Assert.Empty(audit.NewValues);
    }

    [Fact]
    public void Audit_SetTrailType_ShouldRetainValue()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            TrailType = TrailType.Create
        };

        // Act & Assert
        Assert.Equal(TrailType.Create, audit.TrailType);
    }

    [Fact]
    public void Audit_SetUserId_ShouldRetainValue()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            UserId = 1
        };

        // Act & Assert
        Assert.Equal(1, audit.UserId);
    }

    [Fact]
    public void Audit_SetUser_ShouldRetainValue()
    {
        // Arrange
        var user = new IdentityUser<int> { Id = 1, UserName = "TestUser" };
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            User = user
        };

        // Act & Assert
        Assert.NotNull(audit.User);
        Assert.Equal("TestUser", audit.User.UserName);
    }

    [Fact]
    public void Audit_SetTimestamp_ShouldRetainValue()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.Equal(timestamp, audit.Timestamp);
    }

    [Fact]
    public void Audit_SetPrimaryKey_ShouldRetainValue()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            PrimaryKey = "123"
        };

        // Act & Assert
        Assert.Equal("123", audit.PrimaryKey);
    }

    [Fact]
    public void Audit_SetEntityName_ShouldRetainValue()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            EntityName = "TestEntity"
        };

        // Act & Assert
        Assert.Equal("TestEntity", audit.EntityName);
    }

    [Fact]
    public void Audit_SetChangedColumns_ShouldRetainValues()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            ChangedColumns = ["Column1", "Column2"]
        };

        // Act & Assert
        Assert.NotEmpty(audit.ChangedColumns);
        Assert.Contains("Column1", audit.ChangedColumns);
        Assert.Contains("Column2", audit.ChangedColumns);
        Assert.Equal(2, audit.ChangedColumns.Count);
    }

    [Fact]
    public void Audit_SetOldValues_ShouldRetainValues()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            OldValues = new Dictionary<string, object> { { "Column1", "OldValue1" } }
        };

        // Act & Assert
        Assert.NotEmpty(audit.OldValues);
        Assert.Equal("OldValue1", audit.OldValues["Column1"]);
        Assert.Single(audit.OldValues);
    }

    [Fact]
    public void Audit_SetNewValues_ShouldRetainValues()
    {
        // Arrange
        var audit = new Audit<int, IdentityUser<int>, int>
        {
            NewValues = new Dictionary<string, object> { { "Column1", "NewValue1" } }
        };

        // Act & Assert
        Assert.NotEmpty(audit.NewValues);
        Assert.Equal("NewValue1", audit.NewValues["Column1"]);
        Assert.Single(audit.NewValues);
    }
}
