// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Web.Server.Tests;

public class ProgramTests
{
	[Fact]
	public void BuilderServicesAddRouting()
	{
		var builder = WebApplication.CreateBuilder();
		builder.Services.AddRouting(options => options.LowercaseUrls = true);
		var app = builder.Build();
		Assert.NotNull(app.Services.GetService(typeof(Microsoft.AspNetCore.Routing.IRouter)));
	}
}