# Nation Migration Epic - Detailed Task Breakdown

## Phase 1: Repository Setup and Local Environment

### Task 1.1: Local Repository Setup

- [ ] Clone target repository to `~/Sources/nation/`
- [ ] Verify repository access and permissions
- [ ] Set up initial branch structure
- [ ] Configure git remotes and authentication

### Task 1.2: Directory Structure Creation

- [ ] Create `src/` directory
- [ ] Create `test/` directory
- [ ] Create `benchmark/` directory
- [ ] Create `.github/` directory structure
- [ ] Create `.github/workflows/` directory
- [ ] Create `Assets/` directory

### Task 1.3: Basic Configuration Files

- [ ] Create `Nation.slnx` solution file (empty template)
- [ ] Create basic `Directory.Build.props`
- [ ] Create basic `.gitignore` file
- [ ] Create basic `README.md` placeholder

## Phase 2: Source Code Migration

### Task 2.1: Core Source Files

- [ ] Copy `Nation/src/` → `nation/src/`
- [ ] Copy `Nation/test/` → `nation/test/`
- [ ] Copy `Nation/benchmark/` → `nation/benchmark/`
- [ ] Verify all source files are copied correctly

### Task 2.2: Project File Updates

- [ ] Update `src/Wangkanai.Nation.csproj` for standalone operation
- [ ] Update `test/Wangkanai.Nation.Tests.csproj` references
- [ ] Update `benchmark/Wangkanai.Nation.Benchmark.csproj` references
- [ ] Remove parent project dependencies

### Task 2.3: Solution Configuration

- [ ] Populate `Nation.slnx` with project references
- [ ] Add folder structure to solution
- [ ] Add configuration files to solution
- [ ] Verify solution loads correctly

### Task 2.4: Dependency Management

- [ ] Create standalone `Directory.Packages.props`
- [ ] Extract Nation-specific dependencies from parent
- [ ] Update package versions to latest stable
- [ ] Resolve any dependency conflicts

## Phase 3: Infrastructure Setup

### Task 3.1: GitHub Actions Workflow

- [ ] Copy and adapt `.github/workflows/dotnet.yml`
- [ ] Update workflow name to "dotnet"
- [ ] Configure triggers (push to main, PR)
- [ ] Set up build matrix if needed
- [ ] Configure test execution
- [ ] Set up code coverage reporting

### Task 3.2: SonarCloud Integration

- [ ] Configure SonarCloud project settings
- [ ] Add organization: `wangkanai`
- [ ] Set project key: `wangkanai_nation`
- [ ] Configure token reference: `${{ secrets.SONAR_TOKEN }}`
- [ ] Set version: `1.0.0`
- [ ] Test SonarCloud connectivity

### Task 3.3: Branch Protection Setup

- [ ] Enable branch protection for main branch
- [ ] Require pull request reviews
- [ ] Require status checks to pass
- [ ] Require up-to-date branches
- [ ] Configure required reviewers if needed

### Task 3.4: Build Scripts Migration

- [ ] Copy and adapt `build.ps1`
- [ ] Copy and adapt `sign.ps1`
- [ ] Update script paths for new structure
- [ ] Test script execution locally
- [ ] Update script documentation

## Phase 4: Documentation and Assets

### Task 4.1: Primary Documentation

- [ ] Copy `Nation/README.md` as primary README
- [ ] Update README links and references
- [ ] Add installation instructions
- [ ] Add usage examples
- [ ] Update project URLs

### Task 4.2: Brand Assets Migration

- [ ] Copy `Assets/wangkanai-logo.afdesign`
- [ ] Copy `Assets/wangkanai-logo.svg`
- [ ] Copy `Assets/wangkanai-logo.ico`
- [ ] Copy `Assets/wangkanai-logo.png`
- [ ] Copy `Assets/wangkanai-logo-large.png`
- [ ] Verify asset paths in project files

### Task 4.3: GitHub Configuration Files

- [ ] Copy `.github/FUNDING.yml`
- [ ] Copy `.github/RELEASE.yml`
- [ ] Copy `md-styles.css`
- [ ] Update any repository-specific references
- [ ] Test GitHub feature functionality

### Task 4.4: Additional Documentation

- [ ] Create `CONTRIBUTING.md` if needed
- [ ] Create `LICENSE` file if needed
- [ ] Create `CHANGELOG.md` template
- [ ] Update package documentation

## Phase 5: Integration Testing and Validation

### Task 5.1: Local Build Testing

- [ ] Execute full build locally
- [ ] Run all unit tests
- [ ] Execute benchmark tests
- [ ] Verify NuGet package generation
- [ ] Test signing process

### Task 5.2: CI/CD Pipeline Validation

- [ ] Push initial commit to trigger pipeline
- [ ] Monitor GitHub Actions execution
- [ ] Verify build success
- [ ] Verify test execution
- [ ] Check code coverage reports

### Task 5.3: SonarCloud Validation

- [ ] Verify SonarCloud project creation
- [ ] Check code quality metrics
- [ ] Review security analysis
- [ ] Validate coverage integration
- [ ] Test quality gates

### Task 5.4: Package Testing

- [ ] Generate NuGet package locally
- [ ] Test package installation
- [ ] Verify package metadata
- [ ] Test package functionality
- [ ] Validate package dependencies

## Phase 6: Parent Repository Integration

### Task 6.1: NuGet Package Reference

- [ ] Remove Nation projects from `Wangkanai.slnx`
- [ ] Add NuGet package reference where needed
- [ ] Update any direct project references
- [ ] Test parent repository builds

### Task 6.2: Build Script Updates

- [ ] Update `build.ps1` to exclude Nation
- [ ] Update `sign.ps1` to exclude Nation
- [ ] Remove Nation from directory arrays
- [ ] Test build script execution

### Task 6.3: Documentation Updates

- [ ] Update parent repository README
- [ ] Add migration notes
- [ ] Update project structure documentation
- [ ] Create migration announcement

### Task 6.4: Repository Cleanup

- [ ] Remove `Nation/` directory from parent
- [ ] Update solution file references
- [ ] Clean up any remaining Nation references
- [ ] Commit and push cleanup changes

## Phase 7: Final Validation and Documentation

### Task 7.1: End-to-End Testing

- [ ] Full parent repository build test
- [ ] Full nation repository build test
- [ ] Integration testing between repos
- [ ] Performance benchmark validation
- [ ] Documentation accuracy check

### Task 7.2: Release Preparation

- [ ] Tag initial release (v1.0.0)
- [ ] Create GitHub release
- [ ] Publish NuGet package (if automated)
- [ ] Update version numbers
- [ ] Create release notes

### Task 7.3: Migration Documentation

- [ ] Document migration process
- [ ] Create troubleshooting guide
- [ ] Update contribution guidelines
- [ ] Create maintenance procedures
- [ ] Archive migration artifacts

## Success Metrics

- [ ] Nation repository builds successfully ✅
- [ ] All tests pass (100% success rate) ✅
- [ ] GitHub Actions pipeline green ✅
- [ ] SonarCloud quality gate passes ✅
- [ ] NuGet package installs correctly ✅
- [ ] Parent repository builds after changes ✅
- [ ] Zero breaking changes introduced ✅
- [ ] Documentation is accurate and complete ✅

## Rollback Plan (if needed)

- [ ] Revert parent repository changes
- [ ] Restore Nation module to original location
- [ ] Restore original solution file
- [ ] Restore original build scripts
- [ ] Communicate rollback to stakeholders

---
**Total Tasks**: 52 individual tasks across 7 phases
**Estimated Duration**: 6-9 hours
**Risk Level**: Medium (well-defined scope with clear rollback path)