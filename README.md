# ASP.NET Core Detection

[![Build status](https://ci.appveyor.com/api/projects/status/033qv4nqv8g4altq?svg=true&retina=true)](https://ci.appveyor.com/project/wangkanai/detection)
![GitHub](https://img.shields.io/github/license/wangkanai/detection)


[![Open Collective](https://img.shields.io/badge/open%20collective-support%20us-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-become%20a%20patron-d9643a.svg)](https://www.patreon.com/wangkanai)

### ASP.NET Core 3.X (Branch: [dev](https://github.com/wangkanai/Detection/tree/dev))

- **Wangkanai.Detection**
  [![MyGet Badge](https://buildstats.info/myget/wangkanai/Wangkanai.detection)](https://www.myget.org/feed/wangkanai/package/nuget/Wangkanai.detection)
- **Wangkanai.Responsive**
  [![MyGet Badge](https://buildstats.info/myget/wangkanai/Wangkanai.Responsive)](https://www.myget.org/feed/wangkanai/package/nuget/Wangkanai.Responsive)

### ASP.NET Core 2.X (Branch: [master](https://github.com/wangkanai/Detection/tree/master))

- **Wangkanai.Detection**
  [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection)](https://www.nuget.org/packages/wangkanai.detection)
- **Wangkanai.Responsive**
  [![NuGet Badge](https://buildstats.info/nuget/wangkanai.Responsive)](https://www.nuget.org/packages/wangkanai.Responsive)


[![Build history](https://buildstats.info/appveyor/chart/wangkanai/detection)](https://ci.appveyor.com/project/wangkanai/detection/history)

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

ASP.NET Core Responsive middleware for routing base upon request client device detection to specific view


![ASP.NET Core Responsive](https://raw.githubusercontent.com/wangkanai/Detection/dev/asset/aspnet-core-detection-2.svg?sanitize=true)


## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Detection
```

Installation of Responsive library will bring in all dependency packages (This will include `Wangkanai.Detection.Device).

```powershell
PM> install-package Wangkanai.Responsive
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

If you would like to add Responsive is configured in the `ConfigureServices` method also:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddResponsive();

    // Add framework services.
    services.AddControllersWithViews();
}
```

Or you can customize the responsive

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddResponsive(options =>
    {
        options.View.DefaultTablet = Device.Desktop;
        options.View.DefaultMobile = Device.Desktop;
        options.View.DefaultDesktop = Device.Desktop;
    });

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddResponsive()` Adds the Responsive services to the services container.

  * **Suffix** Ex `*views/[controller]/[action]/index.mobile.cshtml*`
  * **SubFoler** Ex `*views/[controller]/[action]/mobile/index.cshtml*`

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseRouting();

    app.UseResponsive();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

### Directory Structure
* `src` - The code of this project lives here
* `collection` - Collection of sample user agents for lab testing
* `sample` - Contains sample web application of usage
* `doc` - Contains the documetation on how utilized this library

### Contributing

All contribution are welcome, please contact the author.

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/wangkanai/Detection/graphs/contributors"><img src="https://opencollective.com/wangkanai/contributors.svg?width=890&button=false" /></a>

### Financial Contributors

Become a financial contributor and help us sustain our community. [[Contribute](https://opencollective.com/wangkanai/contribute)]

#### Individuals

<a href="https://opencollective.com/wangkanai"><img src="https://opencollective.com/wangkanai/individuals.svg?width=890"></a>

#### Organizations

Support this project with your organization. Your logo will show up here with a link to your website. [[Contribute](https://opencollective.com/wangkanai/contribute)]

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

### See the [LICENSE](https://github.com/wangkanai/Browser/blob/master/LICENSE) file.
