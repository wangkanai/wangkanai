dotnet restore
dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /d:sonar.host.url=https://sonarcloud.io /s:"D:\Sources\Wangkanai\SonarQube.Analysis.xml"
dotnet build -c release
dotnet coverage collect "dotnet test" -f xml -o "coverage.xml"
# dotnet test --collect "Code coverage" 
dotnet sonarscanner end
