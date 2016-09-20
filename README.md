# ASP.NET Core Detection

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

![ASP.NET Core Responsiveness](https://raw.githubusercontent.com/wangkanai/browser/master/asset/aspnet-core-browser.png)

package | build | nuget    |
--------|-------|----------|
Wangkanai.Detection | [![Build status](https://ci.appveyor.com/api/projects/status/033qv4nqv8g4altq?svg=true)](https://ci.appveyor.com/project/wangkanai/detection) | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Detection/)  |
Wangkanai.Detection.Device | | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Detection.Device.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Detection.Device/) | 
Wangkanai.Detection.Browser | | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Browser.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Browser/) | 
Wangkanai.Detection.Engine | | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Engine.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Engine/) | 
Wangkanai.Detection.Platform | | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Platform.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Platform/) | 

### Installation - [NuGet](https://www.nuget.org/packages/Wangkanai.Browser/)

```powershell
PM> install-package Wangkanai.Detection.Device -pre
PM> install-package Wangkanai.Detection.Browser -pre  //concept
PM> install-package Wangkanai.Detection.Engine -pre   //concept
PM> install-package Wangkanai.Detection.Platform -pre //concept
```

### Implement detection the device for each request

#### Configuring
Configuring the `Startup.cs` by adding the Client Service in the `ConfigureServices` method.
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// Add browser detection services.
    services.AddDetection()
		.AddDevice()
		.AddBrowser()   // concept
		.AddEngine()    // concept
		.AddPlatform(); // concept

    // Add framework services.
    services.AddMvc();
}
```
* `AddDetection()` Adds the detection services to the services container.
* `AddDevice()` Adds the device resolver service to the detection services builder.
* `AddBrowser()` Adds the browser resolver service to the detection services builder.
* `AddEngine()` Adds the engine resolver service to the detection services builder.
* `AddPlatform()` Adds the platform resolver service to the detection services builder.


#### Usage

Example of calling the client service in the `Controller`.
```csharp
public class HomeController : Controller
{
    private readonly IClientInfo _client;

    public HomeController(IClientInfo client)
    {
        _client = client;
    }

    public IActionResult Index()
    {            
        return View(_client);
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
