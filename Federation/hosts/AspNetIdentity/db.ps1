dotnet ef database drop -f
remove-item -Recurse -Force  .\Data\Migrations
dotnet ef migrations add init -o .\Data\Migrations
dotnet ef database update
