dotnet --version
dotnet clean   .\src\Federation -c Release -tl
dotnet restore .\src\Federation
dotnet build   .\src\Federation -c Release -tl

dotnet clean   .\tests\ -c Release -tl
dotnet restore .\tests\
dotnet build   .\tests\ -c Release -tl
