using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Wangkanai.Domain.Tests;

public class AuditTrailTests
{
    [Fact]
    public void AuditTrail_DefaultValues_ShouldBeInitialized()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>();

        // Act & Assert
        Assert.NotNull(auditTrail.ChangedColumns);
        Assert.Empty(auditTrail.ChangedColumns);
        Assert.NotNull(auditTrail.OldValues);
        Assert.Empty(auditTrail.OldValues);
        Assert.NotNull(auditTrail.NewValues);
        Assert.Empty(auditTrail.NewValues);
    }

    [Fact]
    public void AuditTrail_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var auditTrail = new AuditTrail<int, IdentityUser<int>, int>
        {
            Type           = TrailType.Create,
            UserId         = 1,
            User           = new IdentityUser<int> { Id = 1, UserName = "TestUser" },
            Timestamp      = DateTime.UtcNow,
            PrimaryKey     = "123",
            EntityName     = "TestEntity",
            ChangedColumns = ["Column1", "Column2"],
            OldValues      = new Dictionary<string, object> { { "Column1", "OldValue1" } },
            NewValues      = new Dictionary<string, object> { { "Column1", "NewValue1" } }
        };

        // Act & Assert
        Assert.Equal(TrailType.Create, auditTrail.Type);
        Assert.Equal(1, auditTrail.UserId);
        Assert.NotNull(auditTrail.User);
        Assert.Equal("TestUser", auditTrail.User.UserName);
        Assert.NotEqual(default, auditTrail.Timestamp);
        Assert.Equal("123", auditTrail.PrimaryKey);
        Assert.Equal("TestEntity", auditTrail.EntityName);
        Assert.Contains("Column1", auditTrail.ChangedColumns);
        Assert.Equal("OldValue1", auditTrail.OldValues["Column1"]);
        Assert.Equal("NewValue1", auditTrail.NewValues["Column1"]);
    }
}
