# Markdown Module Migration Todos

## Epic Assignment: Migrate Markdown Module to Standalone Repository

This document provides a comprehensive checklist for migrating the Markdown module from `wangkanai/wangkanai` to the dedicated `wangkanai/markdown` repository.

---

## Phase 1: Repository Setup and Initial Configuration

### Task 1.1: Repository Preparation
- [ ] Clone the `wangkanai/markdown` repository to `~/Sources/markdown/`
- [ ] Verify repository access and permissions
- [ ] Create initial branch for migration work (`feature/initial-migration`)
- [ ] Set up local git configuration for the new repository

### Task 1.2: Directory Structure Creation
- [ ] Create `/src/` directory for main library code
- [ ] Create `/tests/` directory for unit tests
- [ ] Create `/benchmark/` directory for performance tests
- [ ] Create `/.github/workflows/` directory for CI/CD
- [ ] Create `/Assets/` directory for brand assets

### Task 1.3: Asset Migration
- [ ] Copy `Assets/wangkanai-logo.afdesign` from source repository
- [ ] Copy `Assets/wangkanai-logo.svg` from source repository
- [ ] Copy `Assets/wangkanai-logo.ico` from source repository
- [ ] Copy `Assets/wangkanai-logo.png` from source repository
- [ ] Copy `Assets/wangkanai-logo-large.png` from source repository
- [ ] Copy `md-styles.css` from source repository root
- [ ] Copy `.github/FUNDING.yml` from source repository
- [ ] Copy `.github/RELEASE.yml` from source repository

### Task 1.4: Documentation Migration
- [ ] Copy `Markdown/README.md` as the main repository README
- [ ] Copy `Markdown/INSTALL.md` to repository root
- [ ] Update README.md to reflect standalone repository context
- [ ] Update package URLs and links in documentation
- [ ] Add migration notes and changelog

---

## Phase 2: Code and Project Migration

### Task 2.1: Source Code Migration
- [ ] Copy entire `Markdown/src/` directory contents to new `/src/`
- [ ] Verify all source files are present and intact
- [ ] Update namespace references if needed
- [ ] Check for any hardcoded paths or dependencies
- [ ] Validate file structure matches expected layout

### Task 2.2: Test Project Migration
- [ ] Copy entire `Markdown/tests/` directory contents to new `/tests/`
- [ ] Verify all test files are present and functional
- [ ] Update test project references and dependencies
- [ ] Check for any test data or configuration files
- [ ] Validate test project structure

### Task 2.3: Benchmark Project Migration
- [ ] Copy entire `Markdown/benchmark/` directory contents to new `/benchmark/`
- [ ] Verify all benchmark files and configurations
- [ ] Update benchmark project references
- [ ] Check benchmark data and baseline files
- [ ] Validate benchmark project structure

### Task 2.4: Build Scripts Migration
- [ ] Copy `sign.ps1` from `Markdown/` directory
- [ ] Copy `build.ps1` from `Markdown/` directory
- [ ] Update script paths and references for standalone repository
- [ ] Modify script parameters for new repository context
- [ ] Test script functionality in new environment

### Task 2.5: Build Configuration Migration
- [ ] Copy `Markdown/Directory.Build.props` to repository root
- [ ] Update parent directory import references
- [ ] Modify package URLs to point to new repository
- [ ] Update project references and dependencies
- [ ] Validate build properties and configurations

---

## Phase 3: Solution Configuration and Project Setup

### Task 3.1: Solution File Creation
- [ ] Create `Wangkanai.Markdown.slnx` using new slnx format
- [ ] Add main library project (`src/Wangkanai.Markdown.csproj`)
- [ ] Add test project (`tests/Wangkanai.Markdown.Tests.csproj`)
- [ ] Add benchmark project (`benchmark/Wangkanai.Markdown.Benchmark.csproj`)
- [ ] Add solution-level files (README.md, Directory.Build.props, etc.)

### Task 3.2: Project Reference Updates
- [ ] Update project references in test project to point to src
- [ ] Update benchmark project references to point to src
- [ ] Replace any wangkanai internal project references with NuGet packages
- [ ] Verify all PackageReference entries are correct
- [ ] Update framework target versions if needed

### Task 3.3: Build Validation
- [ ] Perform full solution restore (`dotnet restore`)
- [ ] Build entire solution (`dotnet build`)
- [ ] Verify no compilation errors or warnings
- [ ] Check all projects build successfully
- [ ] Validate output artifacts are generated correctly

### Task 3.4: Test Validation
- [ ] Run all unit tests (`dotnet test`)
- [ ] Verify all tests pass in new environment
- [ ] Check test coverage reports
- [ ] Validate test output and logging
- [ ] Fix any test failures related to migration

---

## Phase 4: CI/CD Pipeline Implementation

### Task 4.1: GitHub Actions Workflow Creation
- [ ] Create `.github/workflows/dotnet.yml` workflow file
- [ ] Configure workflow name as "dotnet"
- [ ] Set trigger for main branch pushes and pull requests
- [ ] Add .NET SDK setup and dependency caching
- [ ] Configure multi-target framework builds

### Task 4.2: Build Pipeline Configuration
- [ ] Add restore step with dependency caching
- [ ] Configure build step with Release configuration
- [ ] Add comprehensive test execution with coverage
- [ ] Configure parallel test execution for performance
- [ ] Add build artifact collection

### Task 4.3: SonarQube Integration
- [ ] Add SonarQube scanner initialization
- [ ] Configure organization as `wangkanai`
- [ ] Set project key as `wangkanai_markdown`
- [ ] Configure version as `1.0.0`
- [ ] Add coverage report configuration
- [ ] Add quality gate checks and exclusions

### Task 4.4: Quality and Security Checks
- [ ] Add code coverage collection and reporting
- [ ] Configure security vulnerability scanning
- [ ] Add dependency vulnerability checks
- [ ] Configure code quality thresholds
- [ ] Add performance regression detection

### Task 4.5: Workflow Testing and Validation
- [ ] Test workflow execution on feature branch
- [ ] Verify all workflow steps complete successfully
- [ ] Validate SonarQube analysis results
- [ ] Check coverage reports and quality metrics
- [ ] Test failure handling and notifications

---

## Phase 5: Branch Protection and Repository Settings

### Task 5.1: Branch Protection Configuration
- [ ] Enable branch protection for main branch
- [ ] Require pull request reviews before merging
- [ ] Require status checks to pass before merging
- [ ] Include CI workflow as required status check
- [ ] Enable "Require branches to be up to date before merging"

### Task 5.2: Repository Settings
- [ ] Configure repository description and topics
- [ ] Set repository homepage URL
- [ ] Enable Issues and Wiki if needed
- [ ] Configure repository visibility and access
- [ ] Set up repository administrators and collaborators

### Task 5.3: Secret Management
- [ ] Verify `SONAR_TOKEN` secret is configured
- [ ] Add any additional required secrets
- [ ] Test secret access in workflow
- [ ] Document secret requirements
- [ ] Configure secret rotation if needed

### Task 5.4: Integration Testing
- [ ] Create test pull request to validate protection rules
- [ ] Verify PR requires review and status checks
- [ ] Test branch protection enforcement
- [ ] Validate CI workflow execution on PR
- [ ] Test merge process and requirements

---

## Phase 6: Source Repository Integration Updates

### Task 6.1: Main Repository Dependency Updates
- [ ] Identify all projects that reference Markdown module
- [ ] Update project references to NuGet package references
- [ ] Add Wangkanai.Markdown NuGet package dependency
- [ ] Update version constraints and compatibility
- [ ] Test compilation of dependent projects

### Task 6.2: Build System Updates
- [ ] Remove Markdown folder from main solution file
- [ ] Update main repository build scripts
- [ ] Remove Markdown-specific build configurations
- [ ] Update CI/CD to exclude Markdown module
- [ ] Validate main repository builds successfully

### Task 6.3: Documentation Updates
- [ ] Update main repository README references
- [ ] Add migration notes and new repository links
- [ ] Update architecture documentation
- [ ] Add deprecation notices for old references
- [ ] Update developer setup instructions

### Task 6.4: Final Validation
- [ ] Build and test main repository without Markdown module
- [ ] Verify all dependent projects work with NuGet package
- [ ] Test full integration with new standalone package
- [ ] Validate no broken references or missing dependencies
- [ ] Perform smoke tests on affected functionality

---

## Phase 7: Release and Publication

### Task 7.1: Package Preparation
- [ ] Finalize package version and metadata
- [ ] Generate package documentation and release notes
- [ ] Create package signing configuration
- [ ] Prepare package for NuGet publication
- [ ] Validate package contents and structure

### Task 7.2: Release Management
- [ ] Create release branch for first version
- [ ] Tag repository with initial version
- [ ] Create GitHub release with changelog
- [ ] Publish NuGet package to official feed
- [ ] Update package documentation and links

### Task 7.3: Communication and Documentation
- [ ] Update project wikis and documentation sites
- [ ] Notify stakeholders of migration completion
- [ ] Publish migration guide and best practices
- [ ] Update community resources and links
- [ ] Archive old documentation appropriately

### Task 7.4: Post-Migration Monitoring
- [ ] Monitor CI/CD pipeline for stability
- [ ] Track NuGet package downloads and usage
- [ ] Monitor for integration issues or bug reports
- [ ] Validate SonarQube quality metrics over time
- [ ] Ensure all automated processes are functioning

---

## Validation Checklist

### Technical Validation
- [ ] All code compiles without errors or warnings
- [ ] All tests pass with expected coverage levels
- [ ] CI/CD pipeline executes successfully
- [ ] SonarQube quality gate passes
- [ ] Package generates and installs correctly
- [ ] No security vulnerabilities detected

### Process Validation
- [ ] Branch protection rules are active and enforced
- [ ] Pull request workflow functions properly
- [ ] Code review process is established
- [ ] Automated testing prevents regressions
- [ ] Release process is documented and tested

### Documentation Validation
- [ ] README is complete and accurate
- [ ] Installation instructions are current
- [ ] API documentation is up to date
- [ ] Migration notes are comprehensive
- [ ] Troubleshooting guides are available

### Integration Validation
- [ ] Main repository builds without Markdown module
- [ ] NuGet package integration works correctly
- [ ] No breaking changes introduced
- [ ] Performance meets baseline requirements
- [ ] All dependent projects function properly

---

## Success Metrics

- **Repository Migration**: ✅ Complete migration with no data loss
- **Build Success**: ✅ 100% successful builds in CI/CD
- **Test Coverage**: ✅ Maintain or exceed current test coverage levels
- **Quality Gates**: ✅ Pass all SonarQube quality and security checks
- **Integration**: ✅ Seamless integration with main repository via NuGet
- **Documentation**: ✅ Complete and accurate documentation for new repository

---

## Emergency Rollback Procedures

In case of critical issues during migration:

1. **Immediate Actions**
   - [ ] Stop all migration work immediately
   - [ ] Document the specific issue encountered
   - [ ] Assess impact on dependent systems

2. **Rollback Steps**
   - [ ] Restore original Markdown module in main repository
   - [ ] Revert any dependency changes made to other projects
   - [ ] Re-enable original build and test configurations
   - [ ] Validate main repository functionality is restored

3. **Recovery Planning**
   - [ ] Analyze root cause of migration failure
   - [ ] Develop remediation plan for identified issues
   - [ ] Update migration procedures to prevent recurrence
   - [ ] Schedule reattempt with improved planning

---

*This comprehensive todo list serves as the execution guide for the Markdown module migration epic. Each checkbox should be verified and marked as complete with proper validation before proceeding to dependent tasks.*