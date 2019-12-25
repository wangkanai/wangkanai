#./dotnet-install.ps1 -version 3.1.100
dotnet --version
dotnet restore
dotnet build
dotnet test
dotnet pack -c Release -o .\artifacts
