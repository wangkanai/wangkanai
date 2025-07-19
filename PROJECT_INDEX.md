# Wangkanai Libraries - Complete Project Index

> A comprehensive .NET library ecosystem providing essential components for modern web applications, from device detection to security, UI components, and infrastructure utilities.

## 📚 Table of Contents

1. [Overview](#overview)
2. [Quick Start](#quick-start)
3. [Module Categories](#module-categories)
4. [Module Index](#module-index)
5. [Architecture](#architecture)
6. [Contributing](#contributing)

## Overview

The Wangkanai Libraries form a modular monorepo containing 37 specialized libraries designed to work seamlessly together. Built on .NET 8.0, these libraries provide enterprise-grade solutions for common development challenges.

### Key Statistics
- **Total Libraries**: 37
- **Total Projects**: 100+
- **NuGet Downloads**: 6.5M+ (Detection library alone)
- **Target Framework**: .NET 8.0
- **Architecture**: Modular monorepo with centralized configuration

## Quick Start

### Installation

Install individual packages as needed:

```bash
# Core utilities
dotnet add package Wangkanai.System
dotnet add package Wangkanai.Domain

# Detection and responsive design
dotnet add package Wangkanai.Detection
dotnet add package Wangkanai.Responsive

# UI Components
dotnet add package Wangkanai.Blazor
dotnet add package Wangkanai.Tabler

# Security
dotnet add package Wangkanai.Identity
dotnet add package Wangkanai.Security
```

### Basic Usage

```csharp
// Configure services
services.AddDetection();
services.AddResponsive();
services.AddIdentity();

// Use in middleware
app.UseDetection();
app.UseResponsive();
```

## Module Categories

### 🔧 Core Libraries
Essential utilities and base functionality that other libraries depend on.

### 🎨 UI & Presentation
Components for building modern, responsive user interfaces.

### 🔒 Security & Identity
Authentication, authorization, and security infrastructure.

### 💾 Data & Infrastructure
Database, hosting, and infrastructure support.

### 🧩 Extensions
Specialized extensions for various .NET components.

### 🛠️ Development Tools
Tools to enhance development productivity and code quality.

### 🌍 Business Domain
Domain-specific libraries for business logic.

## Module Index

### Core Libraries

#### [Wangkanai.System](System/README.md)
Core system utilities and extension methods providing foundational functionality.
- **Purpose**: Base utilities, extensions, and helpers
- **Key Features**: String extensions, collection helpers, type utilities
- **Dependencies**: None (root dependency)
- **Package**: `Wangkanai.System`

#### [Wangkanai.Shared](Shared/README.md)
Internal shared components used across multiple libraries.
- **Purpose**: Common internal utilities
- **Key Features**: Shared helpers, internal APIs
- **Dependencies**: None
- **Package**: Internal only

#### [Wangkanai.Domain](Domain/README.md)
Domain-Driven Design base classes and patterns.
- **Purpose**: DDD building blocks
- **Key Features**: Entity base classes, value objects, domain events
- **Dependencies**: System
- **Package**: `Wangkanai.Domain`

#### [Wangkanai.Annotations](Annotations/README.md)
Custom attributes and annotations for enhanced functionality.
- **Purpose**: Validation and metadata attributes
- **Key Features**: Custom validation attributes, metadata markers
- **Dependencies**: System
- **Package**: `Wangkanai.Annotations`

### UI & Presentation

#### [Wangkanai.Detection](Detection/README.md) ⭐ Featured
Industry-leading device detection library for ASP.NET Core.
- **Purpose**: Client device, browser, and platform detection
- **Key Features**: User agent parsing, device classification, bot detection
- **Downloads**: 6.5M+
- **Dependencies**: Hosting, System
- **Package**: `Wangkanai.Detection`

#### [Wangkanai.Responsive](Responsive/README.md)
Adaptive responsive design based on device detection.
- **Purpose**: Device-specific view rendering
- **Key Features**: Responsive view locations, device-based layouts
- **Dependencies**: Detection
- **Package**: `Wangkanai.Responsive`

#### [Wangkanai.Blazor](Blazor/README.md)
Custom Blazor components and utilities.
- **Purpose**: Reusable Blazor components
- **Structure**: Core, Components, Components.Web
- **Dependencies**: System
- **Package**: `Wangkanai.Blazor`

#### [Wangkanai.Tabler](Tabler/README.md)
Modern dashboard and admin panel components based on Tabler.
- **Purpose**: Admin UI components
- **Structure**: Core, Components, Components.Web
- **Features**: Dashboard layouts, forms, charts
- **Package**: `Wangkanai.Tabler`

#### [Wangkanai.Markdown](Markdown/README.md)
Markdown parsing and rendering for ASP.NET Core.
- **Purpose**: Markdown content management
- **Features**: Markdown to HTML, syntax highlighting
- **Dependencies**: Detection, Mvc
- **Package**: `Wangkanai.Markdown`

#### [Wangkanai.Mvc](Mvc/README.md)
MVC extensions and enhancements.
- **Purpose**: MVC framework extensions
- **Features**: Action filters, model binding, routing
- **Dependencies**: Extensions.Internal, Shared
- **Package**: `Wangkanai.Mvc`

#### [Wangkanai.Webmaster](Webmaster/README.md)
SEO and web optimization tools.
- **Purpose**: SEO optimization, web tools
- **Features**: Meta tags, sitemaps, Gravatar, canonical URLs
- **Dependencies**: Multiple (System, Detection, Hosting, etc.)
- **Package**: `Wangkanai.Webmaster`

### Security & Identity

#### [Wangkanai.Identity](Identity/README.md)
Authentication and authorization utilities.
- **Purpose**: Identity management helpers
- **Features**: Identity constants, JWT utilities, OAuth support
- **Dependencies**: Domain
- **Package**: `Wangkanai.Identity`

#### [Wangkanai.Security](Security/README.md)
Comprehensive security components.
- **Purpose**: Security infrastructure
- **Structure**: Core, Authentication, Authorization
- **Features**: Security policies, authentication handlers
- **Dependencies**: System
- **Package**: `Wangkanai.Security.*`

#### [Wangkanai.Federation](Federation/README.md)
Identity provider federation support.
- **Purpose**: Multi-provider authentication
- **Structure**: Multiple sub-projects for different scenarios
- **Features**: SSO, identity federation
- **Dependencies**: Identity, Cryptography, System
- **Package**: `Wangkanai.Federation`

#### [Wangkanai.Cryptography](Cryptography/README.md)
Cryptographic utilities and helpers.
- **Purpose**: Encryption and hashing utilities
- **Features**: Hash algorithms, encryption helpers
- **Dependencies**: System
- **Package**: `Wangkanai.Cryptography`

### Data & Infrastructure

#### [Wangkanai.EntityFramework](EntityFramework/README.md)
Entity Framework Core extensions.
- **Purpose**: EF Core enhancements
- **Features**: Audit support, query extensions
- **Dependencies**: Domain, Hosting
- **Package**: `Wangkanai.EntityFramework`

#### [Wangkanai.Audit](Audit/README.md)
Comprehensive audit logging functionality.
- **Purpose**: Data change tracking
- **Features**: Audit trails, change history
- **Dependencies**: EntityFramework, Domain
- **Package**: `Wangkanai.Audit`

#### [Wangkanai.Hosting](Hosting/README.md)
ASP.NET Core hosting configuration helpers.
- **Purpose**: Hosting setup utilities
- **Features**: Host configuration, startup helpers
- **Dependencies**: System
- **Package**: `Wangkanai.Hosting`

#### [Wangkanai.Webserver](Webserver/README.md)
Web server configuration and utilities.
- **Purpose**: Server configuration
- **Features**: Security headers, compression
- **Dependencies**: System
- **Package**: `Wangkanai.Webserver`

#### [Wangkanai.Microservice](Microservice/README.md)
Microservice patterns and utilities.
- **Purpose**: Microservice infrastructure
- **Features**: Service discovery, communication patterns
- **Dependencies**: System
- **Package**: `Wangkanai.Microservice`

### Extensions

#### [Wangkanai.Extensions.FileProviders](Extensions/FileProviders/README.md)
File provider extensions and utilities.
- **Purpose**: Enhanced file operations
- **Features**: Custom file providers
- **Dependencies**: System
- **Package**: `Wangkanai.Extensions.FileProviders`

#### [Wangkanai.Extensions.Html](Extensions/Html/README.md)
HTML manipulation and generation utilities.
- **Purpose**: HTML helpers
- **Features**: HTML generation, manipulation
- **Dependencies**: System
- **Package**: `Wangkanai.Extensions.Html`

#### [Wangkanai.Extensions.Internal](Extensions/Internal/README.md)
Internal extension utilities.
- **Purpose**: Shared internal extensions
- **Features**: Internal APIs
- **Dependencies**: System
- **Package**: Internal only

### Development Tools

#### [Wangkanai.Testing](Testing/README.md)
Testing utilities and base classes.
- **Purpose**: Test infrastructure
- **Features**: Test helpers, mocking utilities
- **Dependencies**: System
- **Package**: `Wangkanai.Testing`

#### [Wangkanai.Validation](Validation/README.md)
Advanced validation framework.
- **Purpose**: Data validation
- **Features**: Custom validators, validation rules
- **Dependencies**: System
- **Package**: `Wangkanai.Validation`

#### [Wangkanai.Analytics](Analytics/README.md)
Analytics integration and tracking.
- **Purpose**: Usage analytics
- **Features**: Analytics providers integration
- **Dependencies**: Detection
- **Package**: `Wangkanai.Analytics`

#### [Wangkanai.Tools.MSBuild](Tools/MSBuild/README.md)
Custom MSBuild tasks and targets.
- **Purpose**: Build automation
- **Features**: Custom build tasks
- **Dependencies**: None
- **Package**: `Wangkanai.MSBuild`

#### [Wangkanai.Tools.Watcher](Tools/Watcher/README.md)
File system watcher utilities.
- **Purpose**: File monitoring
- **Features**: File change detection
- **Dependencies**: None
- **Package**: `Wangkanai.Watcher`

### Business Domain

#### [Wangkanai.Nation](Nation/README.md)
Localization and country/region data.
- **Purpose**: Geographic data management
- **Features**: Country data, regions, cities
- **Dependencies**: Domain, EntityFramework
- **Package**: `Wangkanai.Nation`

#### [Wangkanai.Solver](Solver/README.md)
Problem-solving utilities and algorithms.
- **Purpose**: Algorithm implementations
- **Features**: Linear programming, optimization
- **Dependencies**: System
- **Package**: `Wangkanai.Solver`

### Application Support

#### [Wangkanai.Web](Web/README.md)
Web application support utilities.
- **Purpose**: Web app helpers
- **Features**: Web-specific utilities
- **Dependencies**: Various
- **Package**: Project-specific

#### [Assets](Assets/README.md)
Shared assets and resources.
- **Purpose**: Common assets
- **Features**: Images, icons, resources
- **Dependencies**: None
- **Package**: Internal only

## Architecture

### Dependency Hierarchy

```
Layer 1 (Core - No Dependencies):
├── System
├── Shared
└── Annotations

Layer 2 (Foundation):
├── Domain → System
├── Hosting → System
├── Cryptography → System
├── Validation → System
└── Testing → System

Layer 3 (Infrastructure):
├── Detection → Hosting, System
├── Identity → Domain
├── EntityFramework → Domain, Hosting
└── Extensions.* → System

Layer 4 (Features):
├── Responsive → Detection
├── Analytics → Detection
├── Audit → EntityFramework, Domain
└── Webmaster → Multiple dependencies

Layer 5 (Complex Components):
├── Federation → Identity, Cryptography, System
├── Security → System (with sub-projects)
├── Blazor → Multiple projects
└── Tabler → Multiple projects
```

### Design Principles

1. **Modular Architecture**: Each library has a specific purpose and minimal dependencies
2. **Consistent Structure**: All modules follow the same project organization (src, tests, benchmark)
3. **Centralized Configuration**: Shared build properties and package versions
4. **Comprehensive Testing**: Every library includes unit tests and benchmarks
5. **Clean Dependencies**: No circular dependencies, clear hierarchy

## Contributing

### Building from Source

```bash
# Clone the repository
git clone https://github.com/wangkanai/wangkanai.git

# Build all projects
./build.ps1

# Run tests
dotnet test

# Build specific module
cd Detection
./build.ps1
```

### Development Guidelines

1. Follow existing project structure
2. Add tests for new features
3. Include benchmarks for performance-critical code
4. Update README.md for your module
5. Ensure no circular dependencies

### Quality Standards

- Target .NET 8.0
- Enable nullable reference types
- Follow existing coding conventions
- Maintain 60%+ test coverage
- Document public APIs

---

*For detailed information about each module, click on the module name to view its specific documentation.*

*Last updated: [Current Date]*