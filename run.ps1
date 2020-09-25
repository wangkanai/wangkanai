#./dotnet-install.ps1 -version 2.1.810
dotnet --version
dotnet restore
dotnet build -c Release -p:Version=2.0.1
# dotnet test
dotnet pack -c Release -p:PackageVersion=2.0.1 -o C:\Users\sarin\source\Detection\artifacts
