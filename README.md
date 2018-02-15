# ASP.NET Core Detection

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

The library is the base foundation for [ASP.NET Core Responsive](https://github.com/wangkanai/Responsive)

![ASP.NET Core Responsive](https://raw.githubusercontent.com/wangkanai/Detection/dev/asset/aspnet-core-detection.svg?sanitize=true)

[![Build status](https://ci.appveyor.com/api/projects/status/033qv4nqv8g4altq?svg=true)](https://ci.appveyor.com/project/wangkanai/detection)

package | nuget    |
--------|----------|
Wangkanai.Detection | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection/)  |
Wangkanai.Detection.Device | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Device.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection.Device/) | 
Wangkanai.Detection.Browser | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Browser.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection.Browser/) | 
Wangkanai.Detection.Engine | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Engine.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection.Engine/) | 
Wangkanai.Detection.Platform | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Platform.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection.Platform/) |
Wangkanai.Detection.Crawler | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Crawler.svg?maxAge=3600)](https://www.nuget.org/packages/Wangkanai.Detection.Crawler/) | 


## Installation (beta7)

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


## Configuration (beta8) {[Breaking change #59](/../../issues/59)}

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

While the detection service is congifured globally, its can also be configure individually if you only need some functions.

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

## *(Concept)* Add extensions to `HttpRequest` [Learn more #1](/../../issues/1)

This would allow quick access to the Detection in any client request.

```csharp
var browser = Request.Browser();
var device = Request.Device();
var platform = Request.Platform();
var engine = Request.Engine();
```

### Directory Structure
* `src` - The code of this project lives here
* `test` - Unit tests of this project to valid that everything pass specs
* `sample` - Contains sample web application of usage

### Contributing

All contribution are welcome, please contact the author.

### See the [LICENSE](https://github.com/wangkanai/Browser/blob/master/LICENSE) file.
