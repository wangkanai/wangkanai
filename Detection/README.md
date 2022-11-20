## ASP.NET Core Detection

ASP.NET Core Detection service components for identifying details about client device, browser, engine, platform, &
crawler.

**Please show me some love and click the** :star:

<img src="https://raw.githubusercontent.com/wangkanai/wangkanai/main/Assets/aspnet-core-detection-3.svg?sanitize=true" width="650" alt="ASP.NET Core Detection" />

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection)](https://www.nuget.org/packages/wangkanai.detection)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.detection?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.detection)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

This project development has been in the long making of my little spare time. Please show your appreciation and help me
provide feedback on you think will improve this library. All developers are welcome to come and improve the code by
submit a pull request. We will have constructive good discussion together to the greater good.

- [ASP.NET Core Detection](#aspnet-core-detection)
- [Installation](#installation)
- [Detection Service](#detection-service)
    - [Make your web app able to detect what client is accessing](#make-your-web-app-able-to-detect-what-client-is-accessing)
        - [MVC](#mvc)
        - [Razor Pages](#razor-pages)
    - [Detection in Middleware](#detection-in-middleware)
    - [Detection Fundamentals](#detection-fundamentals)
        - [Device Resolver](#device-resolver)
        - [Browser Resolver](#browser-resolver)
        - [Platform Resolver](#platform-resolver)
        - [Engine Resolver](#engine-resolver)
        - [Crawler Resolver](#crawler-resolver)
        - [Detection Options](#detection-options)
- [Responsive Service](#responsive-service)
    - [Responsive MVC](#responsive-mvc)
    - [Responsive Razor Pages](#responsive-razor-pages)
    - [Responsive Tag Helpers](#responsive-tag-helpers)
    - [User Preference](#user-preference)
    - [Responsive Options](#responsive-options)
- [Directory Structure](#directory-structure)
    - [Contributing](#contributing)
- [Contributors](#contributors)
    - [Code Contributors](#code-contributors)
    - [Financial Contributors](#financial-contributors)
        - [Individuals](#individuals)
        - [Organizations](#organizations)

## Installation

Installation of detection library is now done with a single package reference point. If you are using ASP.NET Core 2.X
please use [detection version 2.0 installation](https://github.com/wangkanai/wangkanai/tree/release/2.0).

```powershell
PM> install-package Wangkanai.Detection
```

This library host the component services to resolve the access client device type. To the servoces your web application
is done by configuring the `Startup.cs` by adding the detection service in the `ConfigureServices` method.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddDetection();

    // Needed by Wangkanai Detection
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddDetection()` Adds the detection services to the services container.

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in
the `Configure` method of `Startup.cs`
file. [Make sure that you have app.UseDetection() before app.UseRouting](https://github.com/wangkanai/wangkanai/issues/355)
.

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseDetection();
    
    app.UseRouting();

    app.UseSession();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```razor
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Detection
```

## Detection Service

After you have added the basic of the detection services, let us learn how to utilized the library in your web
application. In which we have got the help
from [dependency injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection) to access
all functionality of `IDetectionService` has to offer.

### Make your web app able to detect what client is accessing

Detection service support usage in both Model-View-Controller (MVC) and Razor Pages.

#### MVC

Here is how you would use the library in `Controller` of
a [MVC pattern](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/) by injecting the detection
service into the constructor of the controller.

```c#
public class AboutController : Controller
{
    private readonly IDetectionService _detectionService;

    public AboutController(IDetectionService detectionService)
    {
        _detectionService = detectionService;
    }

    public IActionResult Index()
    {
        return View(_detectionService);
    }
}
```

#### Razor Pages

For [razor pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/) web application that only have
the pages without `PageModel` behind, you can access the detection service via the `@inject` tag after the `@page` in
your _.cshtml_ files. Here would be the example below;

```razor
@page
@inject Wangkanai.Detection.Services.IDetectionService DetectionService
@{
    ViewData["Title"] = "Detection";
}
<ul>
    <li>@DetectionService.Device.Type</li>
    <li>@DetectionService.Browser.Name</li>
    <li>@DetectionService.Platform.Name</li>
    <li>@DetectionService.Engine.Name</li>
    <li>@DetectionService.Crawler.Name</li>
</ul>
```

What if you razor pages use the code behind model, you can still inject the detection service into it via the
constructor just similar way as MVC controller does it.

```c#
public class IndexModel : PageModel
{
    private readonly IDetectionService _detectionService;

    public IndexModel(IDetectionService detectionService)
    {
        _detectionService = detectionService;
    }
    
    public void OnGet()
    {
        var device = _detectionService.Device.Type;
    }
}
```

### Detection in Middleware

Would you think that [Middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/) can also use
this detection service. Actually it can! and our [Responsive](#responsive-service) make good use of it too. Let us learn
how that you would use detection service in your custom middleware which we would use
the [Per-request middleware dependencies](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write#per-request-middleware-dependencies)
. But why would we use pre-request injection for our middleware you may ask? Easy! because every user is unique.
Technically answer would be that `IDetectionService` by using `TryAddTransient<TInterface, TClass>` where you
can [learn more about transient](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection#transient)
. If you inject `IDetectionServices` into the middleware constructor, then the service would become a singleton. Meaning
to subject to application not per client request.

So now we know about the basic lets look at the code:

```c#
public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, IDetectionService detection)
    {
        if(detection.Device.Type == Device.Mobile)
            context.Response.WriteAsync("You are Mobile!");

        await _next(context);
    }
}
```

### Detection Fundamentals

Detection services would extract information about the visitor web client by parsing
the [user agent](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/User-Agent) that the web browser gives to the
web server on every http/https request. We would make the assumption that every requester is using common Mozilla
syntax: _Mozilla/[version] ([system and browser information]) [platform] ([platform details]) [extensions]_. If
detection service can not identify the information, it will we have give you `Unknown` enum flag. There are total of 5
resolver services the our detection services.

#### Device Resolver

This would be the basic resolver that you might be thinking using to identify what kind client `Device` is access your
web app, which include Desktop, Tablet, and Mobile for the basic stuff. While we can also use to identify is the device
a Watch, Tv, Game Console, Car, and Internet of Things.

```c#
var isMobile = detectionService.Device.Type == Device.Mobile;
```

#### Browser Resolver

Moving the stack we get what `Browser` is the client using to access your web app. We only include the common web
browser detection starting from Chrome, Internet Explorer, Safari, Firefox, Edge, and Opera. For the rest we would mark
them as others or unknown (aka Crawler).

```c#
var isIE = detectionService.Browser.Name == Browser.InternetExplorer;
```

#### Platform Resolver

Now we can also identify what `Platform` is the client using to access your web app starting
in [version 3.0](https://github.com/wangkanai/wangkanai/milestone/13). We got Windows, Mac, iOS, Linux, and Android.

```c#
var isMac = detectionService.Platform.Name == Platform.Mac;
```

#### Engine Resolver

Now we can also identify what `Engine` is the client using to access your web app starting
in [version 3.0](https://github.com/wangkanai/wangkanai/milestone/13). We got WebKit, Blink, Gecko, Trident, EdgeHTML,
and Servo.

```c#
var isTrident = detectionService.Engine.Name == Engine.Trident;
```

#### Crawler Resolver

This would be something that web analytics to keep track on how are web crawler are access your website for indexing. We
got starting everybody favorite that is Google, Bing, Yahoo, Baidu, Facebook, Twitter, LinkedIn, WhatsApp, and Skype.

```c#
var isGoogle = detectionService.Crawler.Name == Crawler.Google;
```

#### Detection Options

There are basic options that you can add to detection services. Like to adding something that detection does not
identify by default to the `Others` list.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddDetection(options =>
    {
        options.Crawler.Others.Add("goodbot");
    });

    // Add framework services.
    services.AddControllersWithViews();
}
```

## Directory Structure

* `src` - The source code of this project lives here
* `test` - The test code of this project lives here
* `collection` - Collection of sample user agents for lab testing
* `sample` - Contains sample web application of usage
* `doc` - Contains the documentation on how utilized this library

### Contributing

All contribution are welcome, please contact the author.

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/wangkanai/wangkanai/graphs/contributors">
    <img src="https://opencollective.com/wangkanai/contributors.svg?width=890&button=false" />
</a>

### Financial Contributors

Become a financial contributor and help us sustain our
community. [[Contribute](https://opencollective.com/wangkanai/contribute)]

#### Individuals

[![Individuals Contributors](https://opencollective.com/wangkanai/individuals.svg?width=890)](https://opencollective.com/wangkanai)

#### Organizations

Support this project with your organization. Your logo will show up here with a link to your website.
[[Contribute](https://opencollective.com/wangkanai/contribute)]

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
