# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Build and Restore
```bash
# Full restore and release build
./build.ps1
# Equivalent to:
dotnet restore --force
dotnet build -c Release --tl
```

### Development Server
```bash
# Start development environment with hot reload
./watch.ps1
# Equivalent to:
dotnet watch --project ./Web/Razor/RiverSync.Web.Razor.csproj
```

### Testing
```bash
# Run all tests
dotnet test --configuration Release
# Test pattern matches: **/*[Tt]ests/*.csproj
```

### Solution Management
```bash
# Restore all projects
dotnet restore **/*.slnx
# Build all projects
dotnet build **/*.slnx --configuration Release
```

### Database Operations
```bash
# Database management script
./db.ps1
```

## Architecture Overview

RiverSync is a .NET 9.0 enterprise application built using Clean Architecture principles with Domain-Driven Design (DDD) patterns. This is part of the larger Wangkanai ecosystem of libraries and applications. The solution consists of multiple independent modules without centralized orchestration.

### Solution Structure

The RiverSync solution (`RiverSync.slnx`) is organized into the following structure:

#### Configuration Folders
- **/.claude/**: Claude Code configuration
  - `settings.local.json`: Local Claude settings
  - `CLAUDE.md`: This documentation file
- **/.serena/**: Serena AI assistant configuration
  - `project.yml`: Project configuration for Serena
- **/.solutions/**: Solution-level configuration files
  - `.editorconfig`, `.gitattributes`, `.gitignore`: Development environment setup
  - `Directory.Build.props`: Common MSBuild properties
  - `Directory.Build.targets`: Common MSBuild targets
  - `Directory.Packages.props`: Centralized package version management
  - `azure-pipelines.yml`: CI/CD pipeline configuration
  - `build.ps1`, `watch.ps1`, `db.ps1`: Development scripts
  - `qodana.yaml`: Code quality analysis configuration
  - `azure-aad-config.txt`: Azure AD configuration notes

#### Application Modules

**Portal/** - Main business application (Clean Architecture)
- `RiverSync.Portal.Domain.csproj`: Core business entities and domain logic
- `RiverSync.Portal.Application.csproj`: Application services and use cases (MediatR)
- `RiverSync.Portal.Infrastructure.csproj`: External service integrations
- `RiverSync.Portal.Persistence.csproj`: Data access layer with Entity Framework Core
- `RiverSync.Portal.Client.csproj`: Blazor WebAssembly client application
- `RiverSync.Portal.csproj`: ASP.NET Core Web API and hosting (Server)
- `RiverSync.Portal.Migration.csproj`: Database migration utilities
- `Directory.Build.props`: Portal-specific build properties

**Processor/** - Background processing service (Clean Architecture)
- `RiverSync.Processor.Domain.csproj`: Processing domain entities
- `RiverSync.Processor.Application.csproj`: Processing application services
- `RiverSync.Processor.Infrastructure.csproj`: Processing infrastructure
- `RiverSync.Processor.Server.csproj`: Processing service host
- `Directory.Build.props`: Processor-specific build properties

**Web/** - Additional web applications
- `RiverSync.Web.Client.csproj`: Blazor WebAssembly client
- `RiverSync.Web.Razor.csproj`: Server-side Blazor/Razor Pages application
- `RiverSync.Web.Old.csproj`: Legacy web application
- `Directory.Build.props`: Web-specific build properties

**Aspire/** - Deprecated/Empty directory
- Only contains `Directory.Build.props`
- Referenced in `watch.ps1` but no longer contains projects

**Docs/** - Documentation
- `RiverSync.Docs.csproj`: Documentation site project

**Admin/** - Administrative tools and interfaces
- `Directory.Build.props`: Admin-specific build properties

#### Wangkanai Libraries in Solution
The RiverSync solution is part of the Wangkanai monorepo containing multiple libraries:

**Core Libraries**:
- **Detection**: Client device/browser/platform detection
- **Responsive**: Responsive design and adaptation
- **Blazor**: Custom Blazor UI components
- **Identity**: User authentication and authorization
- **Domain**: Domain-driven design base classes
- **System**: System utilities and extensions
- **Shared**: Shared components across libraries

**UI & Presentation**:
- **Markdown**: Markdown parsing and rendering
- **Mvc**: MVC extensions and helpers
- **Webmaster**: SEO and web optimization tools

**Infrastructure**:
- **EntityFramework**: EF Core extensions
- **Hosting**: Hosting configuration helpers
- **Webserver**: Web server utilities
- **Microservice**: Microservice patterns

**Security & Identity**:
- **Federation**: Identity provider federation
- **Security**: Authentication/Authorization components
- **Cryptography**: Cryptographic utilities
- **Audit**: Audit logging functionality

**Development Tools**:
- **Testing**: Testing utilities and base classes
- **Validation**: Validation framework
- **Analytics**: Analytics integration
- **Solver**: Problem-solving utilities
- **Tools**: MSBuild tasks and watchers

**Additional Components**:
- **Nation**: Localization and country data
- **Annotations**: Custom attributes
- **Extensions**: Various extension libraries

#### Project Dependencies and Relationships
- **Portal.Server** (main web API) references Portal.Client, Portal.Persistence
- **Processor.Server** runs as independent background service
- Each module follows Clean Architecture with clear layer separation
- Centralized package management ensures consistent versions across all projects

### Technology Stack

- **.NET 9.0** with C# 12+ features (nullable reference types, implicit usings)
- **Blazor WebAssembly** for client-side applications
- **Entity Framework Core 9.0** with PostgreSQL provider
- **MediatR** for CQRS pattern implementation
- **OpenTelemetry** for observability and monitoring
- **xUnit** for testing with Moq for mocking
- **Wangkanai** custom libraries for various functionality

### Database and Infrastructure

- **PostgreSQL** as primary database
- **Redis** for caching
- **RabbitMQ** for message queuing
- **Seq** for structured logging
- **Azure** services integration (Event Hubs, App Configuration)

### Development Patterns

- **Clean Architecture** with clear separation of concerns
- **Domain-Driven Design** with rich domain models
- **CQRS** pattern using MediatR
- **Repository Pattern** through Entity Framework Core
- **Dependency Injection** throughout the application
- **Centralized package management** via Directory.Packages.props

### Authentication and Authorization

- **ASP.NET Core Identity** with Entity Framework storage
- **JWT Bearer** authentication
- **OpenID Connect** support
- **Social login** providers (Facebook, Google)
- **Microsoft Authentication Library (MSAL)** for WebAssembly
- **Azure AD** integration

### Key Files

- `RiverSync.slnx`: Main solution file (Visual Studio solution)
- `Directory.Build.props`: Common MSBuild properties for all projects
- `Directory.Packages.props`: Centralized NuGet package version management
- `azure-pipelines.yml`: CI/CD pipeline configuration for Azure DevOps

### Running the Application

Current development workflow:
1. **Web Razor Application** (Primary): Use the watch script for hot reload
   ```bash
   ./watch.ps1
   # Or directly:
   dotnet watch --project ./Web/Razor/RiverSync.Web.Razor.csproj
   ```
2. **Portal Application**: Run the Portal.Server project for API services
   ```bash
   dotnet run --project Portal/Server/RiverSync.Portal.csproj
   ```
3. **Processor Service**: Run the Processor.Server project for background processing
   ```bash
   dotnet run --project Processor/Server/RiverSync.Processor.Server.csproj
   ```
4. **Infrastructure**: Manually start required services (PostgreSQL, Redis, RabbitMQ) using Docker or local installations

### Deployment and Environment Configuration

#### Azure Deployment
- **Azure App Services** for hosting web applications
- **Azure Container Instances** or **Azure Kubernetes Service** for containerized services
- **Azure Database for PostgreSQL** for production database
- **Azure Cache for Redis** for distributed caching
- **Azure Service Bus** or **Azure Event Hubs** for messaging
- **Azure Application Insights** for monitoring and telemetry

#### Environment Configuration
- **Development**: Manual service startup with local Docker containers
- **Staging**: Azure-hosted services with test databases
- **Production**: Full Azure deployment with high availability

#### Missing Configuration Files
The following configuration files are referenced but may need to be created:
- `azure-pipelines.yml`: Azure DevOps CI/CD pipeline
- Environment-specific `appsettings.{Environment}.json` files
- Docker/container configuration files
- Kubernetes manifests (if using AKS)

## Available MCP Commands (Ranked by Efficiency)

This project has access to several MCP (Model Context Protocol) servers. Commands are organized by priority and frequency of use for .NET development with Azure DevOps integration:

### 🔥 Tier 1: Essential Daily Development Commands

#### JetBrains IDE Integration (Most Used)
```bash
# Code editing and navigation (highest efficiency)
mcp__jetbrains__replace_specific_text          # Most efficient for precise edits
mcp__jetbrains__search_in_files_content        # Fast codebase search
mcp__jetbrains__find_files_by_name_substring   # Quick file location
mcp__jetbrains__get_file_text_by_path          # Direct file reading
mcp__jetbrains__list_directory_tree_in_folder  # Project structure overview

# Active development
mcp__jetbrains__get_current_file_errors        # Real-time error checking
mcp__jetbrains__get_project_problems           # Project-wide issues
mcp__jetbrains__run_configuration              # Execute builds/tests
mcp__jetbrains__execute_terminal_command       # Run dotnet commands

# Code formatting and quality
mcp__jetbrains__reformat_current_file          # Format current file
mcp__jetbrains__reformat_file                  # Format specific file

# Debugging
mcp__jetbrains__toggle_debugger_breakpoint     # Quick debugging setup
mcp__jetbrains__get_debugger_breakpoints       # Breakpoint management
```

#### Azure DevOps Integration (Critical for Project Management)
```bash
# Work Item Management
mcp__devops__wit_my_work_items                # Get assigned work items
mcp__devops__wit_create_work_item             # Create new tasks/bugs
mcp__devops__wit_update_work_item             # Update work item status
mcp__devops__wit_add_work_item_comment        # Add comments to work items
mcp__devops__wit_link_work_item_to_pull_request # Link work to PRs

# Source Control & Pull Requests
mcp__devops__repo_create_pull_request         # Create PRs
mcp__devops__repo_list_pull_requests_by_repo  # List open PRs
mcp__devops__repo_get_pull_request_by_id      # Get PR details
mcp__devops__repo_list_pull_request_threads   # Review PR comments
mcp__devops__repo_reply_to_comment            # Respond to PR reviews
mcp__devops__repo_resolve_comment             # Resolve PR threads

# Build Pipeline Management
mcp__devops__build_get_definitions             # List build definitions
mcp__devops__build_run_build                  # Trigger builds
mcp__devops__build_get_status                 # Check build status
mcp__devops__build_get_log                    # View build logs
mcp__devops__build_get_changes                # See build changes

# Git Operations
mcp__devops__repo_list_repos_by_project       # List repositories
mcp__devops__repo_list_branches_by_repo       # List branches
mcp__devops__repo_search_commits              # Search commit history
mcp__devops__repo_get_branch_by_name          # Get branch details
```

#### Azure Integration (Critical for Cloud Development)
```bash
# Essential Azure commands
mcp__azure__azmcp_subscription_list            # View available subscriptions
mcp__azure__azmcp_group_list                   # List resource groups
mcp__azure__azmcp_appconfig_kv_list            # App Configuration management
mcp__azure__azmcp_appconfig_kv_set             # Update configuration
mcp__azure__azmcp_keyvault_secret_get          # Retrieve secrets

# Database operations
mcp__azure__azmcp_sql_db_show                  # SQL Database info
mcp__azure__azmcp_postgres_database_query      # PostgreSQL queries
mcp__azure__azmcp_cosmos_database_container_item_query  # Cosmos DB queries

# Monitoring and diagnostics
mcp__azure__azmcp_monitor_workspace_log_query  # Log Analytics queries
mcp__azure__azmcp_monitor_metrics_query        # Performance metrics
```

#### Microsoft Documentation (.NET & Azure Focus)
```bash
# Documentation search - focused on .NET and Azure only
mcp__ms-docs__microsoft_docs_search            # Search .NET/Azure docs
# Use for: ASP.NET Core, Blazor, Entity Framework, Azure services,
# C# features, .NET APIs, Azure SDK, deployment guides
```

### 🚀 Tier 2: High-Value Development Tools

#### Azure DevOps Advanced Features
```bash
# Sprint & Iteration Management
mcp__devops__work_list_team_iterations        # View team iterations
mcp__devops__work_create_iterations           # Create new sprints
mcp__devops__wit_get_work_items_for_iteration # Sprint work items

# Release Management
mcp__devops__release_get_definitions           # List release definitions
mcp__devops__release_get_releases             # View releases

# Wiki & Documentation
mcp__devops__wiki_list_wikis                  # List project wikis
mcp__devops__wiki_get_page_content            # Read wiki pages
mcp__devops__search_wiki                      # Search wiki content

# Test Management
mcp__devops__testplan_list_test_plans         # List test plans
mcp__devops__testplan_create_test_case        # Create test cases
mcp__devops__testplan_show_test_results_from_build_id # Test results

# Search
mcp__devops__search_code                      # Search code in repos
mcp__devops__search_workitem                  # Search work items
```

#### Code Analysis & Quality
```bash
# Repository analysis
mcp__repomix__pack_codebase                   # Analyze entire codebase
mcp__repomix__grep_repomix_output             # Search analysis results

# File operations
mcp__repomix__file_system_read_file           # Read files with security
mcp__repomix__file_system_read_directory      # List directory contents
```

#### Container Management
```bash
# Container operations (if using Podman/Docker)
mcp__podman__container_list                   # List running containers
mcp__podman__container_logs                   # View container logs
mcp__podman__image_list                       # Available images
# Note: Manual container management required without Aspire orchestration
```

### 📊 Tier 3: Project Management & Architecture

#### Memory/Knowledge Graph
```bash
# Relationship tracking
mcp__memory__create_entities                  # Track system components
mcp__memory__create_relations                 # Document relationships
mcp__memory__search_nodes                     # Find related concepts
mcp__memory__read_graph                       # View entire knowledge graph
```

### 🔧 Tier 4: Specialized Tools

#### Advanced Azure Operations
```bash
# Load testing
mcp__azure__azmcp_loadtesting_test_create     # Create load tests
mcp__azure__azmcp_loadtesting_testrun_create  # Execute tests

# Service Bus
mcp__azure__azmcp_servicebus_queue_details    # Queue management
mcp__azure__azmcp_servicebus_topic_details    # Topic management

# Search services
mcp__azure__azmcp_search_index_query          # Azure AI Search

# Container services
mcp__azure__azmcp_aks_cluster_list            # AKS clusters

# Grafana monitoring
mcp__azure__azmcp_grafana_list                # Grafana workspaces

# Storage
mcp__azure__azmcp_storage_account_list        # Storage accounts
mcp__azure__azmcp_storage_blob_list           # Blob storage
```

#### Browser Testing
```bash
# UI testing (requires server running)
mcp__browserloop__screenshot                  # Capture UI state
mcp__browserloop__read_console               # Debug browser issues
```

#### Utilities
```bash
# Advanced operations
mcp__sequential-thinking__sequentialthinking  # Complex problem solving
mcp__fetch__fetch                            # External data fetching
mcp__azure__azmcp_extension_az               # Raw Azure CLI commands
mcp__azure__azmcp_extension_azd              # Azure Developer CLI
mcp__azure__azmcp_extension_azqr             # Azure Quick Review
```

### 💡 Usage Tips

1. **Most Efficient Workflow**:
   - Use JetBrains commands for immediate code operations
   - Use Azure DevOps commands for work item and source control management
   - Use Azure commands for cloud resource management
   - Use Microsoft Docs for .NET/Azure documentation only

2. **DevOps Integration Best Practices**:
   - Always link commits to work items using DevOps commands
   - Create PRs through DevOps MCP for proper tracking
   - Use DevOps search before creating duplicate work items
   - Monitor build pipelines through DevOps commands

3. **Performance Optimization**:
   - Prefer `replace_specific_text` over full file replacements
   - Use DevOps batch operations when updating multiple work items
   - Cache frequently accessed Azure configurations
   - Batch Azure operations to reduce API calls

4. **Best Practices**:
   - Always use `get_current_file_errors` after code changes
   - Run `get_project_problems` before commits
   - Use `search_in_files_content` before creating new code
   - Link all PRs to work items for traceability
   - Check build status before merging PRs

These MCP commands extend Claude's capabilities for efficient .NET development, Azure DevOps integration, and cloud deployment management.
