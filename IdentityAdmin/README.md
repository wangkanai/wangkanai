# <img src="https://raw.githubusercontent.com/wangkanai/IdentityAdmin/main/asset/Identity-admin-logo.svg?sanitize=true" width="650" alt="IdentityAdmin" />

***(Alpha Development)***

IdentityAdmin is a free, open source that provides the necessary administration portal for managing [IdentityServer](https://github.com/IdentityServer/) to manage clients and users. IdentityAdmin will allow easier implementation of IdentityServer whom provides no administration tool for managing its configuration.

**Please show some me love and click on** :star: This help movtivate me to continue on developing the project.

[![Build status](https://ci.appveyor.com/api/projects/status/m4sukyo2hyjadg1u?svg=true&retina=true)](https://ci.appveyor.com/project/wangkanai/identityadmin)
[![GitHub](https://img.shields.io/github/license/wangkanai/IdentityAdmin)](https://github.com/wangkanai/IdentityAdmin/blob/dev/LICENSE)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.IdentityAdmin)](https://www.nuget.org/packages/wangkanai.IdentityAdmin)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.IdentityAdmin?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.IdentityAdmin)
[![MyGet Badge](https://buildstats.info/myget/wangkanai/Wangkanai.IdentityAdmin)](https://www.myget.org/feed/wangkanai/package/nuget/Wangkanai.IdentityAdmin)
 
[![Build history](https://buildstats.info/appveyor/chart/wangkanai/IdentityAdmin)](https://ci.appveyor.com/project/wangkanai/detection/history)

## Overview

Installation of IdentityAdmin

```powershell
PM> install-package Wangkanai.IdentityAdmin
```

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the IdentityAdmin service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddIdentityServer()
        .AddAspNetIdentity<ApplicationUser>();

    services.AddIdentityAdmin<ApplicationUser>();
}
```

Adding the IdentityAdmin middleware to the pipeline. The IdentityAdmin middleware is enabled in the `Configure` method of *Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseIdentityServer();

    app.UseIdentityAdmin();

    app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
}
```

### Directory Structure

* `src` - The source code of this project lives here.
* `test` - The unit tests code of this project to valid that everything pass specs.
* `sample` - The sample code of this project lives here. 

### Contributing

All contribution are welcome, please contact the author.
