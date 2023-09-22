// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Wangkanai.Federation.EntityFramework;

namespace Wangkanai.Federation.RazorApp.Data;

public class ApplicationDbContext : FederationDbContext<IdentityUser, IdentityRole, Guid>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}