## Wangkanai Analytics (Alpha Development)

Wangkanai Analytics is a [.NET Core](https://dotnet.github.io/) library extension that tracks and generates details statistics about visitors to your website.
The core feature is to track website activity such as session duration, pages per session, bounce rate and etc. of individuals using the site, along with the information on the source of the traffic.

**Please show some me love and click on** :star: This help movtivate me to continue on developing the project.

[![GitHub](https://github.com/wangkanai/analytics/workflows/Analytics-CI/badge.svg)](https://github.com/wangkanai/analytics/actions)
[![Build status](https://ci.appveyor.com/api/projects/status/t46adtm386rxiqam?svg=true)](https://ci.appveyor.com/project/wangkanai/analytics)

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.analytics)](https://www.nuget.org/packages/wangkanai.analytics)
[![NuGet Pre Release](https://buildstats.info/nuget/wangkanai.analytics?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.analytics)
[![MyGet Badge](https://buildstats.info/myget/wangkanai/wangkanai.analytics)](https://www.myget.org/feed/wangkanai/package/nuget/wangkanai.analytics)

[![Build history](https://buildstats.info/appveyor/chart/wangkanai/analytics)](https://ci.appveyor.com/project/wangkanai/analytics/history)

### Installation

Add the NuGet package to your project.

```
PM> install-package Wangkanai.Analytics
```

Add the service to your web app.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Add application services.
    services.AddAnalytics();

    // Add framework services.
    services.AddMvc();    
}
```

### How do i contribute?

Wangkanai Analytics is a powerful and continuous improving platform. We would like to invite developers to help maintain and add features so that this library is keep aligned with most of the popular web analytics out there. 
