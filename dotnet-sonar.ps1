dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.host.url=https://sonarcloud.io  /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
dotnet build -c release
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
dotnet sonarscanner end
