## Wangkanai Webmaster

ASP.NET Core Search Engine Optimization for Webmaster lets you improve the art, craft, and science of driving web
traffic to enabling you with easy and efficiency native tool build in the asp.net core library.

**Please give some love by clicking on the star!** :star::star::star: This help movtivate me to continue on developing
the project.

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.Webmaster)](https://www.nuget.org/packages/wangkanai.Webmaster)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.Webmaster?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.Webmaster)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

### Installation

Installation of Wangkanai Webmaster

```powershell
PM> install-package Wangkanai.Webmaster
```

Implementation of the library into your web application is done by configuring the `startup.cs` by adding the Webmaster
service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddWebmaster();

    services.AddControllersWithViews();
}
```

Adding the Webmaster middleware to the pipeline. The Webmaster middleware is enabled in the `Configure` method
of `Startup.cs` file.

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseWebmaster();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

### Directory Structure

- `src` - The code of this project lives here
- `test` - The unit test of this project lives here
- `sample` - The sample web app of this project lives here

### Contributing

All contribution are welcome, please contact the author.
