# ASP.NET Core Detection

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection)](https://www.nuget.org/packages/wangkanai.detection)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.detection)


ASP.NET Core Detection service components for identifying details about client device, browser, engine, platform, &
crawler.

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

![ASP.NET Core Detection](https://raw.githubusercontent.com/wangkanai/wangkanai/main/Asset/aspnet-core-detection-3.svg?sanitize=true)

## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Detection
```

## Configuration

This library host the component to resolve the access client device type.

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection
service in the `ConfigureServices` method.

```c#
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

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDetection();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```c#
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Detection
```
