./dotnet-install.ps1 -version 2.1.202
dotnet --version
dotnet restore
dotnet build
dotnet test
dotnet pack .\src\**\*.csproj -c Release -o .\artifacts
