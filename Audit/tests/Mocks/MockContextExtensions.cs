// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Wangkanai.Audit;

public static class MockExtensions
{
   public static EntityTypeBuilder<TEntity> GetEntityTypeBuilder<TEntity, TConfiguration>()
      where TEntity : class
      where TConfiguration : IEntityTypeConfiguration<TEntity>, new()
   {
      var options = new DbContextOptionsBuilder<MockContext>()
                   .UseInMemoryDatabase("test")
                   .Options;

      var context       = new MockContext(options);
      var convention    = ConventionSet.CreateConventionSet(context);
      var modelBuilder  = new ModelBuilder(convention);
      var entityBuilder = modelBuilder.Entity<TEntity>();
      var entityConfig  = new TConfiguration();
      entityConfig.Configure(entityBuilder);

      return entityBuilder;
   }
}