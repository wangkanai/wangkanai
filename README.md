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
Configuring the `Startup.cs` by adding the Browser Service in the `ConfigureServices` method.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBrowser();
    // Add framework services.
    services.AddMvc();
}
```
* `AddBrowser()` Add the browser service to the services container.
* `AddCrawler()` (*concept*) Add support for crawler detection seperation to the browser service.
* `AddExtensive()` (*concept*) Add support for extensive detection to cover a wide of client request types to the browser service.

#### Usage

Example of calling the browser detector service in the `Controller`.
```csharp
public class HomeController : Controller
{
    private readonly IBrowserService _browser;

    public HomeController(IBrowserService browser)
    {
        _browser = browser;
    }

    public IActionResult Index()
    {            
        return View(_browser);
    }
}
```

### Directory Structure
* `src` - The code of this project lives here
* `test` - Unit tests of this project to valid that everything pass specs
* `sample` - Contains sample web application of usage

### Contributing

All contribution are welcome, please contact the author.

### See the [LICENSE](https://github.com/wangkanai/Browser/blob/master/LICENSE) file.
