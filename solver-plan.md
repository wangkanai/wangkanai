# Solver Repository Migration Plan

## Overview

Migration of the @Solver/ module from the main wangkanai repository to a dedicated repository at https://github.com/wangkanai/solver. This plan ensures smooth transition while maintaining architecture
consistency, CI/CD integration, and proper dependency management.

## Repository Analysis

- **Source Module**: `/Solver/` containing 3 projects
   - `src/Wangkanai.Solver.csproj` (main library)
   - `tests/Wangkanai.Solver.Tests.csproj` (unit tests)
   - `benchmark/Wangkanai.Solver.Benchmark.csproj` (performance benchmarks)
- **Current Version**: 2.4.0
- **Architecture**: Follows wangkanai standard structure with Directory.Build.props inheritance

## Migration Objectives

### Primary Goals

1. **Repository Setup**: Create standalone solver repository with complete architecture
2. **CI/CD Integration**: Implement GitHub Actions with SonarCloud integration
3. **Dependency Management**: Replace internal references with NuGet packages
4. **Documentation**: Migrate and update all relevant documentation
5. **Branch Protection**: Configure main branch protection requiring PR reviews

### Secondary Goals

1. **Quality Assurance**: Maintain test coverage and quality metrics
2. **Performance**: Preserve benchmark capabilities
3. **Community**: Ensure proper funding and release configurations

## Architecture Strategy

### Repository Structure

```
solver/
├── .github/
│   ├── workflows/
│   │   └── dotnet.yml           # CI/CD pipeline
│   ├── FUNDING.yml              # GitHub funding
│   └── RELEASE.yml              # Release configuration
├── src/
│   └── Wangkanai.Solver.csproj  # Main library project
├── tests/
│   └── Wangkanai.Solver.Tests.csproj    # Unit tests
├── benchmark/
│   └── Wangkanai.Solver.Benchmark.csproj # Performance tests
├── Directory.Build.props         # Project configuration
├── Directory.Build.targets       # Build targets (if needed)
├── Directory.Packages.props      # Package management
├── build.ps1                    # Build automation
├── sign.ps1                     # Package signing
├── md-styles.css                # Documentation styles
├── README.md                    # Project documentation
├── LICENSE                      # License file
└── .gitignore                   # Git ignore rules
```

### Dependencies Strategy

- **Internal Dependencies**: Replace with NuGet package references
- **External Dependencies**: Migrate existing package references
- **Version Management**: Use Central Package Management (CPM)

## Implementation Phases

### Phase 1: Repository Initialization

- Clone new repository locally
- Set up basic structure and configuration files
- Copy core architecture files (Directory.Build.props, etc.)
- Configure git repository settings

### Phase 2: Code Migration

- Copy source code from Solver module
- Update project references to NuGet packages
- Migrate test projects with dependency updates
- Transfer benchmark projects

### Phase 3: CI/CD Configuration

- Configure GitHub Actions workflow
- Set up SonarCloud integration
- Configure branch protection rules
- Test build and deployment pipeline

### Phase 4: Documentation & Assets

- Migrate README.md as primary documentation
- Copy required assets and styling files
- Update documentation URLs and badges
- Configure funding and release settings

### Phase 5: Quality Assurance

- Run full test suite
- Execute benchmark validation
- Perform security scanning
- Validate NuGet package generation

### Phase 6: Integration Testing

- Test CI/CD pipeline end-to-end
- Validate SonarCloud reporting
- Test branch protection enforcement
- Verify automated releases

## Risk Assessment & Mitigation

### High Risk Items

1. **Dependency Breaking Changes**: NuGet package compatibility issues
   - *Mitigation*: Thorough testing with latest package versions
2. **CI/CD Configuration**: Complex build pipeline setup
   - *Mitigation*: Incremental testing of each workflow component
3. **Branch Protection**: Blocking development workflow
   - *Mitigation*: Gradual rollout with team validation

### Medium Risk Items

1. **Documentation Links**: Broken references to old repository
   - *Mitigation*: Systematic URL updating and validation
2. **Asset Dependencies**: Missing logos or styling files
   - *Mitigation*: Complete asset inventory and migration

## Success Criteria

- [x] Repository cloned and initialized
- [ ] Complete source code migration
- [ ] All tests passing in new environment
- [ ] CI/CD pipeline operational with SonarCloud
- [ ] Branch protection rules enforced
- [ ] Documentation updated and accessible
- [ ] NuGet packages publishing successfully

## Timeline Estimation

- **Phase 1**: 30 minutes (Repository setup)
- **Phase 2**: 45 minutes (Code migration)
- **Phase 3**: 60 minutes (CI/CD setup)
- **Phase 4**: 30 minutes (Documentation)
- **Phase 5**: 45 minutes (Quality assurance)
- **Phase 6**: 30 minutes (Integration testing)

**Total Estimated Time**: 4 hours

## Next Steps

1. Execute detailed workflow todos in solver-todos.md
2. Begin Phase 1 implementation
3. Validate each phase before proceeding
4. Document any deviations from plan