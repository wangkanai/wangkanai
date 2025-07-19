# Wangkanai Build System Guide

## Overview

The Wangkanai project uses an enhanced build system that provides comprehensive error handling, parallel execution, and
multiple build configurations. The build system is designed to handle the multi-project structure efficiently.

## Quick Start

### Basic Commands

```powershell
# Production build (clean + optimized)
./build-simple.ps1 prod

# Development build (debug mode with verbose output)
./build-simple.ps1 dev

# Test build (with code coverage)
./build-simple.ps1 test

# Clean all build artifacts
./build-simple.ps1 clean

# Restore all packages
./build-simple.ps1 restore
```

### Original Build Script

For compatibility, the original simple build script is still available:

```powershell
# Original build command
./build.ps1
```

## Enhanced Build System

The enhanced build script (`build-enhanced.ps1`) provides advanced features and fine-grained control.

### Usage

```powershell
./build-enhanced.ps1 [[-Target] <string>] [-Type <string>] [-Clean] [-Optimize] [-Verbose] [-SkipRestore] [-SkipTests] [-Configuration <string>]
```

### Parameters

| Parameter        | Description                   | Default | Values               |
|------------------|-------------------------------|---------|----------------------|
| `-Target`        | Project or component to build | "all"   | Project name or path |
| `-Type`          | Build type                    | "prod"  | dev, prod, test      |
| `-Clean`         | Clean artifacts before build  | False   | Switch               |
| `-Optimize`      | Enable optimizations          | False   | Switch               |
| `-Verbose`       | Detailed output               | False   | Switch               |
| `-SkipRestore`   | Skip package restore          | False   | Switch               |
| `-SkipTests`     | Skip test projects            | False   | Switch               |
| `-Configuration` | Direct config override        | -       | Debug, Release       |

### Build Types

#### Development (`dev`)

- Configuration: Debug
- Includes debug symbols
- No optimizations
- Faster build times

#### Production (`prod`)

- Configuration: Release
- Full optimizations enabled
- No debug symbols
- Treats warnings as errors

#### Test (`test`)

- Configuration: Debug
- Code coverage enabled
- OpenCover format output
- Includes all test projects

### Examples

```powershell
# Build specific module in development mode
./build-enhanced.ps1 -Target Analytics -Type dev -Verbose

# Production build with optimization
./build-enhanced.ps1 -Type prod -Clean -Optimize

# Build without tests
./build-enhanced.ps1 -Type prod -SkipTests

# Parallel optimized build
./build-enhanced.ps1 -Optimize

# Build with custom configuration
./build-enhanced.ps1 -Configuration Debug -Verbose
```

## Features

### Error Handling

- Comprehensive error reporting with colored output
- Detailed diagnostics for build failures
- Exit codes indicate number of failed projects

### Performance Optimization

- Parallel project builds when `-Optimize` is used
- Terminal logger for cleaner output
- Locked restore mode for consistent builds

### Environment Validation

- .NET SDK version checking (requires 9.0+)
- Automatic SDK and runtime listing in verbose mode
- Project discovery and validation

### Build Output

- Colored status indicators:
	- ✓ Success (Green)
	- ✗ Error (Red)
	- ⚠ Warning (Yellow)
- Timing information for each step
- Comprehensive build summary

## Project Structure

The build system automatically discovers projects in the following structure:

```
/ModuleName/
  ├── src/           # Source projects
  ├── tests/         # Test projects
  ├── benchmark/     # Benchmark projects
  └── build.ps1      # Module-specific build (optional)
```

### Supported Project Types

- Source projects: `*.csproj` in `src/` directories
- Test projects: `*Tests.csproj` in `tests/` directories
- Benchmark projects: `*Benchmark.csproj` in `benchmark/` directories

## Build Configuration

### Global Settings (Directory.Build.props)

- Target Framework: .NET 8.0
- Implicit Usings: Enabled
- Nullable Reference Types: Enabled
- Package License: Apache-2.0

### SDK Requirements (global.json)

- Minimum SDK: 9.0.300
- Roll Forward: Latest Major
- Preview SDKs: Allowed

## Troubleshooting

### Common Issues

1. **SDK Not Found**
   ```
   ✗ .NET SDK not found. Please install from https://dot.net
   ```
   Solution: Install .NET SDK 9.0 or higher

2. **Version Mismatch**
   ```
   ✗ Required .NET SDK version 9.0.0 or higher. Current: X.X.X
   ```
   Solution: Update to the latest .NET SDK

3. **Target Not Found**
   ```
   ✗ Target 'XYZ' not found
   ```
   Solution: Verify the module name exists in the project structure

4. **Build Failures**
	- Check the detailed error output
	- Run with `-Verbose` for more information
	- Ensure all package sources are accessible

### Performance Tips

1. Use `-Optimize` for parallel builds on multi-core systems
2. Use `-SkipRestore` if packages are already restored
3. Use `-SkipTests` for faster iteration during development
4. Target specific modules instead of building all projects

## CI/CD Integration

The build system is designed to work seamlessly with CI/CD pipelines:

```powershell
# CI build command
./build-enhanced.ps1 -Type prod -Clean -Optimize

# CD build with tests
./build-enhanced.ps1 -Type test -Clean

# Specific module CI
./build-enhanced.ps1 -Target ModuleName -Type prod -Clean
```

## Module-Specific Builds

Individual modules can have their own `build.ps1` scripts. The enhanced build system will use the global configuration
while respecting module-specific requirements.

## Best Practices

1. **Development Workflow**
	- Use `dev` type for rapid iteration
	- Enable verbose output for debugging
	- Skip tests when not needed

2. **Production Builds**
	- Always use `clean` for production builds
	- Enable optimizations
	- Run full test suite

3. **Continuous Integration**
	- Use deterministic restore with lock files
	- Enable parallel builds
	- Treat warnings as errors

## Advanced Usage

### Custom Build Arguments

Pass additional MSBuild arguments:

```powershell
# Custom MSBuild properties
$env:MSBUILD_ARGS = "/p:CustomProperty=Value"
./build-enhanced.ps1 -Type prod
```

### Build Specific Configuration

For module-specific configurations, create a `Directory.Build.props` in the module folder to override global settings.
