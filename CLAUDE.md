# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Overview

This is a comprehensive .NET monorepo containing 27+ libraries developed by Sarin Na Wangkanai. The repository provides
ASP.NET Core extensions and utilities for various use cases including device detection, responsive design, security,
federation, markdown processing, and more.

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

The solution uses xUnit v3 with Microsoft Testing Platform (MTP) for improved performance and reduced overhead.

```bash
# Run tests for specific module (from module directory)
dotnet test .\tests\ -c Release

# Run tests directly with Microsoft Testing Platform (faster)
.\tests\bin\Release\net8.0\YourProject.Tests.exe

# Run tests with coverage (Coverlet)
pwsh ./test-coverage.ps1

# Run tests with coverage for specific project
dotnet test ./path/to/tests.csproj -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage/

# Run all tests with merged coverage
dotnet test -c Release /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=./coverage/ /p:MergeWith=./coverage/coverage.json

# Run single test class
dotnet test .\tests\ -c Release --filter "ClassName=YourTestClass"

# Run tests with specific pattern
dotnet test .\tests\ -c Release --filter "TestCategory=Unit"

# MTP-specific options (when running executable directly)
.\tests\bin\Release\net8.0\YourProject.Tests.exe --list-tests
.\tests\bin\Release\net8.0\YourProject.Tests.exe --filter "FullyQualifiedName~YourTestClass"
```

**Test Configuration:**
- Central `testconfig.json` in repository root for Microsoft Testing Platform configuration
- Microsoft Testing Platform enabled for all test projects
- Parallel test collection execution enabled
- Console and trace output capture enabled

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

### Available MCP Commands (By Priority & Efficiency)

#### **🔴 CRITICAL PRIORITY - Codebase Analysis & Knowledge Tools**

##### **🚀 REPOMIX INTEGRATION - PRIMARY CODEBASE ANALYSIS**

**USE FIRST for any code exploration, understanding, or modification tasks!**

```bash
# CORE WORKFLOW (Always start here!)
mcp__repomix__pack_codebase                         # Generate complete codebase analysis
mcp__repomix__grep_repomix_output                   # Fast pattern search (most efficient)
mcp__repomix__read_repomix_output                   # Selective content reading

# EXTENDED CAPABILITIES
mcp__repomix__pack_remote_repository                # Analyze external GitHub repos
mcp__repomix__file_system_read_file                 # Direct file access (with security checks)
mcp__repomix__file_system_read_directory            # Directory listing

# EFFICIENCY TIPS
# 1. Always pack first: mcp__repomix__pack_codebase
# 2. Use grep for discovery: mcp__repomix__grep_repomix_output "pattern"
# 3. Read selectively: mcp__repomix__read_repomix_output --startLine=X --endLine=Y
```

##### **Common Repomix Search Patterns**

```bash
# Architecture Discovery (High-level understanding)
mcp__repomix__grep_repomix_output "interface.*Service|class.*Controller" --contextLines=2
mcp__repomix__grep_repomix_output "public.*static.*class.*Extensions" --contextLines=1
mcp__repomix__grep_repomix_output "namespace.*DependencyInjection"

# Implementation Patterns (Code patterns)
mcp__repomix__grep_repomix_output "AddRequiredServices|AddCoreServices|AddMarkerService"
mcp__repomix__grep_repomix_output "public.*static.*IServiceCollection.*Add"
mcp__repomix__grep_repomix_output "Configure.*<.*>|Options.*<.*>"

# Quality Checks (Issues & TODOs)
mcp__repomix__grep_repomix_output "TODO|FIXME|BUG|HACK" --ignoreCase=true
mcp__repomix__grep_repomix_output "ThrowIfNull|ArgumentNullException"
mcp__repomix__grep_repomix_output "obsolete|deprecated" --ignoreCase=true

# Testing Patterns (Test discovery)
mcp__repomix__grep_repomix_output "public.*class.*Tests|TestBase"
mcp__repomix__grep_repomix_output "\[Fact\]|\[Theory\]|\[Test\]"
mcp__repomix__grep_repomix_output "Mock<.*>|Substitute\.For"
```

#### **🟠 HIGH PRIORITY - Direct Development Tools**

##### **JetBrains IDE Integration** (Active File Editing)

```bash
# Current file operations (Most efficient for active editing)
mcp__jetbrains__get_open_in_editor_file_text        # Read current file
mcp__jetbrains__get_selected_in_editor_text         # Get selection
mcp__jetbrains__replace_selected_text               # Replace selection
mcp__jetbrains__replace_current_file_text           # Replace entire file
mcp__jetbrains__replace_specific_text               # Find & replace (PREFERRED)

# File management
mcp__jetbrains__create_new_file_with_text           # Create new files
mcp__jetbrains__get_file_text_by_path               # Read by path
mcp__jetbrains__replace_file_text_by_path           # Update by path
mcp__jetbrains__open_file_in_editor                 # Open in editor

# Project navigation (Use Repomix grep first for efficiency!)
mcp__jetbrains__find_files_by_name_substring        # Find files by name
mcp__jetbrains__list_files_in_folder                # List directory
mcp__jetbrains__list_directory_tree_in_folder       # Tree view
mcp__jetbrains__search_in_files_content             # Text search (prefer Repomix)

# Code quality & debugging
mcp__jetbrains__get_current_file_errors             # Current file errors
mcp__jetbrains__get_project_problems                # All project issues
mcp__jetbrains__toggle_debugger_breakpoint          # Set breakpoints
mcp__jetbrains__reformat_current_file               # Format code

# Build & execution
mcp__jetbrains__get_run_configurations              # List run configs
mcp__jetbrains__run_configuration                   # Execute config
mcp__jetbrains__execute_terminal_command            # Run commands
```

##### **GitHub Integration** (Version Control & Collaboration)

```bash
# Essential operations
mcp__github__get_file_contents                      # Read files from GitHub
mcp__github__create_or_update_file                  # Update single file
mcp__github__push_files                             # Batch file updates
mcp__github__create_pull_request                    # Create PRs
mcp__github__create_branch                          # Branch management

# Repository management
mcp__github__search_repositories                    # Find repos
mcp__github__list_commits                           # View history
mcp__github__get_pull_request                       # PR details
mcp__github__list_issues                            # Track issues
```

#### **🟡 MEDIUM PRIORITY - Quality & Analysis Tools**

##### **SonarQube** (Code Quality Metrics)

```bash
# Project ID: wangkanai_github

# Use after code changes for quality checks
mcp__sonarqube__analyze_code_snippet                # Analyze new code
mcp__sonarqube__search_sonar_issues_in_projects     # Find issues
mcp__sonarqube__get_project_quality_gate_status     # Quality gates

# Example commands for this repository
mcp__sonarqube__get_project_quality_gate_status --projectKey="wangkanai_github"
mcp__sonarqube__search_sonar_issues_in_projects --projects=["wangkanai_github"]
mcp__sonarqube__analyze_code_snippet --projectKey="wangkanai_github" --codeSnippet="..." --language="cs"
```

##### **Hugging Face** (ML/AI Resources)

```bash
# Documentation and model search
mcp__huggingface__hf_doc_search                     # Search HF docs
mcp__huggingface__model_search                      # Find models
mcp__huggingface__dataset_search                    # Find datasets
```

##### **Microsoft Docs** (Official .NET Documentation)

```bash
# Primary command
mcp__ms-docs__microsoft_docs_search                 # Search official Microsoft/Azure documentation

# .NET Core/Framework Documentation
mcp__ms-docs__microsoft_docs_search ".NET 8 features"
mcp__ms-docs__microsoft_docs_search "C# 12 new features"
mcp__ms-docs__microsoft_docs_search ".NET performance best practices"
mcp__ms-docs__microsoft_docs_search "dotnet CLI commands"

# ASP.NET Core Documentation
mcp__ms-docs__microsoft_docs_search "ASP.NET Core middleware"
mcp__ms-docs__microsoft_docs_search "ASP.NET Core dependency injection"
mcp__ms-docs__microsoft_docs_search "ASP.NET Core routing"
mcp__ms-docs__microsoft_docs_search "ASP.NET Core security"
mcp__ms-docs__microsoft_docs_search "minimal APIs ASP.NET Core"

# Blazor Documentation
mcp__ms-docs__microsoft_docs_search "Blazor components"
mcp__ms-docs__microsoft_docs_search "Blazor Server vs WebAssembly"
mcp__ms-docs__microsoft_docs_search "Blazor state management"
mcp__ms-docs__microsoft_docs_search "Blazor JavaScript interop"
mcp__ms-docs__microsoft_docs_search "Blazor forms validation"

# .NET MAUI Documentation
mcp__ms-docs__microsoft_docs_search "MAUI getting started"
mcp__ms-docs__microsoft_docs_search "MAUI layouts"
mcp__ms-docs__microsoft_docs_search "MAUI platform-specific code"
mcp__ms-docs__microsoft_docs_search "MAUI data binding"
mcp__ms-docs__microsoft_docs_search "MAUI navigation"

# Entity Framework Core Documentation
mcp__ms-docs__microsoft_docs_search "EF Core migrations"
mcp__ms-docs__microsoft_docs_search "EF Core relationships"
mcp__ms-docs__microsoft_docs_search "EF Core performance"
mcp__ms-docs__microsoft_docs_search "EF Core LINQ queries"
mcp__ms-docs__microsoft_docs_search "EF Core database providers"

# ASP.NET Core Identity Documentation
mcp__ms-docs__microsoft_docs_search "ASP.NET Core Identity setup"
mcp__ms-docs__microsoft_docs_search "Identity authentication"
mcp__ms-docs__microsoft_docs_search "Identity authorization policies"
mcp__ms-docs__microsoft_docs_search "Identity two-factor authentication"
mcp__ms-docs__microsoft_docs_search "Identity external providers"

# ML.NET Documentation
mcp__ms-docs__microsoft_docs_search "ML.NET getting started"
mcp__ms-docs__microsoft_docs_search "ML.NET model training"
mcp__ms-docs__microsoft_docs_search "ML.NET sentiment analysis"
mcp__ms-docs__microsoft_docs_search "ML.NET AutoML"
mcp__ms-docs__microsoft_docs_search "ML.NET model deployment"

# C# Language Documentation
mcp__ms-docs__microsoft_docs_search "C# pattern matching"
mcp__ms-docs__microsoft_docs_search "C# async await best practices"
mcp__ms-docs__microsoft_docs_search "C# record types"
mcp__ms-docs__microsoft_docs_search "C# nullable reference types"
mcp__ms-docs__microsoft_docs_search "C# LINQ operators"
```

#### **🟢 LOW PRIORITY - Utility Tools**

##### **Container Management** (Podman/Docker)

```bash
mcp__podman__container_run                          # Run containers
mcp__podman__image_build                            # Build images
mcp__podman__container_logs                         # View logs
```

##### **Memory & Knowledge Graph** (Context Persistence)

```bash
mcp__memory__create_entities                        # Store knowledge
mcp__memory__search_nodes                           # Query knowledge
```

##### **Web & Sequential Thinking** (External Resources)

```bash
mcp__fetch__fetch                                   # Fetch URLs
mcp__sequential-thinking__sequentialthinking        # Complex reasoning
```

#### **🔵 RARELY USED - System Tools**

##### **IDE & MCP Infrastructure**

```bash
mcp__ide__getDiagnostics                            # IDE diagnostics
ListMcpResourcesTool                                # List MCP resources
ReadMcpResourceTool                                 # Read MCP resource
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
- **Namespace Organization**: Libraries use namespace aliases (e.g.,
  `namespace Microsoft.Extensions.DependencyInjection`)

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

- **xUnit v3**: Primary testing framework with Microsoft Testing Platform integration
- **Test Structure**: Follows AAA pattern (Arrange, Act, Assert)
- **Mock Objects**: Custom mock implementations in `Mocks/` folders
- **Test Execution**: Supports both `dotnet test` and direct executable execution
- **Performance**: ~30-40% faster test execution with MTP compared to VSTest

## Development Workflow

### **REPOMIX-FIRST DEVELOPMENT APPROACH**

#### **Phase 1: Codebase Analysis (ALWAYS START HERE)**

```bash
# 1. Generate current codebase analysis
mcp__repomix__pack_codebase --directory="/Users/wangkanai/Sources/wangkanai"

# 2. Architecture discovery patterns
mcp__repomix__grep_repomix_output "interface.*Service|class.*Controller|public.*Builder"
mcp__repomix__grep_repomix_output "public.*static.*class.*Extensions"
mcp__repomix__grep_repomix_output "Configure.*<.*>"

# 3. Find implementation patterns
mcp__repomix__grep_repomix_output "AddRequiredServices|AddCoreServices|AddMarkerService"
mcp__repomix__grep_repomix_output "ThrowIfNull|ArgumentNullException"
```

#### **Phase 2: Targeted Analysis**

```bash
# 4. Selective content reading (efficient token usage)
mcp__repomix__read_repomix_output --startLine=1000 --endLine=1200

# 5. Cross-module consistency checks
mcp__repomix__grep_repomix_output "namespace.*DependencyInjection"
mcp__repomix__grep_repomix_output "IServiceCollection.*Services"
```

#### **Phase 3: Implementation**

```bash
# 6. Pattern-based implementation guidance
mcp__repomix__grep_repomix_output "class.*Builder.*IServiceCollection"
mcp__repomix__grep_repomix_output "public.*static.*Add.*Builder"
```

### **Traditional Module Development (Use After Repomix Analysis)**

1. Navigate to specific module directory
2. Run `.\build.ps1` to build and test the module
3. Make changes to `src/` directory
4. Add tests to `tests/` directory
5. Run build script to verify changes

### **REPOMIX-ENHANCED FEATURE DEVELOPMENT**

#### **Before Writing Code - Pattern Discovery**

```bash
# 1. Find existing similar implementations
mcp__repomix__grep_repomix_output "class.*YourFeature.*Builder"
mcp__repomix__grep_repomix_output "I.*YourFeature.*Service"

# 2. Discover service registration patterns
mcp__repomix__grep_repomix_output "AddYourFeature.*IServiceCollection"
mcp__repomix__grep_repomix_output "UseYourFeature.*IApplicationBuilder"

# 3. Find testing patterns
mcp__repomix__grep_repomix_output "public.*class.*YourFeature.*Tests"
mcp__repomix__grep_repomix_output "AddYourFeature.*Builder.*Null.*ArgumentNullException"
```

#### **Implementation Guidelines**

1. **Follow discovered patterns** from repomix analysis
2. **Implement builder pattern** based on existing examples
3. **Add comprehensive unit tests** following found test patterns
4. **Update benchmarks** if performance-critical
5. **Follow namespace conventions** discovered in analysis

#### **Quality Assurance Patterns**

```bash
# 4. Verify implementation consistency
mcp__repomix__grep_repomix_output "ThrowIfNull.*builder"
mcp__repomix__grep_repomix_output "Services.*TryAdd.*"
mcp__repomix__grep_repomix_output "public.*static.*class.*Extensions"
```

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

## REPOMIX-ENHANCED ANALYSIS PROCEDURES

### **Systematic Code Analysis Protocol**

#### **1. Initial Repository Analysis**

```bash
# Always start with fresh analysis
mcp__repomix__pack_codebase --directory="/Users/wangkanai/Sources/wangkanai"

# Get repository overview
mcp__repomix__read_repomix_output --startLine=1 --endLine=100
```

#### **2. Architecture Discovery**

```bash
# Service layer analysis
mcp__repomix__grep_repomix_output "interface.*Service" --contextLines=2
mcp__repomix__grep_repomix_output "class.*Service.*implements" --contextLines=2

# Builder pattern analysis
mcp__repomix__grep_repomix_output "class.*Builder.*IServiceCollection" --contextLines=3
mcp__repomix__grep_repomix_output "public.*static.*Add.*Builder" --contextLines=2

# Extension methods discovery
mcp__repomix__grep_repomix_output "public.*static.*class.*Extensions" --contextLines=1
```

#### **3. Cross-Module Consistency Verification**

```bash
# DI registration patterns
mcp__repomix__grep_repomix_output "AddRequiredServices|AddCoreServices|AddMarkerService"
mcp__repomix__grep_repomix_output "Services.*TryAdd.*" --contextLines=1

# Error handling patterns
mcp__repomix__grep_repomix_output "ThrowIfNull" --contextLines=1
mcp__repomix__grep_repomix_output "ArgumentNullException" --contextLines=2
```

#### **4. Testing Coverage Analysis**

```bash
# Test class discovery
mcp__repomix__grep_repomix_output "public.*class.*Tests" --contextLines=1
mcp__repomix__grep_repomix_output "Fact.*public.*void" --contextLines=1

# Mock pattern analysis
mcp__repomix__grep_repomix_output "Mock.*" --contextLines=1
mcp__repomix__grep_repomix_output "Assert.*" --contextLines=1
```

#### **5. Performance & Quality Checks**

```bash
# Performance indicators
mcp__repomix__grep_repomix_output "TODO|FIXME|BUG|HACK" --ignoreCase=true

# Configuration patterns
mcp__repomix__grep_repomix_output "Configure.*<.*>" --contextLines=2
mcp__repomix__grep_repomix_output "Options.*" --contextLines=1
```

### **Efficiency Guidelines for Repomix Usage**

#### **Token Conservation Strategies**

1. **Use grep first** - Find patterns before reading full content
2. **Selective reading** - Use startLine/endLine for targeted analysis
3. **Context lines** - Use --contextLines for focused results
4. **Pattern libraries** - Build reusable search patterns

#### **Pattern Library Examples**

```bash
# Common .NET patterns
BUILDER_PATTERN="class.*Builder.*IServiceCollection"
SERVICE_REGISTRATION="public.*static.*Add.*"
EXTENSION_CLASS="public.*static.*class.*Extensions"
TEST_CLASS="public.*class.*Tests"
DI_PATTERN="AddRequiredServices|AddCoreServices|AddMarkerService"
```
