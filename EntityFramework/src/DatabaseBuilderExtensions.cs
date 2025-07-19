// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.EntityFramework;

public static class DatabaseBuilderExtensions
{
	public static IApplicationBuilder CreateDatabase<T>(this IApplicationBuilder app)
		where T : DbContext
	{
		using var scope = app.ApplicationServices.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<T>();

		context.ThrowIfNull();

		context.Database.EnsureCreated();

		return app;
	}

	public static async Task<IApplicationBuilder> CreateDatabaseAsync<T>(this IApplicationBuilder app)
		where T : DbContext
	{
		await using var scope = app.ApplicationServices.CreateAsyncScope();
		await using var context = scope.ServiceProvider.GetRequiredService<T>();

		context.ThrowIfNull();

		await context.Database.EnsureCreatedAsync();

		return app;
	}

	public static IApplicationBuilder MigrateDatabase<T>(this IApplicationBuilder app)
		where T : DbContext
	{
		using var scope = app.ApplicationServices.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<T>();

		context.ThrowIfNull();

		context.Database.Migrate();

		return app;
	}

	public static async Task<IApplicationBuilder> MigrateDatabaseAsync<T>(this IApplicationBuilder app)
		where T : DbContext
	{
		await using var scope = app.ApplicationServices.CreateAsyncScope();
		await using var context = scope.ServiceProvider.GetRequiredService<T>();

		context.ThrowIfNull();

		await context.Database.MigrateAsync();

		return app;
	}
}
