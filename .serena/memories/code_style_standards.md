# Code Style & Standards

## File Headers
All source files include this copyright header:
```csharp
// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0
```

## Code Style Guidelines
- **Indentation**: Tabs (4 spaces width)
- **Braces**: Next line style (Allman)
- **var usage**: Preferred for built-in types and when type is apparent
- **Accessibility**: Required for non-interface members
- **Line endings**: CRLF
- **Max line length**: 300 characters

## Key Patterns
- **Extension Methods**: Extensive use for ASP.NET Core services
- **Builder Pattern**: Each library provides builder interface (e.g., `IDetectionBuilder`)
- **Service Registration**: Follows ASP.NET Core DI conventions with `AddService()` methods
- **Namespace Organization**: Libraries use namespace aliases