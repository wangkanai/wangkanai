# ASP.NET Core Detection

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

![ASP.NET Core Responsive](https://raw.githubusercontent.com/wangkanai/Detection/dev/asset/aspnet-core-detection-2.svg?sanitize=true)

[![Financial Contributors on Open Collective](https://opencollective.com/wangkanai/all/badge.svg?label=financial+contributors)](https://opencollective.com/wangkanai) [![Build status](https://ci.appveyor.com/api/projects/status/033qv4nqv8g4altq?svg=true&retina=true)](https://ci.appveyor.com/project/wangkanai/detection)

[![Build history](https://buildstats.info/appveyor/chart/wangkanai/detection)](https://ci.appveyor.com/project/wangkanai/detection/history)

package | nuget    |
--------|----------|
Wangkanai.Detection | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.detection) |
Wangkanai.Detection.Device | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection.device?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Detection.Device/) | 
Wangkanai.Detection.Browser | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection.browser?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Detection.Browser/) | 
Wangkanai.Detection.Engine | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection.engine?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Detection.Engine/) | 
Wangkanai.Detection.Platform | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection.platform?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Detection.Platform/) |
Wangkanai.Detection.Crawler | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection.crawler?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Detection.Crawler/) | 
Wangkanai.Responsive | [![NuGet Badge](https://buildstats.info/nuget/wangkanai.responsive?includePreReleases=true)](https://www.nuget.org/packages/Wangkanai.Responsive/) |

## Installation

Installation of detection library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Detection -pre
```

While it is still possible to install the individual package if you just need that specific resolver.

```powershell
PM> install-package Wangkanai.Detection.Device -pre  
PM> install-package Wangkanai.Detection.Browser -pre  
PM> install-package Wangkanai.Detection.Engine -pre   //concept
PM> install-package Wangkanai.Detection.Platform -pre //concept
PM> install-package Wangkanai.Detection.Crawler -pre  
```

Installation of Responsive library will bring in all dependency packages (This will include `Wangkanai.Detection.Device).

```powershell
PM> install-package Wangkanai.Responsive -pre
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
    services.AddMvc();
}
```

* `AddDetection()` Adds the detection services to the services container.

While the detection service is configured globally, its can also be configure individually if you only need some functions.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddDetectionCore()
        .AddDevice()
        .AddBrowser()
        .AddPlatform()  // concept
        .AddEngine()    // concept
        .AddCrawler();

    // Add framework services.
    services.AddMvc();
}
```

* `AddDetectionCore()` Adds the detection services to the services container.
* `AddDevice()` Adds the device resolver service to the detection core services builder.
* `AddBrowser()` Adds the browser resolver service to the detection core services builder.
* `AddPlatform()` Adds the platform resolver service to the detection core services builder.
* `AddEngine()` Adds the engine resolver service to the detection core services builder.
* `AddCrawler()` Adds the crawler resolver service to the detection core services builder.

Responsive is configured in the `ConfigureServices` method:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddResponsive();

    // Add framework services.
    services.AddMvc();  
}
```

Or you could be more specific using the `AddResponsiveCore()` method:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddResponsiveCore()
        .AddViewSuffix()
        .AddViewSubfolder();

    // Add framework services.
    services.AddMvc();  
}
```

* `AddResponsive()` Adds the Responsive services to the services container.
* `AddViewSuffix()` Adds support for device view files  to `Suffix`. In this sample view Responsive is based on the view file suffix. 

  Ex `*views/[controller]/[action]/index.mobile.cshtml*`

* `AddViewSubfolder()` Adds support for device view files to `Subfolder`. In this sample view Responsive is based on the view file subfolder. 

  Ex `*views/[controller]/[action]/mobile/index.cshtml*`

## Configure Middleware

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseResponsive();

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

## Customized able options (beta 3)

This enable the middleware to customized the default response for any type of request device to the configured options.

```
app.UseResponsive(new ResponsiveOptions
{
    TabletDefault = DeviceType.Mobile
});
```

* `UseResponsive()` Add the responsive middleware into the http pipeline. Its will capture the request and resolve the device to responsive services container.

## Global Resolver (beta8)

This library host the component to resolve all the access client related resolver that could resolve.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{
    private readonly IDetection _detection;

    public HomeController(IDetection detection)
    {
        _detection = detection;
    }

    public IActionResult Index()
    {
        return View(_detection);
    }
}
```
* `IDetection` is main service for you to access detection service.

When the `Detection` is pass to the view you can render results like the following example.

```html
@model Wangkanai.Detection.Detection

<h3>UserAgent</h3>
<code>@Model.UserAgent</code>

<h3>Results</h3>

<table>
    <thead>
        <tr>
            <th>Resolver</th>
            <th>Type</th>
            <th>Version</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th>Device</th>
            <td>@Model.Device?.Type.ToString()</td>
            <td></td>
        </tr>
        <tr>
            <th>Browser</th>
            <td>@Model.Browser?.Type.ToString()</td>
            <td>@Model.Browser?.Version</td>
        </tr>
        <tr>
            <th>Platform</th>
            <td>@Model.Platform?.Type.ToString()</td>
            <td>@Model.Platform?.Version</td>
        </tr>
        <tr>
            <th>Engine</th>
            <td>@Model.Engine?.Type.ToString()</td>
            <td>@Model.Engine?.Version</td>
        </tr>
        <tr>
            <th>Crawler</th>
            <td>@Model.Crawler?.Type.ToString()</td>
            <td>@Model.Crawler?.Version</td>
        </tr>
    </tbody>
</table>
```

## Device Resolver

This library host the component to resolve the access client device type.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly IDevice _device;   

    public HomeController(IDeviceResolver deviceResolver)
    {
        _useragent = deviceResolver.UserAgent;
        _device = deviceResolver.Device;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
* `IDetectionService` is main service for you to access UserAgent

## Browser Resolver (beta7)

This library host the component to resolve the access client browser type and version.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly IBrowser _browser;   

    public HomeController(IBrowserResolver browserResolver)
    {
        _useragent = browserResolver.UserAgent;
        _browser = browserResolver.Browser;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
* `IDetectionService` is main service for you to access UserAgent.

## Platform Resolver (concept)

This library host the component to resolve the access client platform type and version.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly IPlatform _platform;   

    public HomeController(IPlatformResolver platformResolver)
    {
        _useragent = platformResolver.UserAgent;
        _platform = platformResolver.Platform;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
* `IDetectionService` is main service for you to access UserAgent.

## Engine Resolver (concept)

This library host the component to resolve the access client engine type and version.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly IEngine _engine;   

    public HomeController(IEngineResolver engineResolver)
    {
        _useragent = engineResolver.UserAgent;
        _engine = engineResolver.Engine;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
* `IDetectionService` is main service for you to access UserAgent.


## Crawler Resolver (beta8)

This library host the component to resolve the access client crawler type and version.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly ICrawler _crawler;   

    public HomeController(ICrawlerResolver crawlerResolver)
    {
        _useragent = crawlerResolver.UserAgent;
        _crawler = crawlerResolver.Crawler;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
* `IDetectionService` is main service for you to access UserAgent.

## `HttpRequest` extensions (beta10) [Learn more #1](/../../issues/1)

This would allow quick access to the Detection in any client request.

```csharp
var browser = Request.Browser();
var device = Request.Device();
var platform = Request.Platform();
var engine = Request.Engine();
var crawler = Request.Crawler();
```

### Directory Structure
* `src` - The code of this project lives here
* `test` - Unit tests of this project to valid that everything pass specs
* `sample` - Contains sample web application of usage

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
