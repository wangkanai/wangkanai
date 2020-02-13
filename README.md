# ASP.NET Core Detection with Responsive View

ASP.NET Core Detection service components for identifying details about client device, browser, engine, platform, & crawler. Responsive middleware for routing base upon request client device detection to specific view. Also in the added feature of user preference made this library even more comprehensive must for developers whom to target multiple devices with view rendered and optimized directly from the server side.

**Please show me some love and click on the** :star:.

<img src="https://raw.githubusercontent.com/wangkanai/Detection/dev/asset/aspnet-core-detection-3.svg?sanitize=true" width="650" alt="ASP.NET Core Detection" />

![GitHub](https://github.com/wangkanai/Detection/workflows/Detection-CI/badge.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/033qv4nqv8g4altq?svg=true&retina=true)](https://ci.appveyor.com/project/wangkanai/detection)
[![GitHub](https://img.shields.io/github/license/wangkanai/detection)](https://github.com/wangkanai/Detection/blob/dev/LICENSE)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
 
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection)](https://www.nuget.org/packages/wangkanai.detection)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.detection)
[![MyGet Badge](https://buildstats.info/myget/wangkanai/wangkanai.detection)](https://www.myget.org/feed/wangkanai/package/nuget/wangkanai.detection)

[![Build history](https://buildstats.info/appveyor/chart/wangkanai/detection)](https://ci.appveyor.com/project/wangkanai/detection/history)

This project development has been in the long making of my little spare time. Please show your appreciation and help me provide feedback on you think will improve this library. All developers are welcome to come and improve the code by submit a pull request. We will have constructive good discussion together to the greater good.

## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Detection
```

## Configuration

This library host the component to resolve the access client device type.

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection service in the `ConfigureServices` method.

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

Or you can customize the responsive

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddDetection(options =>
    {
        options.Responsive.DefaultTablet  = Device.Desktop;
        options.Responsive.DefaultMobile  = Device.Mobile;
        options.Responsive.DefaultDesktop = Device.Desktop;
        options.Responsive.IncludeWebApi  = false;
        options.Responsive.Disable        = false;
        options.Responsive.WebApiPath     = "/Api";
    });

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddDetection()` Adds the detection services to the services container.

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseRouting();

    app.UseDetection();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```csharp
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Detection
```

### Directory Structure

* `src` - The source code of this project lives here
* `test` - The test code of this project lives here
* `collection` - Collection of sample user agents for lab testing
* `sample` - Contains sample web application of usage
* `doc` - Contains the documentation on how utilized this library

### Contributing

All contribution are welcome, please contact the author.

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/wangkanai/Detection/graphs/contributors"><img src="https://opencollective.com/wangkanai/contributors.svg?width=890&button=false" /></a>

### Financial Contributors

Become a financial contributor and help us sustain our community. [[Contribute](https://opencollective.com/wangkanai/contribute)]

#### Individuals

[![Individuals Contributors](https://opencollective.com/wangkanai/individuals.svg?width=890)](https://opencollective.com/wangkanai)

#### Organizations

Support this project with your organization. Your logo will show up here with a link to your website. 
[[Contribute](https://opencollective.com/wangkanai/contribute)]

<a href="https://opencollective.com/wangkanai/organization/0/website"><img src="https://opencollective.com/wangkanai/organization/0/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/1/website"><img src="https://opencollective.com/wangkanai/organization/1/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/2/website"><img src="https://opencollective.com/wangkanai/organization/2/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/3/website"><img src="https://opencollective.com/wangkanai/organization/3/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/4/website"><img src="https://opencollective.com/wangkanai/organization/4/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/5/website"><img src="https://opencollective.com/wangkanai/organization/5/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/6/website"><img src="https://opencollective.com/wangkanai/organization/6/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/7/website"><img src="https://opencollective.com/wangkanai/organization/7/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/8/website"><img src="https://opencollective.com/wangkanai/organization/8/avatar.svg"></a>
<a href="https://opencollective.com/wangkanai/organization/9/website"><img src="https://opencollective.com/wangkanai/organization/9/avatar.svg"></a>
