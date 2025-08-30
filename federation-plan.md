# Federation Repository Migration Plan

## Overview

This document outlines the comprehensive workflow for splitting the Federation, Identity, and Security modules from the main `wangkanai/wangkanai` monorepo into a new dedicated repository at
`https://github.com/wangkanai/federation`.

## Current State Analysis

### Modules to Migrate

1. **Federation Module** (`/Federation/`)
   - Core: `Wangkanai.Federation.csproj`
   - Domain: `Wangkanai.Federation.Domain.csproj`
   - EntityFramework: `Wangkanai.Federation.EntityFramework.csproj`
   - AspNetIdentity: `Wangkanai.Federation.AspNetIdentity.csproj`
   - AuthProxyBackend: `Wangkanai.Federation.AuthProxyBackend.csproj`
   - Tests: Federation.Tests, Federation.Domain.Tests
   - Benchmark: Federation.Benchmark

2. **Identity Module** (`/Identity/`)
   - Core: `Wangkanai.Identity.csproj`
   - Tests: Identity.Tests
   - Benchmark: Identity.Benchmark

3. **Security Module** (`/Security/`)
   - Core: `Wangkanai.Security.Core.csproj`
   - Authentication: `Wangkanai.Security.Authentication.csproj`
   - Authorization: `Wangkanai.Security.Authorization.csproj`
   - Tests: Security.Tests
   - Benchmark: Security.Benchmark

### Internal Dependencies (Current Repo)

These dependencies will need to be replaced with NuGet packages:

- `Wangkanai.Cryptography`
- `Wangkanai.System`
- `Wangkanai.Webserver`
- `Wangkanai.Domain`

### External Dependencies

- Microsoft.AspNetCore.Authentication.OpenIdConnect
- Microsoft.IdentityModel.Tokens
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Other Microsoft.Extensions packages

## Migration Workflow

### Phase 1: Preparation (Current Repository)

#### 1.1 Create Feature Branch

```bash
git checkout -b prepare-federation-split
```

#### 1.2 Update Project References

- Convert all internal project references to PackageReference
- Update version numbers to match published NuGet packages
- Files to modify:
   - `Federation/src/Federation/Wangkanai.Federation.csproj`
   - `Federation/src/Domain/Wangkanai.Federation.Domain.csproj`
   - `Federation/src/EntityFramework/Wangkanai.Federation.EntityFramework.csproj`
   - `Federation/src/AspNetIdentity/Wangkanai.Federation.AspNetIdentity.csproj`
   - `Federation/src/AuthProxyBackend/Wangkanai.Federation.AuthProxyBackend.csproj`
   - `Identity/src/Wangkanai.Identity.csproj`
   - `Security/src/Core/Wangkanai.Security.Core.csproj`
   - `Security/src/Authentication/Wangkanai.Security.Authentication.csproj`
   - `Security/src/Authorization/Wangkanai.Security.Authorization.csproj`

#### 1.3 Verify Build

```bash
dotnet build Federation/src/Federation/Wangkanai.Federation.csproj
dotnet build Identity/src/Wangkanai.Identity.csproj
dotnet build Security/src/Core/Wangkanai.Security.Core.csproj
```

### Phase 2: New Repository Setup

#### 2.1 Clone Existing Repository

```bash
# Repository already created at: https://github.com/wangkanai/federation
# Clone the repository to ~/Sources/federation/
cd ~/Sources
git clone https://github.com/wangkanai/federation.git
cd federation
```

#### 2.2 Configure GitHub Repository Settings

##### Branch Protection Rules

Configure main branch protection in GitHub Settings:

- **Require pull request reviews before merging**: Yes
- **Required approving reviews**: 1
- **Dismiss stale pull request approvals**: Yes
- **Require status checks to pass**: Yes
   - Required checks: build-dotnet, build-npm (if applicable)
- **Require branches to be up to date**: Yes
- **Require conversation resolution**: Yes
- **Require signed commits**: Optional
- **Include administrators**: No (for emergency fixes)
- **Allow force pushes**: No
- **Allow deletions**: No

##### Repository Settings

- **Default branch**: main
- **Allow merge commits**: Yes
- **Allow squash merging**: Yes
- **Allow rebase merging**: No
- **Automatically delete head branches**: Yes

#### 2.3 Initialize Repository Structure

```
federation/
├── .github/
│   └── workflows/
│       └── dotnet.yml
├── Federation/
│   ├── src/
│   │   ├── Core/
│   │   ├── Domain/
│   │   ├── EntityFramework/
│   │   ├── AspNetIdentity/
│   │   └── AuthProxyBackend/
│   ├── tests/
│   │   ├── Federation.Tests/
│   │   └── Domain.Tests/
│   └── benchmark/
│       └── Federation.Benchmark/
├── Identity/
│   ├── src/
│   │   └── Identity/
│   ├── tests/
│   │   └── Identity.Tests/
│   └── benchmark/
│       └── Identity.Benchmark/
├── Security/
│   ├── src/
│   │   ├── Core/
│   │   ├── Authentication/
│   │   └── Authorization/
│   ├── tests/
│   │   └── Security.Tests/
│   └── benchmark/
│       └── Security.Benchmark/
├── Directory.Build.props
├── Directory.Build.targets
├── Directory.Packages.props
├── .editorconfig
├── .gitignore
├── .gitattributes
├── LICENSE
├── README.md
└── Federation.slnx
```

#### 2.4 Copy Common Configuration Files

From the main repository, copy:

- `Directory.Build.props` (update repository URLs)
- `Directory.Build.targets`
- `Directory.Packages.props` (keep only relevant packages)
- `.editorconfig`
- `.gitignore`
- `.gitattributes`
- `LICENSE`
- `.github/workflows/dotnet.yml` (adapt for new repository)

### Phase 3: Module Migration

#### 3.1 Copy Module Files

```bash
# From main repository to new repository
cp -r ../wangkanai/Federation/src/* federation/Federation/src/
cp -r ../wangkanai/Federation/tests/* federation/Federation/tests/
cp -r ../wangkanai/Federation/benchmark/* federation/Federation/benchmark/

cp -r ../wangkanai/Identity/src/* federation/Identity/src/
cp -r ../wangkanai/Identity/tests/* federation/Identity/tests/
cp -r ../wangkanai/Identity/benchmark/* federation/Identity/benchmark/

cp -r ../wangkanai/Security/src/* federation/Security/src/
cp -r ../wangkanai/Security/tests/* federation/Security/tests/
cp -r ../wangkanai/Security/benchmark/* federation/Security/benchmark/
```

#### 3.2 Update Project Files

Update all `.csproj` files to:

- Replace ProjectReference with PackageReference for external dependencies
- Update paths to match new repository structure
- Ensure correct package versions

Example transformation:

```xml
<!-- Before -->
<ProjectReference Include="$(RepoRoot)\Cryptography\src\Wangkanai.Cryptography.csproj" />

<!-- After -->
<PackageReference Include="Wangkanai.Cryptography" Version="8.0.0" />
```

#### 3.3 Create Solution File

Create `Federation.slnx` with all projects organized by:

- Top-level folders: Federation, Identity, Security
- Each module containing its own src, tests, benchmark subfolders
- Project references properly configured

### Phase 4: Documentation

#### 4.1 Create Unified README.md

Combine content from:

- Federation/README.md (primary content)
- Identity/README.md (features section)
- Security/README.md (features section)

Structure:

```markdown
# Federation: The Ultimate Authentication and Authorization Server for ASP.NET Core

[Badges for NuGet, Build, Quality, etc.]

## Overview
[Combined overview from all three modules]

## Features

### Federation Features
[From Federation README]

### Identity Features
[From Identity README]

### Security Features
[From Security README]

## Installation

### NuGet Packages
- `Wangkanai.Federation`
- `Wangkanai.Federation.Domain`
- `Wangkanai.Federation.EntityFramework`
- `Wangkanai.Identity`
- `Wangkanai.Security.Core`
- `Wangkanai.Security.Authentication`
- `Wangkanai.Security.Authorization`

## Getting Started
[Usage examples and configuration]

## Documentation
[Links to detailed documentation]

## Contributing
[Contribution guidelines]

## License
[Apache 2.0 License]
```

### Phase 5: CI/CD Setup

#### 5.1 GitHub Actions Workflow with SonarCloud Integration

Create `.github/workflows/dotnet.yml` (adapted from main repository):

```yaml
name: dotnet

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

env:
  VERSION: 1.0.0
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: true
  DOTNET_NOLOGO: true
  DOTNET_CLI_TELEMETRY_OPTOUT: true
  DOTNET_GENERATE_ASPNET_CERTIFICATE: false
  DOTNET_SYSTEM_CONSOLE_ALLOW_ANSI_COLOR_REDIRECTION: true
  DOTNET_MULTILEVEL_LOOKUP: false

jobs:
  build-dotnet:
    runs-on: ubuntu-latest

    env:
      NUGET_PACKAGES: ${{ github.workspace }}/.nuget/packages

    steps:
    - uses: actions/checkout@v4
      with:
        fetch-depth: 0  # Required for SonarCloud analysis

    - name: Setup .NET 9.0
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 9.0.x

    - name: Cache NuGet packages
      uses: actions/cache@v4
      with:
        path: ${{ env.NUGET_PACKAGES }}
        key: ${{ runner.os }}-nuget-${{ hashFiles('**/Directory.Packages.props', '**/*.csproj') }}
        restore-keys: |
          ${{ runner.os }}-nuget-

    - name: Install SonarScanner
      run: |
        dotnet tool install --global dotnet-sonarscanner --version 5.15.0
        echo "$HOME/.dotnet/tools" >> $GITHUB_PATH

    - name: SonarCloud Begin Analysis
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
      run: |
        dotnet sonarscanner begin \
          /k:"wangkanai_federation" \
          /o:"wangkanai" \
          /v:"${{ env.VERSION }}" \
          /d:sonar.token="${{ secrets.SONAR_TOKEN }}" \
          /d:sonar.host.url="https://sonarcloud.io" \
          /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml" \
          /d:sonar.coverage.exclusions="**/Mocks/**,**/Tests/**,**/benchmark/**" \
          /d:sonar.exclusions="**/obj/**,**/bin/**,**/*.Designer.cs"

    - name: Restore dependencies
      run: dotnet restore --verbosity minimal

    - name: Build
      run: |
        dotnet build --no-restore -c Release \
          -p:TreatWarningsAsErrors=false \
          -p:ContinuousIntegrationBuild=true \
          -p:Deterministic=true \
          --verbosity minimal

    - name: Test with Coverage
      run: |
        dotnet test --no-build -c Release \
          --collect:"XPlat Code Coverage" \
          --logger "console;verbosity=normal" \
          --results-directory ./coverage \
          -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

    - name: SonarCloud End Analysis
      env:
        GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
      run: |
        dotnet sonarscanner end /d:sonar.token="${{ secrets.SONAR_TOKEN }}"

    - name: Upload Coverage Report
      uses: actions/upload-artifact@v4
      if: always()
      with:
        name: coverage-report
        path: coverage/
        retention-days: 7
```

#### 5.2 SonarCloud Configuration

Create `SonarQube.Analysis.xml` in repository root:

```xml
<?xml version="1.0" encoding="utf-8"?>
<SonarQubeAnalysisProperties xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                             xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                             xmlns="http://www.sonarsource.com/msbuild/integration/2015/1">
  <Property Name="sonar.organization">wangkanai</Property>
  <Property Name="sonar.projectKey">wangkanai_federation</Property>
  <Property Name="sonar.projectName">Federation</Property>
  <Property Name="sonar.projectVersion">1.0.0</Property>
  <Property Name="sonar.sources">.</Property>
  <Property Name="sonar.exclusions">
    **/bin/**,
    **/obj/**,
    **/node_modules/**,
    **/*.Designer.cs,
    **/*.generated.cs,
    **/wwwroot/lib/**
  </Property>
  <Property Name="sonar.coverage.exclusions">
    **/Tests/**,
    **/benchmark/**,
    **/Migrations/**,
    **/Program.cs,
    **/Startup.cs
  </Property>
  <Property Name="sonar.cs.opencover.reportsPaths">**/coverage.opencover.xml</Property>
  <Property Name="sonar.cs.vstest.reportsPaths">**/*.trx</Property>
</SonarQubeAnalysisProperties>
```

#### 5.3 NuGet Publishing Workflow

Create `.github/workflows/publish.yml`:

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

env:
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: true
  DOTNET_NOLOGO: true
  DOTNET_CLI_TELEMETRY_OPTOUT: true

jobs:
  publish:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 9.0.x

    - name: Restore dependencies
      run: dotnet restore

    - name: Build
      run: dotnet build -c Release --no-restore

    - name: Pack
      run: |
        VERSION=${{ github.event.inputs.version || github.event.release.tag_name }}
        dotnet pack -c Release --no-build -o ./artifacts \
          -p:PackageVersion=${VERSION#v} \
          -p:IncludeSymbols=true \
          -p:SymbolPackageFormat=snupkg

    - name: Push to NuGet
      run: |
        dotnet nuget push "./artifacts/*.nupkg" \
          --api-key ${{ secrets.NUGET_API_KEY }} \
          --source https://api.nuget.org/v3/index.json \
          --skip-duplicate
```

#### 5.4 Branch Protection Setup

Configure via GitHub UI or API:

- Protect main branch with required status checks
- Require PR reviews before merging
- Automatically run CI on all PRs
- Required status check: `SonarCloud Code Analysis`

#### 5.5 SonarCloud Project Setup

1. **Register Project in SonarCloud**:
   - Go to: https://sonarcloud.io/projects/create
   - Organization: `wangkanai`
   - Project Key: `wangkanai_federation`
   - Display Name: `Federation`

2. **Configure Quality Gate**:
   - Use default "Sonar way" quality gate
   - Or create custom gate with:
      - Coverage: >= 80%
      - Duplicated Lines: < 3%
      - Maintainability Rating: A
      - Reliability Rating: A
      - Security Rating: A

3. **Generate Token**:
   - Go to: https://sonarcloud.io/account/security
   - Generate new token for `wangkanai_federation`
   - Add to GitHub Secrets as `SONAR_TOKEN`

### Phase 6: Validation

#### 6.1 Build Verification

```bash
# In new repository
dotnet restore
dotnet build -c Release
dotnet test -c Release
```

#### 6.2 Package Testing

- Create local NuGet feed
- Test package installation in sample project
- Verify all dependencies resolve correctly

### Phase 7: Main Repository Cleanup

#### 7.1 Remove Migrated Modules

```bash
# In main repository
git rm -r Federation/
git rm -r Identity/
git rm -r Security/
```

#### 7.2 Update Solution File

Remove references to migrated projects from `Wangkanai.slnx`

#### 7.3 Update Dependencies

For any remaining projects that depend on Federation, Identity, or Security:

- Replace ProjectReference with PackageReference
- Update to use published NuGet packages

#### 7.4 Update Documentation

- Update main repository README
- Add migration notice with link to new repository
- Update any documentation that references these modules

### Phase 8: Release

#### 8.1 Publish NuGet Packages

```bash
# For each project
dotnet pack -c Release
dotnet nuget push *.nupkg -k [API_KEY] -s https://api.nuget.org/v3/index.json
```

#### 8.2 Create GitHub Release

- Tag version (e.g., v1.0.0)
- Create release notes
- Attach build artifacts

#### 8.3 Announce Migration

- Update main repository with migration notice
- Create announcement in discussions/issues
- Update any external documentation

## Post-Migration Tasks

### Maintenance Considerations

1. **Version Synchronization**: Keep track of version dependencies between main repo packages and federation repo
2. **Breaking Changes**: Coordinate major version updates between repositories
3. **Security Updates**: Establish process for security patches across repositories
4. **Documentation**: Maintain cross-references between repositories

### Rollback Plan

If issues arise:

1. Keep original modules in main repo (branch: `federation-backup`)
2. Maintain package feed with previous versions
3. Document rollback procedure for consumers

## Timeline Estimate

- **Phase 1-2**: 2-3 hours (Preparation and Setup)
- **Phase 3-4**: 3-4 hours (Migration and Documentation)
- **Phase 5-6**: 2-3 hours (CI/CD and Validation)
- **Phase 7-8**: 2-3 hours (Cleanup and Release)

**Total Estimated Time**: 1-2 days of work

## Risk Assessment

### High Priority Risks

1. **Breaking Changes**: Package reference updates may break consuming projects
   - Mitigation: Thorough testing, semantic versioning
2. **Missing Dependencies**: Overlooked internal dependencies
   - Mitigation: Comprehensive dependency analysis, build verification

### Medium Priority Risks

1. **CI/CD Configuration**: New workflows may need tuning
   - Mitigation: Test in feature branches first
2. **Documentation Gaps**: Users may be confused by repository split
   - Mitigation: Clear migration guides and announcements

### Low Priority Risks

1. **Git History**: Loss of detailed history in new repository
   - Mitigation: Keep reference to original repository
2. **Issue Tracking**: Existing issues may need migration
   - Mitigation: Create migration script or manual transfer

## Success Criteria

✅ All projects build successfully in new repository
✅ All tests pass in new repository
✅ NuGet packages published and installable
✅ CI/CD pipelines functional
✅ Documentation complete and accurate
✅ Main repository builds without Federation/Identity/Security modules
✅ No breaking changes for existing consumers

## Questions for Clarification

1. **Package Versioning**: Should we start with version 1.0.0 or continue from current version?
2. **Branch Strategy**: Use main/develop or main only for the new repository?
3. **Package Naming**: Keep current names or rebrand under Federation umbrella?
4. **Repository Visibility**: Public immediately or private during migration?
5. **Issue Migration**: Should existing issues be migrated to new repository?

## Next Steps

1. Review and approve this plan
2. Ensure all NuGet packages for dependencies are published and up-to-date
3. Create new GitHub repository
4. Begin Phase 1 preparation in main repository
5. Execute migration phases sequentially

---

*Document Version: 1.0*
*Created: [Current Date]*
*Last Updated: [Current Date]*