# Domain Migration Todos

Epic assignment breakdown for migrating Domain, Audit, and EntityFramework modules to new GitHub repository with flattened folder structure.

## Phase 1: Repository Preparation & Setup

### Task 1.1: Clone and Initialize Repository

- [ ] Clone https://github.com/wangkanai/domain to ~/Sources/domain/
- [ ] Verify repository clone and remote configuration
- [ ] Create flattened folder structure (src/, tests/, benchmark/)
- [ ] Set up basic .gitignore and .gitattributes files

### Task 1.2: Configure Repository Settings

- [ ] Enable branch protection rules for main branch
- [ ] Require pull request reviews before merging
- [ ] Enable automatic deletion of head branches
- [ ] Configure merge options (squash and merge preferred)

### Task 1.3: Set up GitHub Secrets and Variables

- [ ] Verify SONAR_TOKEN secret is properly configured
- [ ] Add any additional secrets needed for CI/CD
- [ ] Configure repository variables if needed

## Phase 2: Code Migration & Structure

### Task 2.1: Copy Supporting Files

- [ ] Copy sign.ps1 to repository root
- [ ] Copy build.ps1 to repository root
- [ ] Copy md-styles.css to repository root
- [ ] Verify PowerShell scripts work in new location

### Task 2.2: Copy GitHub Configuration

- [ ] Copy .github/FUNDING.yml
- [ ] Copy .github/RELEASE.yml
- [ ] Create .github/workflows/ directory
- [ ] Prepare customized dotnet.yml workflow (don't copy yet)

### Task 2.3: Copy Assets

- [ ] Create Assets/ directory
- [ ] Copy Assets/wangkanai-logo.afdesign
- [ ] Copy Assets/wangkanai-logo.svg
- [ ] Copy Assets/wangkanai-logo.ico
- [ ] Copy Assets/wangkanai-logo.png
- [ ] Copy Assets/wangkanai-logo-large.png
- [ ] Verify all assets copied correctly

### Task 2.4: Copy and Restructure Module Folders

- [ ] Copy Domain/src/ content to src/Domain/
- [ ] Copy Domain/tests/ content to tests/Domain/
- [ ] Copy Domain/benchmark/ content to benchmark/Domain/
- [ ] Copy Audit/src/ content to src/Audit/
- [ ] Copy Audit/tests/ content to tests/Audit/
- [ ] Copy Audit/benchmark/ content to benchmark/Audit/
- [ ] Copy EntityFramework/src/ content to src/EntityFramework/
- [ ] Copy EntityFramework/tests/ content to tests/EntityFramework/
- [ ] Copy EntityFramework/benchmark/ content to benchmark/EntityFramework/
- [ ] Verify all source files, tests, and benchmarks copied correctly

### Task 2.5: Create MSBuild Configuration

- [ ] Create Directory.Build.props at repository root
- [ ] Create Directory.Build.targets at repository root
- [ ] Create Directory.Packages.props for centralized package management
- [ ] Configure version properties (start with 1.0.0)

## Phase 3: Project Configuration & Dependencies

### Task 3.1: Create Solution File

- [ ] Create Domain.slnx using new slnx format
- [ ] Add src/Domain/Wangkanai.Domain.csproj
- [ ] Add tests/Domain/Wangkanai.Domain.Tests.csproj
- [ ] Add benchmark/Domain/Wangkanai.Domain.Benchmark.csproj
- [ ] Add src/Audit/Wangkanai.Audit.csproj
- [ ] Add tests/Audit/Wangkanai.Audit.Tests.csproj
- [ ] Add benchmark/Audit/Wangkanai.Audit.Benchmark.csproj
- [ ] Add src/EntityFramework/Wangkanai.EntityFramework.csproj
- [ ] Add tests/EntityFramework/Wangkanai.EntityFramework.Tests.csproj
- [ ] Add benchmark/EntityFramework/Wangkanai.EntityFramework.Benchmark.csproj
- [ ] Organize projects in solution folders (src/, tests/, benchmark/)

### Task 3.2: Update Project References

- [ ] Update Audit project reference to Domain (../../src/Domain/Wangkanai.Domain.csproj)
- [ ] Update all test project references to use new relative paths
- [ ] Update all benchmark project references to use new relative paths
- [ ] Verify all internal project references use correct relative paths
- [ ] Remove references to external wangkanai modules if any

### Task 3.3: Update Package References

- [ ] Review all package references in migrated projects
- [ ] Update versions to match Directory.Packages.props
- [ ] Ensure no conflicts with existing packages
- [ ] Add any missing package references

### Task 3.4: Create Repository README

- [ ] Use Domain/README.md as base content
- [ ] Update content for repository scope (Domain + Audit + EntityFramework)
- [ ] Add installation instructions for all three packages
- [ ] Update badges and links for new repository
- [ ] Add contribution guidelines
- [ ] Document the three NuGet packages being produced

## Phase 4: CI/CD Pipeline & Quality Gates

### Task 4.1: Configure SonarQube Integration

- [ ] Update SonarQube.Analysis.xml for new repository
- [ ] Set organization: wangkanai
- [ ] Set project key: wangkanai_domain
- [ ] Configure token reference: {{secrets.SONAR_TOKEN}}
- [ ] Set version: 1.0.0
- [ ] Configure coverage and exclusions

### Task 4.2: Create GitHub Actions Workflow

- [ ] Create .github/workflows/dotnet.yml
- [ ] Configure build job for Domain, Audit, and EntityFramework projects
- [ ] Set up SonarQube Cloud integration
- [ ] Configure test execution with coverage for all modules
- [ ] Set up artifact publishing for all three packages
- [ ] Configure caching for better performance

### Task 4.3: Configure Quality Gates

- [ ] Enable SonarQube quality gate checks
- [ ] Configure coverage requirements
- [ ] Set up automated code quality checks
- [ ] Configure security scanning

## Phase 5: Local Testing & Validation

### Task 5.1: Local Build Validation

- [ ] Run `dotnet restore` on solution
- [ ] Run `dotnet build` on solution
- [ ] Verify no build errors or warnings
- [ ] Run `dotnet test` on all test projects
- [ ] Run benchmarks to ensure they work

### Task 5.2: PowerShell Scripts Testing

- [ ] Test build.ps1 script execution
- [ ] Test sign.ps1 script execution (if applicable)
- [ ] Verify scripts work with new project structure
- [ ] Update scripts if path adjustments needed

### Task 5.3: Solution File Testing

- [ ] Open Domain.slnx in IDE/editor
- [ ] Verify all projects load correctly
- [ ] Test build and rebuild from solution level
- [ ] Verify project references resolve correctly

## Phase 6: CI/CD Testing & Integration

### Task 6.1: Initial CI/CD Pipeline Test

- [ ] Commit and push initial setup to new repository
- [ ] Create initial pull request to test branch protection
- [ ] Verify GitHub Actions workflow triggers
- [ ] Check workflow runs without errors

### Task 6.2: SonarQube Integration Test

- [ ] Verify SonarQube analysis runs in CI
- [ ] Check code quality metrics are generated
- [ ] Ensure coverage reports are generated
- [ ] Validate quality gate status

### Task 6.3: End-to-End Pipeline Test

- [ ] Test complete CI/CD pipeline flow
- [ ] Verify all quality gates pass
- [ ] Test merge process with branch protection
- [ ] Validate artifact generation

## Phase 7: Package Generation & Publishing

### Task 7.1: NuGet Package Configuration

- [ ] Configure package metadata in project files
- [ ] Set package authors, description, and tags
- [ ] Configure package icon and readme
- [ ] Set license and repository URLs

### Task 7.2: Package Build Testing

- [ ] Test `dotnet pack` for Domain project
- [ ] Test `dotnet pack` for Audit project
- [ ] Test `dotnet pack` for EntityFramework project
- [ ] Verify package contents and metadata for all packages
- [ ] Test package installation locally

### Task 7.3: Publishing Preparation

- [ ] Configure package publishing in CI/CD
- [ ] Set up version automation
- [ ] Configure package push permissions
- [ ] Test publishing to NuGet (dry run)

## Phase 8: Main Repository Updates

### Task 8.1: Remove Migrated Modules

- [ ] Remove Domain/ folder from main repository
- [ ] Remove Audit/ folder from main repository
- [ ] Remove EntityFramework/ folder from main repository
- [ ] Update Wangkanai.slnx to remove migrated projects
- [ ] Remove solution folder references

### Task 8.2: Add NuGet Package References

- [ ] Identify any projects that need Domain/Audit/EntityFramework packages
- [ ] Add NuGet package references where needed
- [ ] Update project references to package references
- [ ] Test build after package reference updates

### Task 8.3: Update Build Scripts

- [ ] Update build.ps1 in main repository
- [ ] Update sign.ps1 in main repository
- [ ] Remove Domain/Audit/EntityFramework from build processes
- [ ] Test updated scripts

### Task 8.4: Update Documentation

- [ ] Update main repository README
- [ ] Update project documentation references
- [ ] Add migration notes and changelog
- [ ] Update architecture documentation

## Phase 9: Final Validation & Cleanup

### Task 9.1: Cross-Repository Testing

- [ ] Verify main repository builds without Domain/Audit
- [ ] Test NuGet package consumption in main repository
- [ ] Verify all tests pass in both repositories
- [ ] Check for any missed dependencies

### Task 9.2: Documentation Updates

- [ ] Finalize README in domain repository
- [ ] Update installation and usage instructions
- [ ] Add contribution guidelines
- [ ] Create migration documentation

### Task 9.3: Release Preparation

- [ ] Create initial release notes
- [ ] Tag first version (1.0.0)
- [ ] Publish NuGet packages
- [ ] Announce migration completion

### Task 9.4: Final Cleanup

- [ ] Remove any temporary files or scripts
- [ ] Clean up branches and old references
- [ ] Archive or update related issues/PRs
- [ ] Update team documentation and workflows

## Completion Checklist

- [ ] New domain repository builds successfully
- [ ] All tests pass in domain repository
- [ ] CI/CD pipeline works correctly
- [ ] SonarQube integration functional
- [ ] NuGet packages can be generated
- [ ] Main repository updated and builds successfully
- [ ] Documentation is complete and accurate
- [ ] Team has been notified of changes

## Risk Mitigation

### Before Each Phase

- [ ] Create backup/checkpoint of current state
- [ ] Review phase tasks and dependencies
- [ ] Ensure prerequisites are met

### After Each Phase

- [ ] Validate phase completion criteria
- [ ] Test critical functionality
- [ ] Update progress and communicate status
- [ ] Commit changes and create checkpoint