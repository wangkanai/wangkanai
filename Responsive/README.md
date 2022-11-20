## Wangkanai Responsive View

ASP.NET Core Responsive middleware for routing base upon request client device detection to specific view. Also in the
added
feature of user preference made this library even more comprehensive must for developers whom to target multiple devices
with view rendered and optimized directly from the server side.

**Please show me some love and click the** :star:

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.responsive)](https://www.nuget.org/packages/wangkanai.responsive)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.responsive?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.responsive)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

This project development has been in the long making of my little spare time. Please show your appreciation and help me
provide feedback on you think will improve this library. All developers are welcome to come and improve the code by
submit a pull request. We will have constructive good discussion together to the greater good.

- [ASP.NET Core Responsive View](#aspnet-core-detection-with-responsive-view)
- [Installation](#installation)
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

Installation of Responsive library is now done with a single package reference point.

```powershell
PM> install-package Wangkanai.Responsive
```

This library host the component services to resolve the access client device type. To the services your web application
is done by configuring the `Startup.cs` by adding the detection service in the `ConfigureServices` method.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add detection services container and device resolver service.
    services.AddResponsive();

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

* `AddResponsive()` Adds the detection services to the services container.

The current device on a request is set in the Responsive middleware. The Responsive middleware is enabled in
the `Configure` method of `Startup.cs`
file. [Make sure that you have app.UseResponsive() before app.UseRouting](https://github.com/wangkanai/wangkanai/issues/355)
.

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseSession();
    app.UseResponsive();
    
    app.UseRouting();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```razor
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Detection
```

## Responsive Service

This is where thing get more interesting that is built upon detection service, or matter a fact detection service was
built because of responsive service. The concept is that we would like to have views that correspond to what kind of
device to accessing to our web app.

### Responsive MVC

Responsive Views for MVC has 2 format for you to utilize. First is would be to most common is `Suffix` and the secord
format is `SubFolder`. Lets make this follow example a `suffix` as of my opinionated would be the most common way to
managed all the views. This `suffix` format is done by add device type before the file extension **_.cshtml_** like **_
.mobile.cshtml_**. Below is how you would structure your Views folder.

![Responsive view file structure](docs/responsive-views-file-structure.png)

### Responsive Razor Pages

Responsive for razor pages newly added in [wangkanai.detection 3.0](https://github.com/wangkanai/Detection/pull/297).
This enable completed responsive in asp.net core ecosystem. Same like Views in MVC we have `suffix` format where we add
the device type in before the file extension **_.cshtml_** like **_.mobile.cshtml_**.

![Responsive razor pages file structure](docs/responsive-pages-file-structure.png)

### Responsive Tag Helpers

The next exciting feature is Tag Helpers. This make you able to use the same view and just show/hide specific part of the 
views to the client base upon their type, this include Device, Browser, Platform, Engine, and Crawler that our 
detection resolver could determine from
the resolver parsing services.

```razor
<device include="mobile">is mobile</device>
<device exclude="mobile">not mobile</device>
```

```razor
<browser include="chrome">is chrome</browser>
<browser exclude="chrome">not chrome</browser>
```

```razor
<platform include="windows">is windows</platform>
<platform exclude="windows">not windows</platform>
```

```razor
<engine include="blink">is blink</engine>
<engine exclude="blink">not blink</engine>
```

```razor
<crawler include="google">is google</crawler>
<crawler exclude="google">not google</crawler>
```

### User Preference

When a client visit your web application by using a mobile device and you have responsive view for mobile device. But
the visitor would like to view the web app with a desktop view, their click this link to change their preference to
desktop view.

```razor
<a href="/Detection/Preference/Prefer">
    <div class="alert alert-light" role="alert">
        Desktop version
    </div>
</a>
```

If the client selected to view in desktop view, he/she can switch back mobile view by the follow example;

```razor
<preference only="mobile">
    <a href="/Detection/Preference/Clear">
        <div class="alert alert-light" role="alert">
            Switch to mobile version
        </div>
    </a>
</preference>
```

### Responsive Options

You can customize the default behaviour of how responsive service would react to client request. You can go in deep by
examining `ResponsiveOptions`.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add responsive services.
    services.AddResponsive(options =>
    {
        options.DefaultTablet  = Device.Desktop;
        options.DefaultMobile  = Device.Mobile;
        options.DefaultDesktop = Device.Desktop;
        options.IncludeWebApi  = false;
        options.Disable        = false;
        options.WebApiPath     = "/Api";
    });

    // Add framework services.
    services.AddControllersWithViews();
}
```

* `AddResponsive(Action<ResponsiveOptions> options)` Adds the responsive services to the services container.

## Directory Structure

* `src` - The source code of this project lives here
* `tests` - The test code of this project lives here
* `samples` - Contains sample web application of usage
* `docs` - Contains the documentation on how utilized this library

### Contributing

All contribution are welcome, please contact the author.

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/wangkanai/Detection/graphs/contributors">
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
