// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Wangkanai.Federation.EntityFramework;

namespace Wangkanai.Federation.Data;

public class ApplicationDbContext : FederationDbContext<IdentityUser, IdentityRole, string>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}