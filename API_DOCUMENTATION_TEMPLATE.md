# Wangkanai Libraries - API Documentation Structure

This document provides a template and guidelines for creating consistent API documentation across all Wangkanai
libraries.

## API Documentation Template

### Module: Wangkanai.[ModuleName]

```markdown
# Wangkanai.[ModuleName] API Reference

## Overview

Brief description of the module's purpose and key functionality.

## Installation

\```bash
dotnet add package Wangkanai.[ModuleName]
\```

## Configuration

### Service Registration

\```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.Add[ModuleName](options =>
    {
        // Configuration options
    });
}
\```

### Middleware Setup (if applicable)

\```csharp
public void Configure(IApplicationBuilder app)
{
    app.Use[ModuleName]();
}
\```

## Core APIs

### Classes

#### [ClassName]

**Namespace**: `Wangkanai.[ModuleName]`

**Description**: Brief description of the class purpose.

**Example**:
\```csharp
var instance = new [ClassName]();
instance.DoSomething();
\```

**Properties**:
| Property | Type | Description |
|----------|------|-------------|
| PropertyName | `string` | Description |

**Methods**:
| Method | Returns | Description |
|--------|---------|-------------|
| `MethodName(params)` | `ReturnType` | Description |

### Interfaces

#### I[InterfaceName]

**Namespace**: `Wangkanai.[ModuleName]`

**Description**: Brief description of the interface purpose.

**Methods**:
| Method | Returns | Description |
|--------|---------|-------------|
| `MethodName(params)` | `ReturnType` | Description |

### Extension Methods

#### [ExtensionClassName]

**Namespace**: `Wangkanai.[ModuleName].Extensions`

**Methods**:
| Method | Extends | Returns | Description |
|--------|---------|---------|-------------|
| `ExtensionMethod(this Type obj)` | `Type` | `ReturnType` | Description |

### Options Classes

#### [ModuleName]Options

**Namespace**: `Wangkanai.[ModuleName].Options`

**Properties**:
| Property | Type | Default | Description |
|----------|------|---------|-------------|
| PropertyName | `string` | `"default"` | Description |

## Advanced Usage

### Scenario 1: [Use Case Name]

Description of the scenario.

\```csharp
// Example code
\```

### Scenario 2: [Use Case Name]

Description of the scenario.

\```csharp
// Example code
\```

## Error Handling

### Common Exceptions

| Exception | Thrown When | How to Handle |
|-----------|-------------|---------------|
| `[ModuleName]Exception` | Description | Solution |

## Performance Considerations

- Point 1 about performance
- Point 2 about performance

## Migration Guide

### From Version X to Y

1. Breaking change 1
2. Breaking change 2

## See Also

- [Related Module 1](../Module1/README.md)
- [Related Module 2](../Module2/README.md)
```

## Documentation Standards

### 1. Namespace Documentation

Every namespace should have XML documentation:

```csharp
/// <summary>
/// The <see cref="Wangkanai.Detection"/> namespace contains types that
/// enable device detection functionality in ASP.NET Core applications.
/// </summary>
namespace Wangkanai.Detection
{
    // ...
}
```

### 2. Class Documentation

```csharp
/// <summary>
/// Provides device detection services for ASP.NET Core applications.
/// </summary>
/// <remarks>
/// This service analyzes HTTP request headers to determine device characteristics.
/// </remarks>
/// <example>
/// <code>
/// var device = detectionService.Device;
/// if (device.Type == DeviceType.Mobile)
/// {
///     // Mobile-specific logic
/// }
/// </code>
/// </example>
public class DetectionService : IDetectionService
{
    // ...
}
```

### 3. Method Documentation

```csharp
/// <summary>
/// Detects the device type from the provided user agent string.
/// </summary>
/// <param name="userAgent">The user agent string to analyze.</param>
/// <returns>The detected device type.</returns>
/// <exception cref="ArgumentNullException">
/// Thrown when <paramref name="userAgent"/> is null.
/// </exception>
public DeviceType DetectDevice(string userAgent)
{
    // ...
}
```

### 4. Property Documentation

```csharp
/// <summary>
/// Gets the current device information.
/// </summary>
/// <value>
/// An instance of <see cref="Device"/> containing device details.
/// </value>
public Device Device { get; }
```

## API Documentation Generation

### Using DocFX

1. Install DocFX:

```bash
dotnet tool install -g docfx
```

2. Create docfx.json in module root:

```json
{
	"metadata": [
		{
			"src": [
				{
					"src": "src",
					"files": [
						"**/*.csproj"
					]
				}
			],
			"dest": "api",
			"filter": "filterConfig.yml"
		}
	],
	"build": {
		"content": [
			{
				"files": [
					"api/**.yml",
					"api/index.md"
				]
			},
			{
				"files": [
					"articles/**.md",
					"*.md",
					"toc.yml"
				]
			}
		],
		"dest": "_site"
	}
}
```

3. Generate documentation:

```bash
docfx metadata
docfx build
```

## Example API Sections by Module Type

### For Service Libraries (e.g., Detection, Analytics)

1. Service Registration
2. Configuration Options
3. Core Service Interface
4. Service Implementation
5. Extension Methods
6. Events and Handlers
7. Middleware Components

### For UI Libraries (e.g., Blazor)

1. Component Registration
2. Component Catalog
3. Component Parameters
4. Event Callbacks
5. Styling and Theming
6. JavaScript Interop

### For Data Libraries (e.g., EntityFramework, Audit)

1. Context Configuration
2. Entity Mappings
3. Query Extensions
4. Migration Support
5. Interceptors
6. Conventions

### For Utility Libraries (e.g., System, Cryptography)

1. Static Methods
2. Extension Methods
3. Helper Classes
4. Constants and Enums
5. Algorithm Implementations

## Cross-Reference Guidelines

### Internal References

Use relative links to reference other modules:

```markdown
See also: [Wangkanai.System](../System/README.md) for base utilities.
```

### External References

Link to official documentation:

```markdown
Based on [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core).
```

### Code References

Reference specific source files:

```markdown
Implementation: [`DetectionService.cs`](src/Services/DetectionService.cs)
```

## Version Documentation

### Compatibility Matrix

| Library Version | .NET Version | Breaking Changes   |
|-----------------|--------------|--------------------|
| 8.0.x           | .NET 8.0     | None               |
| 7.0.x           | .NET 7.0     | Major API revision |
| 6.0.x           | .NET 6.0     | Initial release    |

### Change Log Format

```markdown
## [8.0.1] - 2024-01-15

### Added

- New feature description

### Changed

- Modified behavior description

### Fixed

- Bug fix description

### Deprecated

- Deprecated feature notice

### Removed

- Removed feature notice

### Security

- Security fix description
```

---

This template ensures consistent, high-quality API documentation across all Wangkanai libraries.
