dotnet --version
dotnet restore
dotnet build
dotnet test .\test\*
dotnet pack -c Release -o .\artifacts