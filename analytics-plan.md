# Analytics Module Migration Plan

## Overview
This document outlines the comprehensive plan for migrating the Analytics module from the main wangkanai repository to a standalone repository at https://github.com/wangkanai/analytics.

## Project Analysis Summary

### Current Analytics Module Structure
```
Analytics/
├── src/
│   ├── Wangkanai.Analytics.csproj
│   ├── Hosting/AnalyticsMiddleware.cs
│   ├── DependencyInjection/
│   │   ├── AnalyticsApplicationExtensions.cs
│   │   ├── AnalyticsBuilder.cs
│   │   ├── AnalyticsCollectionExtensions.cs
│   │   ├── IAnalyticsBuilder.cs
│   │   ├── Options/AnalyticsOptions.cs
│   │   └── Builders/Core.cs
│   ├── Services/
│   │   ├── AnalyticsService.cs
│   │   ├── MarkerService.cs
│   │   └── Interfaces/IAnalyticsService.cs
│   ├── Properties/AssemblyInfo.cs
│   └── Usings.cs
├── tests/
│   ├── Wangkanai.Analytics.Tests.csproj
│   ├── DependencyInjection/
│   │   ├── ApplicationExtensionsTests.cs
│   │   └── CoreBuilderExtensionsTests.cs
│   └── Mocks/
│       ├── MockClient.cs
│       ├── MockServer.cs
│       └── MockService.cs
├── benchmark/
│   ├── Wangkanai.Analytics.Benchmark.csproj
│   ├── AnalyticsBenchmark.cs
│   └── Program.cs
├── Directory.Build.props
├── README.md
├── INSTALL.md
├── build.ps1
└── sign.ps1
```

### Dependencies Analysis
- **Direct Dependency**: Detection module (`$(RepoRoot)\Detection\src\Wangkanai.Detection.csproj`)
- **Framework Reference**: Microsoft.AspNetCore.App
- **Testing Dependencies**: Standard .NET testing framework

### New Repository Structure
The new standalone repository will maintain the same architecture patterns as the main repository:

```
analytics/
├── src/
│   └── Wangkanai.Analytics.csproj
├── tests/
│   └── Wangkanai.Analytics.Tests.csproj  
├── benchmark/
│   └── Wangkanai.Analytics.Benchmark.csproj
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml
│   ├── FUNDING.yml
│   └── RELEASE.yml
├── Assets/
│   ├── wangkanai-logo.afdesign
│   ├── wangkanai-logo.svg
│   ├── wangkanai-logo.ico
│   ├── wangkanai-logo.png
│   └── wangkanai-logo-large.png
├── Analytics.slnx (new SLNX format solution)
├── Directory.Build.props
├── Directory.Build.targets
├── Directory.Packages.props
├── .editorconfig
├── .gitignore
├── .gitattributes
├── build.ps1
├── sign.ps1
├── md-styles.css
├── README.md (based on Analytics/README.md)
├── LICENSE
└── SonarQube.Analysis.xml
```

## Migration Strategy

### Phase 1: Repository Setup & Infrastructure
**Goal**: Establish the new repository with complete infrastructure

#### Infrastructure Tasks:
1. **Repository Configuration**
   - Clone analytics repository locally
   - Set up branch protection rules for main branch
   - Configure GitHub Actions for CI/CD

2. **Solution Structure Setup**
   - Create Analytics.slnx using new SLNX format
   - Configure proper folder structure matching parent repo
   - Set up Directory.Build.props inheritance

3. **Asset Migration**
   - Copy branding assets (logos, icons)
   - Copy configuration files (build scripts, styles)
   - Copy GitHub configuration files

4. **CI/CD Pipeline**
   - Configure GitHub Actions workflow
   - Set up SonarQube Cloud integration
   - Configure automated testing and building

### Phase 2: Code Migration
**Goal**: Migrate all Analytics module code with dependency resolution

#### Code Migration Tasks:
1. **Source Code Transfer**
   - Copy all source files maintaining structure
   - Copy all test files with mocks
   - Copy benchmark projects

2. **Dependency Resolution**
   - Replace Detection project reference with NuGet package
   - Update all using statements and imports
   - Ensure all dependencies are properly referenced

3. **Project File Updates**
   - Update .csproj files for standalone operation
   - Configure NuGet package metadata
   - Set up versioning and packaging

### Phase 3: Configuration & Integration
**Goal**: Ensure the standalone repository works independently

#### Configuration Tasks:
1. **Build System Integration**
   - Configure build.ps1 for standalone operation
   - Set up sign.ps1 for package signing
   - Configure version management

2. **Quality & Testing Setup**
   - Ensure all tests pass independently
   - Configure SonarQube analysis
   - Set up code quality gates

3. **Documentation Updates**
   - Update README.md with standalone instructions
   - Update INSTALL.md for independent installation
   - Create migration documentation

### Phase 4: Package Publishing & Integration
**Goal**: Publish packages and update main repository references

#### Publishing Tasks:
1. **NuGet Package Creation**
   - Build and test packages locally
   - Configure package metadata and versioning
   - Test package installation

2. **Package Publishing**
   - Publish initial version to NuGet.org
   - Verify package availability and functionality
   - Test installation in sample projects

3. **Main Repository Updates**
   - Replace project references with NuGet package references
   - Update solution file to remove Analytics projects
   - Update any dependent modules

### Phase 5: Validation & Cleanup
**Goal**: Ensure successful migration and clean up

#### Validation Tasks:
1. **Integration Testing**
   - Test Analytics functionality via NuGet package
   - Verify all features work as expected
   - Run comprehensive test suites

2. **Documentation Finalization**
   - Update all documentation links
   - Create migration guides
   - Update main repository documentation

3. **Repository Cleanup**
   - Remove Analytics module from main repository
   - Update CI/CD to exclude Analytics
   - Archive or document the migration

## Key Technical Considerations

### SLNX Solution Format
- Use the new `.slnx` format for modern solution management
- Maintain folder-based organization similar to main repository
- Ensure compatibility with modern .NET tooling

### NuGet Package Strategy
- Package Name: `Wangkanai.Analytics`
- Maintain semantic versioning
- Include proper dependency declarations
- Ensure backward compatibility

### CI/CD Pipeline Requirements
- **Build Triggers**: Push to main branch, Pull Requests
- **Testing**: Run all unit tests, integration tests, benchmarks
- **Quality Gates**: SonarQube analysis, code coverage
- **Package Publishing**: Automatic NuGet publishing on successful builds
- **Branch Protection**: Require PR approval, passing CI checks

### SonarQube Integration
- **Organization**: wangkanai
- **Project Key**: wangkanai_analytics
- **Token**: Use GitHub Secrets (`SONAR_TOKEN`)
- **Version**: 1.0.0 (starting version)

## Dependencies Management

### Current Dependencies to Resolve:
1. **Wangkanai.Detection**: Replace project reference with NuGet package
2. **Microsoft.AspNetCore.App**: Framework reference (maintained)
3. **Testing Framework**: Standard .NET testing (maintained)

### New NuGet Dependencies:
- Update to latest Wangkanai.Detection package from NuGet
- Ensure version compatibility across all packages
- Maintain dependency chain integrity

## Risk Assessment & Mitigation

### High Risk Areas:
1. **Dependency Conflicts**: Detection module compatibility
   - **Mitigation**: Test thoroughly with latest Detection NuGet package
2. **CI/CD Pipeline**: Complex GitHub Actions setup
   - **Mitigation**: Incremental testing of pipeline components
3. **Package Publishing**: First-time standalone package
   - **Mitigation**: Test in isolated environment first

### Medium Risk Areas:
1. **SLNX Format**: New solution format adoption
   - **Mitigation**: Fallback to traditional .sln if needed
2. **Asset Migration**: Ensuring all required files are copied
   - **Mitigation**: Comprehensive checklist and verification

### Low Risk Areas:
1. **Code Migration**: Straightforward file copying
2. **Documentation**: Well-defined process

## Success Criteria

### Phase Completion Criteria:
- [ ] New repository fully configured with CI/CD
- [ ] All Analytics code successfully migrated
- [ ] All tests passing in new environment
- [ ] NuGet package published and functional
- [ ] Main repository successfully using NuGet package
- [ ] All documentation updated and accurate

### Quality Gates:
- All unit tests pass (100% success rate)
- SonarQube analysis shows no blocking issues
- Code coverage maintains current levels
- Package installs and functions correctly
- No breaking changes for consumers

## Timeline Estimation

### Phase 1 (Repository Setup): 2-3 hours
### Phase 2 (Code Migration): 3-4 hours  
### Phase 3 (Configuration): 2-3 hours
### Phase 4 (Publishing): 2-3 hours
### Phase 5 (Validation): 1-2 hours

**Total Estimated Time**: 10-15 hours

## Next Steps

1. Review and approve this migration plan
2. Execute Phase 1: Repository Setup & Infrastructure
3. Proceed through phases systematically with validation at each step
4. Monitor for issues and adjust plan as needed
5. Complete final validation and documentation updates