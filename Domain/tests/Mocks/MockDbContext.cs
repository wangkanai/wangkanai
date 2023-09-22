// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain;

public class MockDbContext : DbContext
{
	public DbSet<GuidEntity> Guids { get; set; }
	public DbSet<IntEntity>  Ints  { get; set; }

	public MockDbContext(DbContextOptions<MockDbContext> options)
		: base(options) { }

	protected override void OnModelCreating(ModelBuilder builder) 
		=> base.OnModelCreating(builder);
}