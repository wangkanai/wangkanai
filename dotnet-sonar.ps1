rimraf .sonarqube/out
dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.cs.dotcover.reportsPaths=dotCover.Output.html
dotnet build  –no-incremental
dotnet dotcover test --dcReportType=HTML
dotnet sonarscanner end