# Solver Migration Workflow Todos

## Phase 1: Repository Initialization

**Objective**: Set up new solver repository with basic structure and configuration

### Task 1.1: Repository Setup

- [ ] Clone repository to ~/Sources/solver/
- [ ] Verify repository structure and permissions
- [ ] Configure git settings (user, email, etc.)
- [ ] Create initial directory structure

### Task 1.2: Core Configuration Files

- [ ] Copy Directory.Build.props from current Solver module
- [ ] Update Directory.Build.props for standalone repository
- [ ] Create Directory.Packages.props for central package management
- [ ] Set up .gitignore file for .NET projects
- [ ] Copy LICENSE file from parent repository

### Task 1.3: Build Automation Setup

- [ ] Copy build.ps1 script to new repository
- [ ] Copy sign.ps1 script to new repository
- [ ] Update build scripts for standalone operation
- [ ] Test build script functionality

## Phase 2: Code Migration

**Objective**: Transfer all Solver module code with dependency updates

### Task 2.1: Source Code Transfer

- [ ] Copy src/Wangkanai.Solver.csproj to new repository
- [ ] Copy all source code files from src/ directory
- [ ] Update project file for standalone operation
- [ ] Remove parent repository references

### Task 2.2: Test Project Migration

- [ ] Copy tests/Wangkanai.Solver.Tests.csproj to new repository
- [ ] Copy all test files from tests/ directory
- [ ] Update test project references to NuGet packages
- [ ] Verify test project builds successfully

### Task 2.3: Benchmark Project Migration

- [ ] Copy benchmark/Wangkanai.Solver.Benchmark.csproj to new repository
- [ ] Copy all benchmark files from benchmark/ directory
- [ ] Update benchmark project dependencies
- [ ] Verify benchmark project builds successfully

### Task 2.4: Dependency Management

- [ ] Identify all internal wangkanai dependencies
- [ ] Replace internal references with NuGet package references
- [ ] Update package versions to latest stable
- [ ] Test all projects build without errors

## Phase 3: CI/CD Configuration

**Objective**: Set up GitHub Actions with SonarCloud integration

### Task 3.1: GitHub Actions Workflow

- [ ] Create .github/workflows/ directory
- [ ] Copy dotnet.yml from parent repository as template
- [ ] Update workflow for solver-specific configuration
- [ ] Configure workflow triggers (push to main, PRs)

### Task 3.2: SonarCloud Integration

- [ ] Configure SonarCloud project settings
- [ ] Update workflow with SonarCloud scanner configuration
- [ ] Set organization: wangkanai
- [ ] Set project key: wangkanai_solver
- [ ] Verify SONAR_TOKEN secret is available
- [ ] Set version: 1.0.0

### Task 3.3: Branch Protection Setup

- [ ] Navigate to repository Settings > Branches
- [ ] Add branch protection rule for main branch
- [ ] Require pull request reviews before merging
- [ ] Require status checks to pass (dotnet workflow)
- [ ] Restrict pushes that create files to main branch
- [ ] Test protection rules with dummy PR

### Task 3.4: Workflow Testing

- [ ] Push initial code to trigger workflow
- [ ] Verify build process completes successfully
- [ ] Check SonarCloud analysis results
- [ ] Validate test execution and coverage reporting
- [ ] Confirm NuGet package artifacts generation

## Phase 4: Documentation & Assets

**Objective**: Migrate and update all project documentation

### Task 4.1: Primary Documentation

- [ ] Copy README.md from Solver module as base
- [ ] Update README.md badges for new repository URLs
- [ ] Update GitHub Actions badge URL
- [ ] Update SonarCloud badge with new project key
- [ ] Update NuGet package URLs and badges

### Task 4.2: GitHub Configuration Files

- [ ] Copy .github/FUNDING.yml from parent repository
- [ ] Copy .github/RELEASE.yml from parent repository
- [ ] Update any repository-specific references in config files
- [ ] Verify funding configurations work correctly

### Task 4.3: Styling and Assets

- [ ] Copy md-styles.css to new repository
- [ ] Verify any logo/asset dependencies
- [ ] Update documentation styling references if needed
- [ ] Test documentation rendering

### Task 4.4: Repository Settings

- [ ] Update repository description on GitHub
- [ ] Add repository topics/tags for discoverability
- [ ] Configure repository homepage URL
- [ ] Set up repository social preview image if available

## Phase 5: Quality Assurance

**Objective**: Validate functionality and quality metrics

### Task 5.1: Build Validation

- [ ] Run complete build process locally
- [ ] Execute all unit tests and verify pass rate
- [ ] Run benchmark tests to ensure performance baseline
- [ ] Validate NuGet package generation

### Task 5.2: Code Quality Checks

- [ ] Review SonarCloud quality gate results
- [ ] Address any critical code quality issues
- [ ] Verify test coverage meets expected thresholds
- [ ] Check for security vulnerabilities

### Task 5.3: Integration Testing

- [ ] Test package installation from generated NuGet packages
- [ ] Create simple integration test project
- [ ] Verify API compatibility and functionality
- [ ] Test package in different .NET versions if applicable

## Phase 6: Final Integration

**Objective**: Complete setup and validate end-to-end workflow

### Task 6.1: End-to-End Pipeline Test

- [ ] Create feature branch for testing
- [ ] Make minor change and create pull request
- [ ] Verify PR triggers CI pipeline
- [ ] Confirm branch protection prevents direct merge
- [ ] Complete PR review and merge process

### Task 6.2: Release Process Validation

- [ ] Tag initial release (v2.4.0)
- [ ] Verify release workflow triggers correctly
- [ ] Confirm NuGet package publishing to registry
- [ ] Test package consumption from NuGet.org

### Task 6.3: Documentation Verification

- [ ] Verify all links in README.md work correctly
- [ ] Check that badges display proper status
- [ ] Confirm funding links function properly
- [ ] Validate repository appears correctly in searches

### Task 6.4: Cleanup and Finalization

- [ ] Remove any temporary files or test artifacts
- [ ] Verify .gitignore excludes appropriate files
- [ ] Confirm repository size is reasonable
- [ ] Update any parent repository references to point to new repo

## Post-Migration Tasks

**Objective**: Update parent repository to reference new standalone repository

### Task 7.1: Parent Repository Updates

- [ ] Update parent repository build scripts to exclude Solver
- [ ] Update solution file to remove Solver projects
- [ ] Add NuGet package reference to Solver in any dependent projects
- [ ] Update documentation to reference new repository
- [ ] Create migration notice in parent repository

## Success Validation Checklist

- [ ] New repository builds successfully in CI/CD
- [ ] All tests pass in automated pipeline
- [ ] SonarCloud integration reports correctly
- [ ] Branch protection enforces PR requirements
- [ ] NuGet packages publish automatically
- [ ] Documentation is complete and accessible
- [ ] Repository appears professional and complete

## Emergency Rollback Plan

- [ ] Keep backup of original Solver module until migration validated
- [ ] Document rollback steps if migration fails
- [ ] Maintain parent repository references until new repo confirmed stable
- [ ] Create rollback branch in parent repository if needed

---
**Total Estimated Tasks**: 47 tasks across 7 phases
**Estimated Completion Time**: 4-5 hours
**Risk Level**: Medium (primarily configuration and integration complexity)