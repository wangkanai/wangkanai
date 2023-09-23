// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;

namespace Wangkanai.EntityFramework;

public class DatabaseMigrationExtensions
{
    // [Fact]
    // public void GenericNotDbContext()
    // {
    //     var services = new ServiceCollection();
    //     services.AddDbContext<FooDbContext>();
    //     
    //     var scope = new Mock<IServiceScope>();
    //     scope.Setup(x => x.ServiceProvider)
    //          .Returns(services.BuildServiceProvider());
    //     var provider = new Mock<IServiceProvider>();
    //     provider.Setup(x=>x.CreateScope())
    //             .Returns(scope.Object);
    //     var mock = new Mock<IApplicationBuilder>();
    //     mock.Setup(x => x.ApplicationServices)
    //         .Returns(provider.Object);
    //     
    //     var app = mock.Object;
    //     app.MigrateDatabase<FooDbContext>();
    // }

    [Fact]
    public void IsDbContextSubClass()
    {
        Assert.True(typeof(FooDbContext).IsSubclassOf(typeof(DbContext)));
        Assert.False(typeof(string).IsSubclassOf(typeof(DbContext)));
    }
}

public class FooDbContext : DbContext
{
}