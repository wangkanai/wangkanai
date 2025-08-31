# Domain Repository Migration Plan

## Overview

Migration of Domain, Audit, and EntityFramework modules from the main wangkanai repository to a new dedicated repository at `https://github.com/wangkanai/domain`. This migration will establish these
core data/persistence modules as independent NuGet packages while maintaining the same architectural patterns and quality standards.

## Repository Details

- **New Repository**: https://github.com/wangkanai/domain
- **Target Location**: ~/Sources/domain/
- **Solution Format**: Domain.slnx (new slnx format)
- **Main Branch**: main
- **Initial Version**: 1.0.0

## Architecture Overview

The new repository will use a flattened structure organized by project type:

```
domain/
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml          # CI/CD pipeline
│   ├── FUNDING.yml             # Sponsorship links
│   └── RELEASE.yml             # Release configuration
├── Assets/                     # Brand assets
│   ├── wangkanai-logo.*        # Various logo formats
├── src/                        # Source projects
│   ├── Domain/                 # Domain module source
│   ├── Audit/                  # Audit module source
│   └── EntityFramework/        # EntityFramework utilities source
├── tests/                      # Test projects
│   ├── Domain/                 # Domain module tests
│   ├── Audit/                  # Audit module tests
│   └── EntityFramework/        # EntityFramework utilities tests
├── benchmark/                  # Benchmark projects
│   ├── Domain/                 # Domain module benchmarks
│   ├── Audit/                  # Audit module benchmarks
│   └── EntityFramework/        # EntityFramework utilities benchmarks
├── build.ps1                   # Build script
├── sign.ps1                    # Signing script
├── md-styles.css               # Documentation styling
├── Domain.slnx                 # Solution file
├── Directory.Build.props       # MSBuild properties
├── Directory.Build.targets     # MSBuild targets
├── Directory.Packages.props    # Package management
└── README.md                   # Multi-module documentation
```

## Dependencies Analysis

- **Audit → Domain**: Audit module depends on Domain module internally
- **EntityFramework**: Self-contained with EF Core utilities and generators
- **No External Dependencies**: No other wangkanai modules depend on Domain, Audit, or EntityFramework
- **Clean Migration**: All three modules can be migrated together without breaking other modules

## Migration Strategy

### Phase 1: Repository Preparation

1. Clone the new repository
2. Set up basic repository structure
3. Configure CI/CD pipeline
4. Set up branch protection rules

### Phase 2: Code Migration

1. Copy Domain, Audit, and EntityFramework modules with reorganized folder structure
2. Restructure from module/type to type/module organization (src/, tests/, benchmark/)
3. Update project references and solution file
4. Copy supporting files (build scripts, assets, configuration)
5. Update package references and versions

### Phase 3: Integration & Testing

1. Configure SonarQube Cloud integration
2. Test local build and CI pipeline
3. Run all tests and benchmarks
4. Validate package generation

### Phase 4: Main Repository Updates

1. Remove Domain, Audit, and EntityFramework folders from main repository
2. Update solution file to exclude migrated projects
3. Add NuGet package references where needed
4. Update documentation and references

### Phase 5: Quality Assurance

1. Verify all tests pass in both repositories
2. Validate CI/CD pipelines
3. Test NuGet package generation and consumption
4. Update documentation and changelog

## SonarQube Cloud Configuration

- **Organization**: wangkanai
- **Project Key**: wangkanai_domain
- **Token**: {{secrets.SONAR_TOKEN}} (already configured)
- **Version**: 1.0.0
- **Host URL**: https://sonarcloud.io

## Branch Protection Strategy

- **Main branch protection**: Require pull request reviews
- **Status checks**: Require CI pipeline success
- **Merge restrictions**: Squash and merge preferred
- **Auto-delete head branches**: Enabled

## Files to Copy

### Root Level

- sign.ps1
- build.ps1
- md-styles.css

### GitHub Configuration

- .github/FUNDING.yml
- .github/RELEASE.yml
- .github/workflows/dotnet.yml (customized for domain)

### Assets

- Assets/wangkanai-logo.afdesign
- Assets/wangkanai-logo.svg
- Assets/wangkanai-logo.ico
- Assets/wangkanai-logo.png
- Assets/wangkanai-logo-large.png

### Module Folders (Complete with Restructuring)

- Domain/ → src/Domain/, tests/Domain/, benchmark/Domain/
- Audit/ → src/Audit/, tests/Audit/, benchmark/Audit/
- EntityFramework/ → src/EntityFramework/, tests/EntityFramework/, benchmark/EntityFramework/

## Risk Assessment

### High Risk

- **Circular dependencies**: Ensure clean separation during migration
- **Version conflicts**: Manage package versions carefully
- **CI/CD complexity**: New pipeline needs thorough testing

### Medium Risk

- **Documentation updates**: Ensure all references are updated
- **Package publishing**: First-time setup may require iteration
- **Branch protection**: Coordinate with team on workflow changes

### Low Risk

- **Asset copying**: Straightforward file operations
- **Basic configuration**: Well-established patterns to follow

## Success Criteria

- [ ] New repository builds successfully locally
- [ ] CI/CD pipeline runs without errors
- [ ] All tests pass in new repository
- [ ] SonarQube integration works correctly
- [ ] NuGet packages can be generated and published
- [ ] Main repository updated and still builds successfully
- [ ] Documentation is accurate and complete

## Rollback Strategy

1. **Pre-migration backup**: Create backup branch in main repository
2. **Incremental approach**: Complete migration in phases for easy rollback
3. **Validation gates**: Test each phase before proceeding
4. **Repository state**: Keep original repository intact until validation complete

## Timeline Estimate

- **Phase 1**: 2-3 hours (Repository setup and configuration)
- **Phase 2**: 3-4 hours (Code migration and project updates)
- **Phase 3**: 2-3 hours (Integration testing and CI setup)
- **Phase 4**: 2-3 hours (Main repository cleanup)
- **Phase 5**: 1-2 hours (Final validation and documentation)

**Total Estimated Time**: 10-15 hours across multiple sessions

## Next Steps

1. Execute detailed todos in domain-todos.md
2. Begin with Phase 1: Repository Preparation
3. Validate each phase before proceeding to the next
4. Maintain communication throughout the migration process