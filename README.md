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
```

## Device Resolver

This library host the component to resolve the access client device type.

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	// Add detection services container and device resolver service.
    services.AddDetection()
		.AddDevice();

    // Add framework services.
    services.AddMvc();
}
```
* `AddDetection()` Adds the detection services to the services container.
* `AddDevice()` Adds the device resolver service to the detection services builder.

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

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the detection service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
	// Add detection services container and device resolver service.
    services.AddDetection()
		.AddBrowser();

    // Add framework services.
    services.AddMvc();
}
```
* `AddDetection()` Adds the detection services to the services container.
* `AddBrowser()` Adds the browser resolver service to the detection services builder.

Example of calling the detection service in the `Controller` using dependency injection.

```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;
    private readonly IBrowser _browser;   

    public HomeController(IBrowser browserResolver)
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


## Concept waiting for development

Configuring the `Startup.cs` by adding the Client Service in the `ConfigureServices` method.
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// Add browser detection services.
    services.AddDetection()		
		.AddEngine()    // concept
		.AddPlatform(); // concept

    // Add framework services.
    services.AddMvc();
}
```
* `AddDetection()` Adds the detection services to the services container.
* `AddEngine()` Adds the engine resolver service to the detection services builder.
* `AddPlatform()` Adds the platform resolver service to the detection services builder.


#### Usage

Example of calling the client service in the `Controller`.
```csharp
public class HomeController : Controller
{    
    private readonly IUserAgent _useragent;        
    private readonly IEngine _engine;
    private readonly IPlatform _platform;

    public HomeController(IEngineResolver engineResolver, 
        IPlatformResolver platformResolver)
    {
        _useragent = browserResolver.UserAgent;                
        _engine = engineResolver.Engine;
        _platform = platformResolver.Platform;
    }

    public IActionResult Index()
    {            
        return View();
    }
}
```
*(Concept)* Add extensions to `HttpRequest` [Learn more #1](/../../issues/1)
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
