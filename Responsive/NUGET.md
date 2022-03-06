# Wangkanai Responsive View

ASP.NET Core Responsive middleware for routing base upon request client device detection to specific view. Also in the
added
feature of user preference made this library even more comprehensive must for developers whom to target multiple devices
with view rendered and optimized directly from the server side.

[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/dev/LICENSE)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)

## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Responsive
```

## Configuration

This library host the component to resolve the access client device type.

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection
service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddResponsive();

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddResponsive()` Adds the responsive services to the services container.

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in
the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseResponsive();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```csharp
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Responsive
```
