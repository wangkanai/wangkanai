## Installation

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
