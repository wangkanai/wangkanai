## Wangkanai Google Universal Analytics

ASP.NET Core Google Analytics service is lightweight server-side code that generator library that JavaScript client-side
on the fly for Google Analytics analytics.js. The goal of the framework is to stay out of the way as much as possible
and provide a super interaction with Google all from C#.

This old project my was developed back long time ago in the days of ASP.NET Full Framework around 2013. If you like me
to upgrade this project using the design pattern my [Detection library](https://github.com/wangkanai/Detection).

**Please give me appreciation and click stars :star::star::star:, when this project reach 100 stars. I will rebuild
project for ASP.NET Core 6.0**.
But you wanna contract me to get start development of the a migration to ASP.NET Core for your production requirement
and long term support. Please contact me personally and we can discuss the details.

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.universal)](https://www.nuget.org/packages/wangkanai.universal)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.universal?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.universal)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

## Installation

Installation of google universal analytics library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Universal
```

## Configuration

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddGoogleAnalytics();

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddGoogleAnalytics()` Adds the google universal services to the services container.

The current device on a request is set in the Responsive middleware. The Universal middleware is enabled in
the `Configure` method of *Startup.cs* file.

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseGoogleAnalytics();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

This project provide the full source code and complied dll library for you to add reference into your visual studio
projects. The library include Intellisense to help guide you write server-side code in an effective manner.

```c#
public ActionResult Ecommerce()
{
    var session = new Wangkanai.Universal.Session();
    session.Transaction = new Transaction("1234", "store", 1.0, 1.0);
    session.Transaction.Items.Add(new Item("product a", "a001", "xxx", 2.0, 1));
    session.Transaction.Items.Add(new Item("product b", "b001", "yyy", 3.0, 2));
    ViewBag.AnalyticsSession = session;
    return View();
}
```

### Features

![IntelliSense](https://raw.githubusercontent.com/wangkanai/wangkanai/main/universal/asset/vs-intellisense.png)

### Demo

![Mvc Demo Web App](https://raw.githubusercontent.com/wangkanai/wangkanai/main/universal/asset/web-sample.png)

#### Where it originated from?

This original library that was introduced in [CodePlex](https://archive.codeplex.com/?p=universalanalytics). The library
was for .NET Framework web application to render **analytics.js** javascript client-side to interact with Google API
from .NET language web application within the server-side code environment. Both ASP.NET WebForm and ASP.NET MVC. Its
was good, but we would like to see a much more extensible and ambust platform, so we are rewriting the entire framework.

#### How do i contribute?

Universal Analytics is a powerful and continuous improving platform. We would like to invite developers to help maintain
and add features so that this library is keep aligned with most of the popular web analytics out there. 

