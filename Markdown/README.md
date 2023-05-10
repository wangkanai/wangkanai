## ASP.NET Core Markdown Pages

ASP.NET Core Markdown Pages make you able to use markdown pages as an alternative view source in ASP.NET Core view
render engine

**Please show some me love and click on** :star: This help motivate me to continue on developing the project.

[![NuGet Badge](https://buildstats.info/nuget/wangkanai.markdown)](https://www.nuget.org/packages/wangkanai.markdown)
[![NuGet Badge](https://buildstats.info/nuget/wangkanai.markdown?includePreReleases=true)](https://www.nuget.org/packages/wangkanai.markdown)

[![Build Status](https://dev.azure.com/wangkanai/GitHub/_apis/build/status/wangkanai?branchName=main)](https://dev.azure.com/wangkanai/GitHub/_build/latest?definitionId=20&branchName=main)
[![Open Collective](https://img.shields.io/badge/open%20collective-support%20me-3385FF.svg)](https://opencollective.com/wangkanai)
[![Patreon](https://img.shields.io/badge/patreon-support%20me-d9643a.svg)](https://www.patreon.com/wangkanai)
[![GitHub](https://img.shields.io/github/license/wangkanai/wangkanai)](https://github.com/wangkanai/wangkanai/blob/main/LICENSE)

## Overview

Installation of Markdown

```shell
PM> install-package Wangkanai.Markdown
```

Implement of the library into your web application is done by configuring the `Startup.cs` by adding the IdentityAdmin
service in the `ConfigureServices` method.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMarkdownPages();    
}
```

* `AddMarkdownPages()` Adds the markdown services to the services container.

Adding the Markdown middleware to the pipeline. The Markdown middleware is enabled in the `Configure` method of *
Startup.cs* file.

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapMarkdownPages();
}
```

### Directory Structure

* `src` - The code of this project lives here
* `test` - The unit tests of this project to valid that everything pass specs
* `sample` - The sample code of this project lives here

### Contributing

All contribution are welcome, please contact the author.
