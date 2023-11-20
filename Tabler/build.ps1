dotnet --version
dotnet clean   .\src\Core -c Release -tl
dotnet restore .\src\Core
dotnet build   .\src\Core -c Release -tl

dotnet clean   .\tests\ -c Release -tl
dotnet restore .\tests\
dotnet build   .\tests\ -c Release -tl
