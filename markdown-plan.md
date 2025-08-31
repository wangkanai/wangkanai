# Markdown Module Migration Plan

## Overview

This document outlines the comprehensive workflow for migrating the Markdown module from the main `wangkanai/wangkanai` repository to a dedicated `wangkanai/markdown` repository. This migration aims
to create a standalone, maintainable package with proper CI/CD, testing, and release management.

## Migration Scope

### Source Repository

- **Repository**: `wangkanai/wangkanai`
- **Module Path**: `/Markdown/`
- **Current Structure**:
   - `src/` - Main library code (Wangkanai.Markdown.csproj)
   - `tests/` - Unit tests (Wangkanai.Markdown.Tests.csproj)
   - `benchmark/` - Performance benchmarks (Wangkanai.Markdown.Benchmark.csproj)
   - `README.md` - Module documentation
   - `Directory.Build.props` - Build configuration
   - `build.ps1` - Build script
   - `sign.ps1` - Package signing script
   - `INSTALL.md` - Installation documentation

### Target Repository

- **Repository**: `wangkanai/markdown` (already created)
- **Location**: `~/Sources/markdown/`
- **Architecture**: Standalone .NET solution with modern slnx format

## Architecture Overview

### Project Structure

The new repository will maintain the same architectural patterns as the source repository:

```
markdown/
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml          # CI/CD pipeline
│   ├── FUNDING.yml             # GitHub sponsorship configuration
│   └── RELEASE.yml             # Release notes configuration
├── Assets/                     # Brand assets
│   ├── wangkanai-logo.afdesign
│   ├── wangkanai-logo.svg
│   ├── wangkanai-logo.ico
│   ├── wangkanai-logo.png
│   └── wangkanai-logo-large.png
├── src/                        # Main library source code
│   └── Wangkanai.Markdown.csproj
├── tests/                      # Unit and integration tests
│   └── Wangkanai.Markdown.Tests.csproj
├── benchmark/                  # Performance benchmarks
│   └── Wangkanai.Markdown.Benchmark.csproj
├── README.md                   # Main documentation (from Markdown/README.md)
├── Directory.Build.props       # MSBuild configuration
├── Wangkanai.Markdown.slnx     # Solution file (new slnx format)
├── build.ps1                   # Build automation script
├── sign.ps1                    # NuGet package signing script
└── md-styles.css               # Markdown styling
```

### Key Features

1. **Modern .NET Solution**: Uses the new `.slnx` format introduced for .NET CLI
2. **GitHub Actions CI/CD**: Automated build, test, and release pipeline
3. **SonarQube Integration**: Code quality and security analysis
4. **Branch Protection**: Main branch requires pull requests for changes
5. **NuGet Package**: Standalone package replacing project references

## Dependencies and References

### Current Dependencies Analysis

The Markdown module currently depends on:

- ASP.NET Core framework packages
- Shared wangkanai infrastructure (will be replaced with NuGet packages)
- Common build props and targets from parent repository

### Migration Strategy

1. **Internal Dependencies**: Replace project references with NuGet package references
2. **Build System**: Create standalone build configuration
3. **Shared Assets**: Copy required assets and configurations
4. **Version Management**: Independent versioning starting from current version (3.4.0)

## CI/CD Strategy

### GitHub Actions Workflow

The `dotnet.yml` workflow will provide:

- **Triggers**: Push to main branch, pull requests
- **Build Matrix**: Multi-target framework support
- **Testing**: Unit tests with coverage reporting
- **Quality Gates**: SonarQube analysis and quality checks
- **Artifacts**: NuGet package generation
- **Security**: Vulnerability scanning and dependency checks

### SonarQube Configuration

- **Organization**: `wangkanai`
- **Project Key**: `wangkanai_markdown`
- **Quality Profile**: C# quality rules and security standards
- **Coverage**: Minimum threshold requirements
- **Integration**: Automatic PR analysis and blocking

### Branch Protection Rules

- **Main Branch Protection**: Enabled
- **PR Requirements**: Required for all changes
- **Status Checks**: CI must pass before merge
- **Review Requirements**: Code review approval needed
- **Up-to-date Requirement**: PRs must be current with main

## Migration Phases

### Phase 1: Repository Setup

1. **Clone Target Repository**: Download to `~/Sources/markdown/`
2. **Initial Structure**: Create basic folder structure
3. **Asset Migration**: Copy brand assets and configurations
4. **Documentation**: Migrate README and supporting docs

### Phase 2: Code Migration

1. **Source Code**: Copy and adapt library source code
2. **Test Migration**: Transfer and update test projects
3. **Benchmark Migration**: Move performance test suite
4. **Build Configuration**: Adapt build props and scripts

### Phase 3: Solution Configuration

1. **Solution File**: Create new `.slnx` solution file
2. **Project References**: Update and validate project structure
3. **Build Validation**: Ensure successful compilation
4. **Test Validation**: Verify all tests pass

### Phase 4: CI/CD Implementation

1. **GitHub Actions**: Implement CI/CD pipeline
2. **SonarQube**: Configure code quality analysis
3. **Branch Protection**: Enable main branch protection
4. **Secret Management**: Configure required secrets

### Phase 5: Integration Testing

1. **Local Build**: Full solution build and test
2. **CI Validation**: Verify GitHub Actions workflow
3. **Quality Checks**: SonarQube analysis validation
4. **Package Generation**: Test NuGet package creation

### Phase 6: Source Repository Updates

1. **Dependency Updates**: Replace project references with NuGet packages
2. **Build System**: Remove Markdown from main solution
3. **Documentation**: Update references and links
4. **Version Management**: Update package references

## Quality Assurance

### Testing Strategy

- **Unit Tests**: Comprehensive test coverage maintenance
- **Integration Tests**: End-to-end functionality validation
- **Performance Tests**: Benchmark regression testing
- **Compatibility Tests**: Multi-framework target validation

### Code Quality Metrics

- **Coverage Threshold**: Minimum 80% code coverage
- **Quality Gate**: SonarQube quality standards
- **Security Standards**: OWASP compliance checks
- **Performance Baseline**: Benchmark performance targets

## Risk Management

### Identified Risks

1. **Breaking Changes**: API compatibility issues
2. **Dependency Conflicts**: NuGet package version mismatches
3. **Build Failures**: Configuration or environment issues
4. **Test Failures**: Migration-related test breakage

### Mitigation Strategies

1. **Incremental Migration**: Phase-based approach with validation
2. **Rollback Plan**: Git-based rollback capabilities
3. **Testing Gates**: Comprehensive testing at each phase
4. **Documentation**: Detailed change documentation

## Success Criteria

### Technical Criteria

- [x] Repository created and accessible
- [x] Complete code migration with no functionality loss
- [x] All tests pass in new repository
- [x] Successful CI/CD pipeline execution
- [x] SonarQube quality gate passes
- [x] NuGet package generates successfully

### Process Criteria

- [x] Branch protection rules active
- [x] PR workflow functional
- [x] Documentation complete and accurate
- [x] Migration documentation published
- [x] Source repository references updated

### Quality Criteria

- [x] Code coverage maintained or improved
- [x] Performance benchmarks meet baseline
- [x] Security scan passes
- [x] Dependency vulnerabilities resolved

## Timeline Estimation

### Phase Duration Estimates

- **Phase 1 (Setup)**: 2-3 hours
- **Phase 2 (Code Migration)**: 4-6 hours
- **Phase 3 (Solution Config)**: 2-3 hours
- **Phase 4 (CI/CD)**: 3-4 hours
- **Phase 5 (Testing)**: 2-3 hours
- **Phase 6 (Source Updates)**: 2-3 hours

**Total Estimated Time**: 15-22 hours

### Critical Path Items

1. Repository structure setup
2. Code and test migration
3. CI/CD pipeline implementation
4. Integration validation
5. Source repository updates

## Communication Plan

### Stakeholder Updates

- **Progress Reports**: Regular status updates during migration
- **Issue Tracking**: GitHub Issues for problem tracking
- **Documentation**: Updated README and migration notes
- **Release Notes**: Comprehensive change documentation

### Post-Migration

- **Package Publication**: NuGet package release
- **Documentation Updates**: Updated integration guides
- **Community Notification**: Migration announcement
- **Support Transition**: Issue tracking in new repository

---

This migration plan serves as the comprehensive guide for successfully extracting the Markdown module into a standalone, production-ready repository with modern development practices and CI/CD
automation.