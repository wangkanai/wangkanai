#./dotnet-install.ps1 -version 2.1.810
dotnet --version
dotnet restore
dotnet build
dotnet publish
dotnet test
dotnet pack -c Release -o .\artifacts
