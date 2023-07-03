## Installation

Add the NuGet package to your project.

```powershell
PM> install-package Wangkanai.Analytics
```

Add the service to your web app.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add application services.
    services.AddAnalytics();

    // Add framework services.
    services.AddControllersWithViews();
}
```

The Analytics middleware is enable in the `Configure` method of *Startup.cs* file.

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseAnalytics();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```c#
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Analytics
```
