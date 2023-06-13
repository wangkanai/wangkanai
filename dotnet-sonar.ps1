dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.host.url=https://sonarcloud.io /d:sonar.cs.dotcover.reportsPaths=dotCover.Output.html
dotnet build -c release
dotnet dotcover test --dcReportType=HTML
dotnet sonarscanner end
