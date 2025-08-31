# Nation Module Migration Plan

## Overview

Migrate the `@Nation/` module from the main wangkanai monorepo to a standalone repository at https://github.com/wangkanai/nation. This will create an independent Nation library with its own CI/CD
pipeline, documentation, and NuGet package distribution.

## Project Structure

The new repository will maintain the same architecture as the parent repo with these components:

### Source Structure

```
nation/
├── src/
│   └── Wangkanai.Nation.csproj          # Main library
├── test/
│   └── Wangkanai.Nation.Tests.csproj    # Unit tests
├── benchmark/
│   └── Wangkanai.Nation.Benchmark.csproj # Performance tests
├── Nation.slnx                          # Solution file (new slnx format)
├── README.md                            # Inherited from Nation/README.md
└── Directory.Build.props                # Build configuration
```

### Infrastructure Files

```
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml                   # CI/CD pipeline
│   ├── FUNDING.yml                      # Sponsorship links
│   └── RELEASE.yml                      # Release configuration
├── Assets/                              # Brand assets
│   ├── wangkanai-logo.svg
│   ├── wangkanai-logo.png
│   ├── wangkanai-logo-large.png
│   ├── wangkanai-logo.ico
│   └── wangkanai-logo.afdesign
├── build.ps1                           # Build script
├── sign.ps1                            # Package signing script
└── md-styles.css                       # Documentation styling
```

## Technical Specifications

### Version Strategy

- **Current Version**: 0.5.0
- **New Repo Version**: 1.0.0 (major milestone as standalone project)
- **NuGet Package**: `Wangkanai.Nation` v1.0.0

### Solution Configuration

- **Format**: New slnx format (XML-based)
- **Target Framework**: Maintain current .NET compatibility
- **Package References**: Inherit from parent but make standalone

### Dependencies

- Extract Nation-specific dependencies from parent `Directory.Packages.props`
- Maintain version compatibility where possible
- Update to latest stable versions for new repo

## CI/CD Pipeline Specifications

### GitHub Actions Workflow

- **Name**: `dotnet`
- **Triggers**: Push to main, Pull Requests
- **Main Branch Protection**: Required PR reviews before merge

### SonarCloud Integration

```yaml
Organization: wangkanai
Project Key: wangkanai_nation
Token: ${{ secrets.SONAR_TOKEN }}
Version: 1.0.0
```

### Build Pipeline Features

- Multi-target framework support
- Automated testing with coverage
- Code quality analysis via SonarCloud
- NuGet package generation
- Performance benchmark execution

## Migration Phases

### Phase 1: Repository Setup

1. Clone target repository locally
2. Set up basic repository structure
3. Copy core files and maintain directory structure

### Phase 2: Source Code Migration

1. Copy Nation source code with full history preservation
2. Update project files for standalone operation
3. Create new slnx solution file
4. Update package references and dependencies

### Phase 3: Infrastructure Setup

1. Configure GitHub Actions workflow
2. Set up SonarCloud integration
3. Copy and adapt build/sign scripts
4. Configure branch protection rules

### Phase 4: Documentation & Assets

1. Migrate README.md as primary documentation
2. Copy brand assets and styling files
3. Update project URLs and references
4. Create migration documentation

### Phase 5: Integration & Testing

1. Verify build pipeline functionality
2. Test NuGet package generation
3. Validate SonarCloud integration
4. Execute full test suite

### Phase 6: Parent Repository Cleanup

1. Update parent repo to reference NuGet package
2. Remove Nation module from main solution
3. Update build scripts to exclude Nation
4. Document migration in parent repo

## Success Criteria

- ✅ Standalone repository builds successfully
- ✅ All tests pass in new environment
- ✅ GitHub Actions pipeline executes without errors
- ✅ SonarCloud integration provides quality metrics
- ✅ NuGet package can be generated and consumed
- ✅ Parent repository references work with NuGet package
- ✅ Documentation accurately reflects new structure

## Risk Mitigation

- **Dependency Conflicts**: Comprehensive dependency audit before migration
- **Build Failures**: Incremental testing at each phase
- **Integration Issues**: Parallel development environment for testing
- **History Loss**: Use git subtree/filter-branch for history preservation

## Timeline Estimation

- **Phase 1-2**: Repository and source setup (2-3 hours)
- **Phase 3**: Infrastructure configuration (1-2 hours)
- **Phase 4**: Documentation and assets (1 hour)
- **Phase 5**: Testing and validation (1-2 hours)
- **Phase 6**: Parent cleanup (1 hour)
- **Total**: 6-9 hours

## Post-Migration Maintenance

- Monitor CI/CD pipeline health
- Regular dependency updates
- Version management coordination with parent repo
- Community contribution guidelines
- Release management process