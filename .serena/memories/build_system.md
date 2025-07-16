# Build System and Commands

## Build Dependencies Order
The main `build.ps1` processes modules in dependency order:
1. System, Validation, Annotations, Extensions, Testing
2. Cryptography, Hosting, Tools, Domain, Mvc
3. Webserver, Webmaster, Detection, Responsive
4. EntityFramework, Identity, Security, Federation
5. Markdown, Analytics, Blazor, Tabler
6. Solver, Microservice, Nation

## Common Build Commands
```bash
# Build entire repository
.\build.ps1

# Build individual module (from module directory)
.\build.ps1

# Clean and rebuild specific module
dotnet clean .\src\ -c Release -tl
dotnet build .\src\ -c Release -tl

# Restore dependencies
dotnet restore
```

## Module Build Process
Each module's `build.ps1` follows this pattern:
1. Clean source → Restore → Build source
2. Clean tests → Restore → Build tests