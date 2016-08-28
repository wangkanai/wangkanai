# ASP.NET Core Browser

[![Build status](https://ci.appveyor.com/api/projects/status/nwke0v8dqp3xkgwr/branch/master?svg=true)](https://ci.appveyor.com/project/wangkanai/browser/branch/master) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/Wangkanai.Browser.svg?maxAge=2592000)](https://www.nuget.org/packages/Wangkanai.Browser/)

ASP.NET Core client web browser detection extension to resolve devices, platforms, engine of the client.

### Installation - [NuGet](https://www.nuget.org/packages/Wangkanai.Browser/)

```powershell
PM> install-package Wangkanai.Browser -pre
```

### Implement detection the device for each request

```csharp
var device = new DeviceResolver(context.Request).DeviceInfo;
```

### Directory Structure
* `src` - The code of this project lives here
* `test` - Unit tests of this project to valid that everything pass specs
* `sample` - Contains sample web application of usage

### Contributing

All contribution are welcome, please contact the author.

### See the [LICENSE](https://github.com/wangkanai/Browser/blob/master/LICENSE) file.
