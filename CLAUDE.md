# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

This is a comprehensive .NET monorepo containing 27+ libraries developed by Sarin Na Wangkanai. The repository provides ASP.NET Core extensions and utilities for various use cases including device detection, responsive design, security, federation, markdown processing, and more.

**Key Libraries:**
- **Detection**: Device, browser, engine, platform & crawler detection
- **Responsive**: Responsive web design utilities for ASP.NET Core
- **Blazor**: Custom UI components for Blazor applications
- **Security**: Authentication and authorization extensions
- **Federation**: Identity federation services
- **Markdown**: Markdown parsing and rendering for web applications
- **Domain**: Domain-driven design primitives and patterns
- **Analytics**: User analytics and tracking services

## Development Commands

### Building the Solution
```bash
# Build entire repository (runs all module builds in order)
.\build.ps1

# Build individual module (from module directory)
.\build.ps1
```

### Testing
```bash
# Run tests for specific module (from module directory)
dotnet test .\tests\ -c Release

# Run tests with coverage (used in CI)
dotnet-coverage collect "dotnet test --no-build --verbosity normal" -f xml -o "coverage.xml"

# Run single test class
dotnet test .\tests\ -c Release --filter "ClassName=YourTestClass"

# Run tests with specific pattern
dotnet test .\tests\ -c Release --filter "TestCategory=Unit"
```

### Package Management
- All packages target `.NET 8.0`
- Uses Central Package Management with `Directory.Packages.props`
- NuGet packages are published under the `Wangkanai` namespace

### Debugging & Development
```bash
# Clean and rebuild specific module
dotnet clean .\src\ -c Release -tl
dotnet build .\src\ -c Release -tl

# Restore dependencies for entire solution
dotnet restore

# Build entire solution without incremental builds
dotnet build --no-restore --no-incremental

# Run benchmarks (from module/benchmark directory)
dotnet run -c Release
```

## Architecture & Code Organization

### Repository Structure
- **Monorepo**: Each library is a self-contained module with its own folder
- **Module Structure**: Each module contains:
  - `src/` - Main library source code
  - `tests/` - Unit tests
  - `benchmark/` - Performance benchmarks
  - `samples/` - Sample applications (where applicable)
  - `build.ps1` - Module-specific build script

### Build Dependencies Order
The main `build.ps1` processes modules in dependency order:
1. System, Validation, Annotations, Extensions, Testing
2. Cryptography, Hosting, Tools, Domain, Mvc
3. Webserver, Webmaster, Detection, Responsive
4. EntityFramework, Identity, Security, Federation
5. Markdown, Analytics, Blazor, Tabler
6. Solver, Microservice, Nation

### Module Build Process
Each module's `build.ps1` follows this pattern:
1. `dotnet clean .\src\ -c Release -tl` - Clean source
2. `dotnet restore .\src\` - Restore dependencies
3. `dotnet build .\src\ -c Release -tl` - Build source
4. `dotnet clean .\tests\ -c Release -tl` - Clean tests
5. `dotnet restore .\tests\` - Restore test dependencies
6. `dotnet build .\tests\ -c Release -tl` - Build tests

### Key Patterns
- **Extension Methods**: Extensive use of extension methods for ASP.NET Core services
- **Builder Pattern**: Each library provides a builder interface (e.g., `IDetectionBuilder`, `IResponsiveBuilder`)
- **Service Registration**: Follows ASP.NET Core DI conventions with `AddService()` extension methods
- **Namespace Organization**: Libraries use namespace aliases (e.g., `namespace Microsoft.Extensions.DependencyInjection`)

### Common File Structures
- **DependencyInjection/**: Service registration and builder classes
- **Services/**: Core business logic and services
- **Hosting/**: ASP.NET Core middleware and hosting integration
- **Models/**: Data models and DTOs
- **Extensions/**: Extension method classes

## Code Style & Standards

### File Headers
All source files include this copyright header:
```csharp
// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0
```

### Code Style
- **Indentation**: Tabs (4 spaces width)
- **Braces**: Next line style (Allman)
- **var usage**: Preferred for built-in types and when type is apparent
- **Accessibility**: Required for non-interface members
- **Line endings**: CRLF
- **Max line length**: 300 characters

### Testing Framework
- **xUnit**: Primary testing framework
- **Test Structure**: Follows AAA pattern (Arrange, Act, Assert)
- **Mock Objects**: Custom mock implementations in `Mocks/` folders

## Development Workflow

### Module Development
1. Navigate to specific module directory
2. Run `.\build.ps1` to build and test the module
3. Make changes to `src/` directory
4. Add tests to `tests/` directory
5. Run build script to verify changes

### Adding New Features
1. Follow existing patterns for service registration
2. Implement builder pattern for configuration
3. Add comprehensive unit tests
4. Update benchmarks if performance-critical
5. Follow namespace conventions

### Dependencies
- **Primary Framework**: .NET 8.0
- **ASP.NET Core**: Latest stable version
- **Entity Framework**: For data access libraries
- **Benchmarking**: BenchmarkDotNet for performance testing

## Quality Assurance

### CI/CD Pipeline
- **GitHub Actions**: `.github/workflows/dotnet.yml` runs on main branch
- **SonarCloud**: Code quality analysis with `SonarQube.Analysis.xml` configuration
- **Coverage**: Uses `dotnet-coverage` for test coverage reporting
- **Multi-target**: Supports .NET 8.0 and 9.0

### Code Quality Tools
- **SonarCloud**: Configured to exclude benchmarks, samples, and node_modules
- **EditorConfig**: Comprehensive formatting rules in `.editorconfig`
- **ReSharper**: Detailed code style configuration for consistent formatting
- **Central Package Management**: All package versions managed in `Directory.Packages.props`

### Exclusions for Analysis
- Benchmarks (`**/benchmark/**`)
- Sample applications (`**/samples/**`)
- Node modules (`**/node_modules/**`)
- Build artifacts (`**/BenchmarkDotNet.Artifacts/**`)