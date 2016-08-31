# ASP.NET Core Browser

[![Build status](https://ci.appveyor.com/api/projects/status/nwke0v8dqp3xkgwr/branch/dev?svg=true)](https://ci.appveyor.com/project/wangkanai/browser/branch/dev) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Browser.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Browser/)

![ASP.NET Core Responsiveness](https://raw.githubusercontent.com/wangkanai/browser/master/asset/aspnet-core-browser.png)

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

### Installation - [NuGet](https://www.nuget.org/packages/Wangkanai.Browser/)

```powershell
PM> install-package Wangkanai.Browser -pre
```

### Implement detection the device for each request

#### Configuring
Configuring the `Startup.cs` by adding the Client Service in the `ConfigureServices` method.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBrowser()
        .AddPlatform() // concept
        .AddEngine();  // concept
    // Add framework services.
    services.AddMvc();
}
```
* `AddBrowser()` Adds the client services to the services container.
* `AddPlatform()` (*concept*) Adds platform identification to the client services.
* `AddEngine()` (*concept*) Adds rendering engine identification to the client services.

#### Usage

Example of calling the client service in the `Controller`.
```csharp
public class HomeController : Controller
{
    private readonly IClientService _client;

    public HomeController(IClientService client)
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
var browser = HttpContext.Request.Browser();
var device = HttpContext.Request.Device();
var platform = HttpContext.Request.Platform();
var engine = HttpContext.Request.Engine();
```

### Directory Structure
* `src` - The code of this project lives here
* `test` - Unit tests of this project to valid that everything pass specs
* `sample` - Contains sample web application of usage

### Contributing

All contribution are welcome, please contact the author.

### See the [LICENSE](https://github.com/wangkanai/Browser/blob/master/LICENSE) file.
