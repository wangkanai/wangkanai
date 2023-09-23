// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Wangkanai.Domain;

public static class MockExtensions
{
	public static EntityTypeBuilder<TEntity> GetEntityTypeBuilder<TEntity, TConfiguration>()
		where TEntity : class
		where TConfiguration : IEntityTypeConfiguration<TEntity>, new()
	{
		var options = new DbContextOptionsBuilder<MockDbContext>()
		              .UseInMemoryDatabase("test")
		              .Options;

		var context       = new MockDbContext(options);
		var convention    = ConventionSet.CreateConventionSet(context);
		var modelBuilder  = new ModelBuilder(convention);
		var entityBuilder = modelBuilder.Entity<TEntity>();
		var entityConfig  = new TConfiguration();
		entityConfig.Configure(entityBuilder);

		return entityBuilder;
	}
}