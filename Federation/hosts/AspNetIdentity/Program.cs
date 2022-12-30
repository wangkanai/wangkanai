using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Wangkanai.Federation;
using Wangkanai.Federation.RazorApp.Data;
using Wangkanai.Webserver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(AppSettings.ConnectionStrings.Default))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddFederation()
       .AddAspNetIdentity<IdentityUser>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();
app.UseFederation();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization(FederationConstants.LocalApi.PolicyName);
app.MapRazorPages().RequireAuthorization();

app.Run();