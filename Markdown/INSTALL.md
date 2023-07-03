## Installation

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