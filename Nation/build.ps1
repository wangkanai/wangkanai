dotnet --version
dotnet clean   .\src\ -c Release -tl
dotnet restore .\src\
dotnet build   .\src\ -c Release -tl

dotnet clean   .\tests\ -c Release -tl
dotnet restore .\tests\
dotnet build   .\tests\ -c Release -tl
