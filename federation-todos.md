# Federation Migration Epic - Workflow Todos

## Overview

Epic: Migrate Federation, Identity, and Security modules from `wangkanai/wangkanai` to `wangkanai/federation`

Repository: https://github.com/wangkanai/federation
Target Directory: ~/Sources/federation/

---

## Phase 1: Preparation (Current Repository)

**Location**: ~/Sources/wangkanai/

### Task 1.1: Create Feature Branch

- [ ] Switch to main branch: `git checkout main`
- [ ] Pull latest changes: `git pull`
- [ ] Create feature branch: `git checkout -b prepare-federation-split`

### Task 1.2: Verify NuGet Dependencies

- [ ] Check if `Wangkanai.Cryptography` is published to NuGet
- [ ] Check if `Wangkanai.System` is published to NuGet
- [ ] Check if `Wangkanai.Webserver` is published to NuGet
- [ ] Check if `Wangkanai.Domain` is published to NuGet
- [ ] Document version numbers for each dependency

### Task 1.3: Update Project References

- [ ] Update `Federation/src/Federation/Wangkanai.Federation.csproj`
   - [ ] Replace ProjectReference for Cryptography with PackageReference
   - [ ] Replace ProjectReference for System with PackageReference
   - [ ] Replace ProjectReference for Webserver with PackageReference
- [ ] Update `Federation/src/Domain/Wangkanai.Federation.Domain.csproj`
   - [ ] Check and update any internal references
- [ ] Update `Federation/src/EntityFramework/Wangkanai.Federation.EntityFramework.csproj`
   - [ ] Check and update any internal references
- [ ] Update `Federation/src/AspNetIdentity/Wangkanai.Federation.AspNetIdentity.csproj`
   - [ ] Check and update any internal references
- [ ] Update `Federation/src/AuthProxyBackend/Wangkanai.Federation.AuthProxyBackend.csproj`
   - [ ] Check and update any internal references
- [ ] Update `Identity/src/Wangkanai.Identity.csproj`
   - [ ] Replace ProjectReference for Domain with PackageReference
- [ ] Update `Security/src/Core/Wangkanai.Security.Core.csproj`
   - [ ] Replace ProjectReference for System with PackageReference
- [ ] Update `Security/src/Authentication/Wangkanai.Security.Authentication.csproj`
   - [ ] Check and update any internal references
- [ ] Update `Security/src/Authorization/Wangkanai.Security.Authorization.csproj`
   - [ ] Check and update any internal references

### Task 1.4: Build Verification

- [ ] Build Federation module: `dotnet build Federation/src/Federation/Wangkanai.Federation.csproj`
- [ ] Build Identity module: `dotnet build Identity/src/Wangkanai.Identity.csproj`
- [ ] Build Security module: `dotnet build Security/src/Core/Wangkanai.Security.Core.csproj`
- [ ] Run Federation tests: `dotnet test Federation/tests/`
- [ ] Run Identity tests: `dotnet test Identity/tests/`
- [ ] Run Security tests: `dotnet test Security/tests/`

### Task 1.5: Commit Preparation Changes

- [ ] Stage all changes: `git add .`
- [ ] Commit changes: `git commit -m "feat: Prepare Federation, Identity, Security for migration"`
- [ ] Push branch: `git push -u origin prepare-federation-split`

---

## Phase 2: New Repository Setup

**Location**: ~/Sources/

### Task 2.1: Clone and Initialize Repository

- [ ] Navigate to Sources directory: `cd ~/Sources`
- [ ] Clone repository: `git clone https://github.com/wangkanai/federation.git`
- [ ] Enter directory: `cd federation`
- [ ] Create initial branch: `git checkout -b initial-setup`

### Task 2.2: Configure GitHub Repository Settings

#### Branch Protection Rules
- [ ] Go to Settings → Branches in GitHub repository
- [ ] Add rule for `main` branch
- [ ] Enable "Require a pull request before merging"
  - [ ] Set required approving reviews to 1
  - [ ] Enable "Dismiss stale pull request approvals when new commits are pushed"
- [ ] Enable "Require status checks to pass before merging"
  - [ ] Add `build-dotnet` as required status check
  - [ ] Enable "Require branches to be up to date before merging"
- [ ] Enable "Require conversation resolution before merging"
- [ ] Disable "Allow force pushes"
- [ ] Disable "Allow deletions"
- [ ] Save protection rules

#### Repository Settings
- [ ] Set default branch to `main`
- [ ] Enable "Automatically delete head branches"
- [ ] Configure merge options:
  - [ ] Allow merge commits
  - [ ] Allow squash merging
  - [ ] Disable rebase merging

### Task 2.3: Create Directory Structure

- [ ] Create `.github/workflows` directory
- [ ] Create `Federation` directory structure:
   - [ ] `Federation/src/`
   - [ ] `Federation/tests/`
   - [ ] `Federation/benchmark/`
- [ ] Create `Identity` directory structure:
   - [ ] `Identity/src/`
   - [ ] `Identity/tests/`
   - [ ] `Identity/benchmark/`
- [ ] Create `Security` directory structure:
   - [ ] `Security/src/`
   - [ ] `Security/tests/`
   - [ ] `Security/benchmark/`

### Task 2.4: Copy Configuration Files

- [ ] Copy `Directory.Build.props` from main repo
- [ ] Update repository URLs in `Directory.Build.props` to `wangkanai/federation`
- [ ] Copy `Directory.Build.targets` from main repo
- [ ] Copy `Directory.Packages.props` from main repo
- [ ] Remove unnecessary package references from `Directory.Packages.props`
- [ ] Copy `.editorconfig` from main repo
- [ ] Copy `.gitignore` from main repo
- [ ] Copy `.gitattributes` from main repo
- [ ] Copy `LICENSE` from main repo

### Task 2.5: Setup GitHub Actions Workflow

- [ ] Copy `.github/workflows/dotnet.yml` from main repo
- [ ] Adapt workflow for federation repository:
  - [ ] Remove npm build job (not needed for these modules)
  - [ ] Update SonarCloud configuration:
    - [ ] Organization: `wangkanai`
    - [ ] Project Key: `wangkanai_federation`
    - [ ] Version: `1.0.0`
  - [ ] Add SonarScanner installation step
  - [ ] Configure SonarCloud Begin/End analysis steps
  - [ ] Simplify test discovery for focused modules
  - [ ] Remove references to Tabler/Blazor assets
- [ ] Create `SonarQube.Analysis.xml` configuration file with:
  ```xml
  <Property Name="sonar.organization">wangkanai</Property>
  <Property Name="sonar.projectKey">wangkanai_federation</Property>
  <Property Name="sonar.projectVersion">1.0.0</Property>
  ```
- [ ] Add required GitHub Secrets:
  - [ ] `SONAR_TOKEN` for SonarCloud integration
  - [ ] `NUGET_API_KEY` for package publishing (later)

---

## Phase 3: Module Migration

**Location**: ~/Sources/federation/

### Task 3.1: Copy Federation Module

- [ ] Copy Federation source files:
  ```bash
  cp -r ../wangkanai/Federation/src/* Federation/src/
  ```
- [ ] Copy Federation test files:
  ```bash
  cp -r ../wangkanai/Federation/tests/* Federation/tests/
  ```
- [ ] Copy Federation benchmark files:
  ```bash
  cp -r ../wangkanai/Federation/benchmark/* Federation/benchmark/
  ```
- [ ] Verify all Federation files copied correctly

### Task 3.2: Copy Identity Module

- [ ] Copy Identity source files:
  ```bash
  cp -r ../wangkanai/Identity/src/* Identity/src/
  ```
- [ ] Copy Identity test files:
  ```bash
  cp -r ../wangkanai/Identity/tests/* Identity/tests/
  ```
- [ ] Copy Identity benchmark files:
  ```bash
  cp -r ../wangkanai/Identity/benchmark/* Identity/benchmark/
  ```
- [ ] Verify all Identity files copied correctly

### Task 3.3: Copy Security Module

- [ ] Copy Security source files:
  ```bash
  cp -r ../wangkanai/Security/src/* Security/src/
  ```
- [ ] Copy Security test files:
  ```bash
  cp -r ../wangkanai/Security/tests/* Security/tests/
  ```
- [ ] Copy Security benchmark files:
  ```bash
  cp -r ../wangkanai/Security/benchmark/* Security/benchmark/
  ```
- [ ] Verify all Security files copied correctly

### Task 3.4: Update Project Files

- [ ] Update all Federation project files to use PackageReference
- [ ] Update all Identity project files to use PackageReference
- [ ] Update all Security project files to use PackageReference
- [ ] Fix all relative paths in project files
- [ ] Update namespace references if needed

### Task 3.5: Create Solution File

- [ ] Create `Federation.slnx` solution file
- [ ] Add all Federation projects to solution
- [ ] Add all Identity projects to solution
- [ ] Add all Security projects to solution
- [ ] Organize projects in solution folders
- [ ] Verify solution structure

---

## Phase 4: Documentation

**Location**: ~/Sources/federation/

### Task 4.1: Create README.md

- [ ] Copy base content from `Federation/README.md`
- [ ] Add Identity features section from `Identity/README.md`
- [ ] Add Security features section from `Security/README.md`
- [ ] Update badges (NuGet, Build, Quality)
- [ ] Add installation instructions for all packages
- [ ] Add getting started guide
- [ ] Add contribution guidelines
- [ ] Add license section

### Task 4.2: Create Package Documentation

- [ ] Create `docs/` directory
- [ ] Create documentation for Federation package
- [ ] Create documentation for Identity package
- [ ] Create documentation for Security package
- [ ] Add API reference documentation
- [ ] Add migration guide from old packages

---

## Phase 5: CI/CD Setup

**Location**: ~/Sources/federation/

### Task 5.1: GitHub Actions Workflow (CI)

- [ ] Verify `.github/workflows/dotnet.yml` exists (copied in Phase 2)
- [ ] Test workflow locally using `act` (optional)
- [ ] Push to feature branch to trigger workflow
- [ ] Verify workflow runs successfully
- [ ] Check build status in GitHub Actions tab
- [ ] Verify test results and coverage reports
- [ ] Add workflow status badge to README.md:
  ```markdown
  [![.NET](https://github.com/wangkanai/federation/actions/workflows/dotnet.yml/badge.svg)](https://github.com/wangkanai/federation/actions/workflows/dotnet.yml)
  ```

### Task 5.2: NuGet Publishing Workflow

- [ ] Create `.github/workflows/publish.yml`:
  ```yaml
  name: Publish NuGet
  on:
    release:
      types: [published]
    workflow_dispatch:
      inputs:
        version:
          description: 'Package version'
          required: true
  ```
- [ ] Add NuGet API key to GitHub Secrets
- [ ] Configure package versioning in `.csproj` files
- [ ] Test pack command locally: `dotnet pack -c Release`
- [ ] Create test release (as draft) to verify workflow

### Task 5.3: SonarCloud Integration

- [ ] Register project in SonarCloud: https://sonarcloud.io/projects/create
  - [ ] Organization: `wangkanai`
  - [ ] Project Key: `wangkanai_federation`
  - [ ] Display Name: `Federation`
- [ ] Configure Quality Gate settings:
  - [ ] Use "Sonar way" quality gate or customize:
    - [ ] Coverage: >= 80%
    - [ ] Duplicated Lines: < 3%
    - [ ] Maintainability Rating: A
    - [ ] Reliability Rating: A
    - [ ] Security Rating: A
- [ ] Generate authentication token:
  - [ ] Go to: https://sonarcloud.io/account/security
  - [ ] Generate new token named: `federation-github-actions`
  - [ ] Copy token value
- [ ] Add token to GitHub repository:
  - [ ] Go to: https://github.com/wangkanai/federation/settings/secrets/actions
  - [ ] Add new secret: `SONAR_TOKEN` with token value
- [ ] Verify SonarQube.Analysis.xml has correct settings:
  ```xml
  <Property Name="sonar.organization">wangkanai</Property>
  <Property Name="sonar.projectKey">wangkanai_federation</Property>
  <Property Name="sonar.projectVersion">1.0.0</Property>
  ```
- [ ] Update README.md badges:
  ```markdown
  [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=wangkanai_federation&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=wangkanai_federation)
  [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=wangkanai_federation&metric=coverage)](https://sonarcloud.io/summary/new_code?id=wangkanai_federation)
  [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=wangkanai_federation&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=wangkanai_federation)
  ```

### Task 5.4: Branch Protection Verification

- [ ] Create test PR to verify protection rules work
- [ ] Confirm CI runs automatically on PR
- [ ] Verify merge is blocked until checks pass
- [ ] Test that direct push to main is blocked
- [ ] Confirm PR review requirement works

---

## Phase 6: Validation

**Location**: ~/Sources/federation/

### Task 6.1: Build Validation

- [ ] Run `dotnet restore`
- [ ] Run `dotnet build -c Release`
- [ ] Verify no build errors
- [ ] Verify all projects build successfully

### Task 6.2: Test Validation

- [ ] Run `dotnet test -c Release`
- [ ] Verify all tests pass
- [ ] Check code coverage meets requirements
- [ ] Run benchmark projects

### Task 6.3: Package Validation

- [ ] Create local NuGet packages: `dotnet pack -c Release`
- [ ] Set up local NuGet feed
- [ ] Test package installation in sample project
- [ ] Verify package dependencies resolve
- [ ] Test basic functionality with packages

### Task 6.4: Initial Commit

- [ ] Stage all files: `git add .`
- [ ] Create initial commit: `git commit -m "feat: Initial migration of Federation, Identity, and Security modules"`
- [ ] Push to GitHub: `git push -u origin initial-setup`
- [ ] Create pull request to main branch

---

## Phase 7: Main Repository Cleanup

**Location**: ~/Sources/wangkanai/

### Task 7.1: Remove Migrated Modules

- [ ] Switch to cleanup branch: `git checkout -b remove-migrated-modules`
- [ ] Remove Federation directory: `git rm -r Federation/`
- [ ] Remove Identity directory: `git rm -r Identity/`
- [ ] Remove Security directory: `git rm -r Security/`
- [ ] Update `.slnx` file to remove references

### Task 7.2: Update Dependencies

- [ ] Find all projects referencing Federation
- [ ] Update to use NuGet package instead
- [ ] Find all projects referencing Identity
- [ ] Update to use NuGet package instead
- [ ] Find all projects referencing Security
- [ ] Update to use NuGet package instead

### Task 7.3: Update Documentation

- [ ] Update main README.md with migration notice
- [ ] Add link to new federation repository
- [ ] Update any documentation referencing old modules
- [ ] Create MIGRATION.md guide

### Task 7.4: Verification

- [ ] Build entire solution: `dotnet build`
- [ ] Run all tests: `dotnet test`
- [ ] Verify no broken references
- [ ] Commit changes

---

## Phase 8: Release

**Location**: ~/Sources/federation/

### Task 8.1: Prepare Release

- [ ] Merge initial-setup branch to main
- [ ] Create release branch: `git checkout -b release/1.0.0`
- [ ] Update version numbers in all projects
- [ ] Update CHANGELOG.md
- [ ] Create release notes

### Task 8.2: Publish NuGet Packages

- [ ] Build release packages: `dotnet pack -c Release`
- [ ] Publish Wangkanai.Federation to NuGet
- [ ] Publish Wangkanai.Federation.Domain to NuGet
- [ ] Publish Wangkanai.Federation.EntityFramework to NuGet
- [ ] Publish Wangkanai.Federation.AspNetIdentity to NuGet
- [ ] Publish Wangkanai.Identity to NuGet
- [ ] Publish Wangkanai.Security.Core to NuGet
- [ ] Publish Wangkanai.Security.Authentication to NuGet
- [ ] Publish Wangkanai.Security.Authorization to NuGet
- [ ] Verify packages on NuGet.org

### Task 8.3: GitHub Release

- [ ] Create tag: `git tag v1.0.0`
- [ ] Push tag: `git push origin v1.0.0`
- [ ] Create GitHub release
- [ ] Attach release notes
- [ ] Attach compiled binaries
- [ ] Publish release

### Task 8.4: Announcement

- [ ] Update main repository with completion notice
- [ ] Create announcement in GitHub Discussions
- [ ] Update any external documentation
- [ ] Notify users of migration

---

## Post-Migration Tasks

### Monitoring

- [ ] Monitor GitHub issues for migration problems
- [ ] Check NuGet download statistics
- [ ] Review build pipeline performance
- [ ] Collect user feedback

### Documentation Updates

- [ ] Create detailed API documentation
- [ ] Add code examples
- [ ] Create video tutorials (optional)
- [ ] Update Stack Overflow tags

### Maintenance Setup

- [ ] Set up dependency update automation
- [ ] Configure security scanning
- [ ] Set up code quality checks
- [ ] Create maintenance schedule

---

## Success Criteria Checklist

- [ ] ✅ New repository at https://github.com/wangkanai/federation is functional
- [ ] ✅ All projects build successfully in new repository
- [ ] ✅ All tests pass in new repository
- [ ] ✅ NuGet packages published and installable
- [ ] ✅ CI/CD pipelines functional
- [ ] ✅ Documentation complete and accurate
- [ ] ✅ Main repository builds without Federation/Identity/Security modules
- [ ] ✅ No breaking changes for existing consumers
- [ ] ✅ GitHub release created with proper tags
- [ ] ✅ Migration announced to users

---

## Notes

- **Repository**: https://github.com/wangkanai/federation
- **Local Path**: ~/Sources/federation/
- **Dependencies to Publish First**: Cryptography, System, Webserver, Domain
- **Target Framework**: .NET 8.0
- **License**: Apache 2.0

## Progress Tracking

- **Started**: [Date]
- **Phase 1 Complete**: [ ]
- **Phase 2 Complete**: [ ]
- **Phase 3 Complete**: [ ]
- **Phase 4 Complete**: [ ]
- **Phase 5 Complete**: [ ]
- **Phase 6 Complete**: [ ]
- **Phase 7 Complete**: [ ]
- **Phase 8 Complete**: [ ]
- **Completed**: [Date]

---

*Last Updated: [Current Date]*