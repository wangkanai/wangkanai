dotnet ef database drop -f
rmdir /s /q .\Data\Migrations
dotnet ef migrations add init -o .\Data\Migrations
dotnet ef database update
