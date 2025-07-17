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

### Available MCP Commands (By Priority & Efficiency)

#### **🔴 HIGH PRIORITY - Core Development Tools**

##### **JetBrains IDE Integration** (Primary Development Environment)
```bash
# File and editor operations
mcp__jetbrains__get_open_in_editor_file_text        # Get text of currently open file
mcp__jetbrains__get_open_in_editor_file_path        # Get path of currently open file
mcp__jetbrains__get_selected_in_editor_text         # Get currently selected text
mcp__jetbrains__replace_selected_text               # Replace selected text
mcp__jetbrains__replace_current_file_text           # Replace entire file content
mcp__jetbrains__create_new_file_with_text           # Create new file with content
mcp__jetbrains__get_file_text_by_path               # Read file by project path
mcp__jetbrains__replace_file_text_by_path           # Replace file content by path
mcp__jetbrains__replace_specific_text               # Replace specific text in file
mcp__jetbrains__open_file_in_editor                 # Open file in editor

# Project navigation and search
mcp__jetbrains__find_files_by_name_substring        # Find files by name pattern
mcp__jetbrains__list_files_in_folder               # List files in directory
mcp__jetbrains__list_directory_tree_in_folder      # Get directory tree structure
mcp__jetbrains__search_in_files_content            # Search text in project files
mcp__jetbrains__get_all_open_file_texts             # Get all open file contents
mcp__jetbrains__get_all_open_file_paths             # Get all open file paths

# Version control and project info
mcp__jetbrains__get_project_vcs_status              # Get VCS status (git status)
mcp__jetbrains__get_project_modules                 # Get project modules
mcp__jetbrains__get_project_dependencies           # Get project dependencies
mcp__jetbrains__find_commit_by_message              # Find commits by message

# Debugging and breakpoints
mcp__jetbrains__toggle_debugger_breakpoint         # Toggle breakpoint at line
mcp__jetbrains__get_debugger_breakpoints           # Get all breakpoints

# Code analysis and formatting
mcp__jetbrains__get_current_file_errors             # Get errors in current file
mcp__jetbrains__get_project_problems                # Get all project problems
mcp__jetbrains__reformat_current_file               # Format current file
mcp__jetbrains__reformat_file                       # Format specific file

# Build and run
mcp__jetbrains__get_run_configurations              # Get available run configs
mcp__jetbrains__run_configuration                   # Run specific configuration

# Terminal operations
mcp__jetbrains__get_terminal_text                   # Get terminal content
mcp__jetbrains__execute_terminal_command            # Execute terminal command

# IDE actions and utilities
mcp__jetbrains__list_available_actions              # List all IDE actions
mcp__jetbrains__execute_action_by_id                # Execute action by ID
mcp__jetbrains__get_progress_indicators             # Get progress status
mcp__jetbrains__wait                                # Wait for specified time
```

##### **GitHub Integration** (Repository Management)
```bash
# Repository operations
mcp__github__create_repository                      # Create new repository
mcp__github__search_repositories                    # Search repositories
mcp__github__fork_repository                        # Fork repository

# File operations
mcp__github__get_file_contents                      # Get file/directory contents
mcp__github__create_or_update_file                  # Create or update single file
mcp__github__push_files                             # Push multiple files

# Branch operations
mcp__github__create_branch                          # Create new branch
mcp__github__list_commits                           # List commits

# Issues
mcp__github__create_issue                           # Create new issue
mcp__github__list_issues                            # List issues with filters
mcp__github__update_issue                           # Update existing issue
mcp__github__get_issue                              # Get issue details
mcp__github__add_issue_comment                      # Add comment to issue

# Pull requests
mcp__github__create_pull_request                    # Create pull request
mcp__github__list_pull_requests                     # List pull requests
mcp__github__get_pull_request                       # Get PR details
mcp__github__get_pull_request_files                 # Get PR file changes
mcp__github__get_pull_request_status                # Get PR status
mcp__github__get_pull_request_comments              # Get PR comments
mcp__github__get_pull_request_reviews               # Get PR reviews
mcp__github__create_pull_request_review             # Create PR review
mcp__github__merge_pull_request                     # Merge pull request
mcp__github__update_pull_request_branch             # Update PR branch

# Search operations
mcp__github__search_code                            # Search code across repos
mcp__github__search_issues                          # Search issues and PRs
mcp__github__search_users                           # Search users
```

#### **🟡 MEDIUM PRIORITY - Quality & Analysis Tools**

##### **SonarQube Code Analysis** (Code Quality Metrics)
```bash
# Project and quality gates
mcp__sonarqube__search_my_sonarqube_projects        # Find SonarQube projects
mcp__sonarqube__get_project_quality_gate_status     # Get quality gate status
mcp__sonarqube__list_quality_gates                  # List all quality gates

# Issues and rules
mcp__sonarqube__search_sonar_issues_in_projects     # Search issues in projects
mcp__sonarqube__change_sonar_issue_status           # Change issue status
mcp__sonarqube__show_rule                           # Show rule details
mcp__sonarqube__list_rule_repositories              # List rule repositories

# Code analysis
mcp__sonarqube__analyze_code_snippet                # Analyze code snippet
mcp__sonarqube__get_component_measures              # Get component metrics
mcp__sonarqube__search_metrics                      # Search available metrics
mcp__sonarqube__get_scm_info                        # Get SCM information
mcp__sonarqube__get_raw_source                      # Get raw source code
mcp__sonarqube__list_languages                      # List supported languages
```

##### **Container Management (Podman)** (Development Environment)
```bash
# Container operations
mcp__podman__container_list                         # List containers
mcp__podman__container_run                          # Run container
mcp__podman__container_stop                         # Stop container
mcp__podman__container_remove                       # Remove container
mcp__podman__container_inspect                      # Inspect container
mcp__podman__container_logs                         # Get container logs

# Image operations
mcp__podman__image_list                             # List images
mcp__podman__image_pull                             # Pull image
mcp__podman__image_push                             # Push image
mcp__podman__image_remove                           # Remove image
mcp__podman__image_build                            # Build image

# Network and volume operations
mcp__podman__network_list                           # List networks
mcp__podman__volume_list                            # List volumes
```

##### **Memory & Knowledge Graph** (Project Context)
```bash
# Entity management
mcp__memory__create_entities                        # Create entities
mcp__memory__delete_entities                        # Delete entities
mcp__memory__search_nodes                           # Search nodes
mcp__memory__open_nodes                             # Open specific nodes
mcp__memory__read_graph                             # Read entire graph

# Relationship management
mcp__memory__create_relations                       # Create relations
mcp__memory__delete_relations                       # Delete relations

# Observation management
mcp__memory__add_observations                       # Add observations
mcp__memory__delete_observations                    # Delete observations
```

#### **🟢 LOW PRIORITY - Utility & Support Tools**

##### **Web & Content Fetching** (External Content)
```bash
mcp__fetch__fetch                                   # Fetch URL content
```

##### **🚀 REPOMIX INTEGRATION - CRITICAL FOR EFFICIENCY**
```bash
# PRIMARY WORKFLOW TOOLS (Use these extensively!)
mcp__repomix__pack_codebase                         # Generate codebase analysis
mcp__repomix__grep_repomix_output                   # Pattern discovery & search
mcp__repomix__read_repomix_output                   # Selective content reading

# ADDITIONAL CAPABILITIES
mcp__repomix__pack_remote_repository                # Analyze external repositories
mcp__repomix__file_system_read_file                 # Direct file system access
mcp__repomix__file_system_read_directory            # Directory exploration

# COMMON REPOMIX PATTERNS FOR THIS CODEBASE
# Architecture Discovery
mcp__repomix__grep_repomix_output "interface.*Service|class.*Controller|public.*Builder"
mcp__repomix__grep_repomix_output "public.*static.*class.*Extensions"
mcp__repomix__grep_repomix_output "namespace.*DependencyInjection"

# Implementation Patterns
mcp__repomix__grep_repomix_output "AddRequiredServices|AddCoreServices|AddMarkerService"
mcp__repomix__grep_repomix_output "public.*static.*Add.*Builder"
mcp__repomix__grep_repomix_output "IServiceCollection.*Services"

# Quality Assurance
mcp__repomix__grep_repomix_output "ThrowIfNull|ArgumentNullException"
mcp__repomix__grep_repomix_output "Services.*TryAdd.*"
mcp__repomix__grep_repomix_output "TODO|FIXME|BUG"

# Testing Patterns
mcp__repomix__grep_repomix_output "public.*class.*Tests"
mcp__repomix__grep_repomix_output "Fact.*public.*void"
mcp__repomix__grep_repomix_output "Assert.*Throws.*ArgumentNullException"
```

##### **Problem Solving & Thinking** (Complex Analysis)
```bash
mcp__sequential-thinking__sequentialthinking        # Sequential problem solving
```

#### **🔵 SPECIALIZED - System & Diagnostic Tools**

##### **IDE Diagnostics** (System Analysis)
```bash
mcp__ide__getDiagnostics                            # Get IDE diagnostics
```

##### **Resource Management** (MCP Infrastructure)
```bash
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

## AUTOMATED REPOMIX HOOKS SYSTEM

### **🔄 Automatic Repomix Packing Triggers**

The repository now includes automated hooks that trigger repomix packing when code changes are detected:

#### **Git Hooks (Repository Level)**
```bash
# Installed hooks
.git/hooks/post-commit          # Triggers after each commit
.git/hooks/post-merge           # Triggers after merge operations

# Hook behavior
- Detects significant file changes (.cs, .csproj, .props, .targets, .json, .config, .xml, .razor, .resx)
- Implements 5-minute cooldown to prevent excessive packing
- Runs in background to avoid blocking git operations
- Logs all activities to .git/hooks/repomix-hook.log
```

#### **Claude Code Hooks (IDE Level)**
```bash
# Hook configuration in .claude/settings.local.json
- user-prompt-submit: Triggers on code-related prompts
- tool-call-complete: Triggers after code-changing tools

# Intelligent triggering
- Analyzes user prompts for code-related keywords
- Monitors tool execution for code changes
- Implements 2-minute cooldown for tool triggers
- Logs activities to .claude/hooks/claude-hook.log
```

#### **Repomix Monitor Daemon**
```bash
# Start the monitoring daemon
./.claude/repomix-monitor.sh start

# Check daemon status
./.claude/repomix-monitor.sh status

# Stop the daemon
./.claude/repomix-monitor.sh stop

# Manual trigger
./.claude/repomix-monitor.sh trigger
```

### **📁 Hook File Structure**
```
.git/hooks/
├── post-commit                 # Git commit hook
├── post-merge                  # Git merge hook
├── repomix-hook.log           # Git hook logs
└── last-repomix-pack          # Timestamp tracking

.claude/
├── settings.local.json         # Claude hook configuration
├── hooks/
│   ├── repomix-auto-pack.sh   # User prompt trigger
│   ├── post-tool-repomix.sh   # Tool execution trigger
│   └── claude-hook.log        # Claude hook logs
├── repomix-monitor.sh          # Monitor daemon
└── repomix-trigger            # Signal files (auto-created)
```

### **🎯 Trigger Conditions**

#### **Code-Related Keywords (User Prompts)**
- Architecture: `analyze`, `architecture`, `structure`, `design`, `pattern`
- Implementation: `implement`, `create`, `add`, `build`, `configure`
- Analysis: `find`, `search`, `discover`, `examine`, `review`
- Quality: `fix`, `optimize`, `improve`, `refactor`, `test`
- Components: `class`, `method`, `service`, `controller`, `interface`

#### **Code-Changing Tools**
- File operations: `Edit`, `MultiEdit`, `Write`, `NotebookEdit`
- JetBrains: `replace_*`, `create_new_file_with_text`
- GitHub: `create_or_update_file`, `push_files`

### **⚡ Performance Optimizations**

#### **Cooldown Periods**
- **Git hooks**: 5-minute cooldown between packs
- **Claude hooks**: 2-minute cooldown for tool triggers
- **Stale detection**: 10-minute threshold for auto-refresh

#### **Background Processing**
- All hooks run in background to avoid blocking
- Separate log files for debugging
- PID tracking for daemon management
- Signal-based communication between hooks

### **🔧 Configuration & Management**

#### **Enable/Disable Hooks**
```bash
# Disable git hooks temporarily
mv .git/hooks/post-commit .git/hooks/post-commit.disabled

# Re-enable
mv .git/hooks/post-commit.disabled .git/hooks/post-commit

# Check Claude hook status
cat .claude/settings.local.json | jq '.hooks'
```

#### **Monitor Hook Activity**
```bash
# View git hook logs
tail -f .git/hooks/repomix-hook.log

# View Claude hook logs
tail -f .claude/hooks/claude-hook.log

# Monitor daemon logs
tail -f .claude/repomix-monitor.log
```

### **🚀 Benefits**

1. **Always Fresh Analysis**: Repomix output stays current with code changes
2. **Intelligent Triggering**: Only packs when necessary, not on every change
3. **Multi-Level Coverage**: Git commits, Claude interactions, and manual triggers
4. **Performance Optimized**: Background processing with cooldown periods
5. **Comprehensive Logging**: Full audit trail of all repomix activities
6. **Zero Maintenance**: Fully automated once set up