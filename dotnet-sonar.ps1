dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai 
dotnet build -c release
dotnet dotcover test --dcReportType=HTML
dotnet sonarscanner end
