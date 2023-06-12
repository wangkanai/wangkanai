dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.host.url=https://sonarcloud.io

dotnet restore

dotnet build -c release

dotnet dotcover test --dcReportType=HTML

dotnet sonarscanner end