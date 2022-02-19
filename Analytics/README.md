## Wangkanai Analytics (Alpha Development)

Wangkanai Analytics is a [.NET Core](https://dotnet.github.io/) library extension that tracks and generates details statistics about visitors to your website.
The core feature is to track website activity such as session duration, pages per session, bounce rate and etc. of individuals using the site, along with the information on the source of the traffic.

**Please show some me love and click on** :star: This help movtivate me to continue on developing the project.

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.analytics)](https://www.nuget.org/packages/wangkanai.analytics)
[![NuGet Pre Release](https://buildstats.info/nuget/wangkanai.analytics?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.analytics)


### Installation

Add the NuGet package to your project.

```powershell
PM> install-package Wangkanai.Analytics
```

Add the service to your web app.

```c#
public void ConfigureServices(IServiceCollection services)
{
    // Add application services.
    services.AddAnalytics();

    // Add framework services.
    services.AddControllersWithViews();
}
```

The Analytics middleware is enable in the `Configure` method of *Startup.cs* file.

```c#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseAnalytics();
    
    app.UseRouting();  

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

Adding the TagHelper features to your web application with following in your `_ViewImports.cshtml`

```c#
@using WebApplication1

@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Wangkanai.Analytics
```

### How do i contribute?

Wangkanai Analytics is a powerful and continuous improving platform. We would like to invite developers to help maintain and add features so that this library is keep aligned with most of the popular web analytics out there. 
