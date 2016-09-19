# ASP.NET Core Detection

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

![ASP.NET Core Responsiveness](https://raw.githubusercontent.com/wangkanai/browser/master/asset/aspnet-core-browser.png)

package | build | nuget    |
--------|-------|----------|
Wangkanai.Detection | [![Build status](https://ci.appveyor.com/api/projects/status/nwke0v8dqp3xkgwr/branch/dev?svg=true)](https://ci.appveyor.com/project/wangkanai/browser/branch/dev) |  [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Browser.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Browser/)  |
Wangkanai.Detection.Device | | | 
Wangkanai.Detection.Browser | | | 
Wangkanai.Detection.Engine | | | 
Wangkanai.Detection.Platform | | | 

### Installation - [NuGet](https://www.nuget.org/packages/Wangkanai.Browser/)

```powershell
PM> install-package Wangkanai.Detection.Device -pre
PM> install-package Wangkanai.Detection.Browser -pre
PM> install-package Wangkanai.Detection.Engine -pre
PM> install-package Wangkanai.Detection.Platform -pre
```

### Implement detection the device for each request

#### Configuring
Configuring the `Startup.cs` by adding the Client Service in the `ConfigureServices` method.
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// Add browser detection services.
    services.AddClientService()
		.AddDevice()
		.AddBrowser()
		.AddEngine()
		.AddPlatform();

    // Add framework services.
    services.AddMvc();
}
```
* `AddClientService()` Adds the client services to the services container.
* `AddDevice()` Adds the device resolver service to the client services builder.
* `AddBrowser()` Adds the browser resolver service to the client services builder.
* `AddEngine()` Adds the engine resolver service to the client services builder.
* `AddPlatform()` Adds the platform resolver service to the client services builder.


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
