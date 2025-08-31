// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain;

public class MockContext(DbContextOptions<MockContext> options) : DbContext(options)
{
   public DbSet<GuidEntity> Guids { get; set; }
   public DbSet<IntEntity>  Ints  { get; set; }

   protected override void OnModelCreating(ModelBuilder builder)
      => base.OnModelCreating(builder);
}