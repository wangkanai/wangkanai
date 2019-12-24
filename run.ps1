./dotnet-install.ps1 -Channel Current -version 2.0.100
dotnet --version
dotnet restore
dotnet build
dotnet test
dotnet pack -c Release -o .\artifacts
