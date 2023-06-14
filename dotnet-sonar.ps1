dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.host.url=https://sonarcloud.io
dotnet build -c release
dotnet dotcover cover coverage.xml
dotnet dotcover test --dcReportType=HTML
dotnet sonarscanner end
