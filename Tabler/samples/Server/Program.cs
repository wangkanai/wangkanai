using Host.Main.Components;

using Wangkanai.Tabler.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCascadingValue(ts => new ThemeService());

builder.Services.AddRazorComponents()
       .AddServerComponents()
       .AddWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapRazorComponents<App>()
   .AddServerRenderMode()
   .AddWebAssemblyRenderMode();

app.Run();