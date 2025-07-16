# Project Structure

## Module Structure
Each module contains:
- `src/` - Main library source code
- `tests/` - Unit tests
- `benchmark/` - Performance benchmarks
- `samples/` - Sample applications (where applicable)
- `build.ps1` - Module-specific build script

## Common File Structures
- **DependencyInjection/**: Service registration and builder classes
- **Services/**: Core business logic and services
- **Hosting/**: ASP.NET Core middleware and hosting integration
- **Models/**: Data models and DTOs
- **Extensions/**: Extension method classes

## Quality Assurance
- **CI/CD**: GitHub Actions (`.github/workflows/dotnet.yml`)
- **Code Quality**: SonarCloud with `SonarQube.Analysis.xml`
- **Coverage**: Uses `dotnet-coverage` for test coverage reporting
- **Multi-target**: Supports .NET 8.0 and 9.0