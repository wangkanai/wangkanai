# ASP.NET Core Detection with Responsive View

ASP.NET Core Detection service components for identifying details about client device, browser, engine, platform, &
crawler. Responsive middleware for routing base upon request client device detection to specific view. Also in the added
feature of user preference made this library even more comprehensive must for developers whom to target multiple devices
with view rendered and optimized directly from the server side.

![ASP.NET Core Detection](https://raw.githubusercontent.com/wangkanai/Wangkanai/main/Asset/aspnet-core-detection-3.svg?sanitize=true)

[![GitHub](https://img.shields.io/github/license/wangkanai/Wangkanai)](https://github.com/wangkanai/Wangkanai/blob/dev/LICENSE)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)

## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Detection
```

## Configuration

This library host the component to resolve the access client device type.

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection
service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddDetection();

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddDetection()` Adds the detection services to the services container.

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in
the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDetection();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```csharp
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Detection
```
