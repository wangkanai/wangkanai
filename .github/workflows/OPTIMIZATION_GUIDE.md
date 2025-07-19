# GitHub Actions Workflow Optimization Guide

## Current Issues with dotnet.yml

1. **Sequential Execution**: Everything runs in a single job
2. **No Caching**: Rebuilds .NET, NuGet packages, and tools every time
3. **Redundant Testing**: Tests run twice (once without coverage, once with)
4. **Inefficient Coverage Collection**: Sequential coverage collection for each project
5. **No Parallelization**: All modules built sequentially

## Optimization Strategies

### 1. Quick Check + Matrix Build (dotnet-matrix.yml)
- **Quick Check Job**: Fast build/test to fail early
- **Parallel Module Builds**: Split 27 modules into 6 groups
- **Separate SonarCloud**: Run analysis in parallel
- **Expected Time Reduction**: 60-70%

### 2. Aggressive Caching (dotnet-optimized.yml)
- **Cache .NET Installation**: Save ~1-2 minutes
- **Cache NuGet Packages**: Save ~2-3 minutes
- **Cache Build Output**: Save ~3-4 minutes  
- **Cache Global Tools**: Save ~30 seconds
- **Expected Time Reduction**: 40-50%

### 3. Additional Optimizations

#### Use Self-Hosted Runners
```yaml
runs-on: [self-hosted, linux, x64]
```
Benefits:
- Pre-installed dependencies
- Persistent caches
- No setup overhead

#### Conditional Workflows
```yaml
on:
  push:
    branches: [ "main" ]
    paths-ignore:
      - '**.md'
      - 'docs/**'
      - '.gitignore'
```

#### Selective Module Testing
```yaml
- name: Detect Changed Modules
  id: changes
  uses: dorny/paths-filter@v2
  with:
    filters: |
      detection:
        - 'Detection/**'
      responsive:
        - 'Responsive/**'
```

#### Use .NET Test Parallelization
```xml
<!-- In test projects -->
<PropertyGroup>
  <ParallelizeTestCollections>true</ParallelizeTestCollections>
  <MaxCpuCount>0</MaxCpuCount>
</PropertyGroup>
```

### 4. Recommended Implementation

1. **Immediate**: Replace current workflow with `dotnet-optimized.yml`
2. **Next Sprint**: Implement matrix build strategy
3. **Future**: Consider self-hosted runners for maximum performance

### 5. Performance Metrics

Current estimated time: 15-20 minutes
- Setup .NET: 2-3 minutes
- Install tools: 1-2 minutes  
- Restore: 3-4 minutes
- Build: 3-4 minutes
- Test: 3-4 minutes
- Coverage: 4-5 minutes
- SonarCloud: 2-3 minutes

Optimized estimated time: 6-8 minutes
- Quick check: 2-3 minutes (parallel)
- Module builds: 3-4 minutes (parallel)
- SonarCloud: 3-4 minutes (parallel)

### 6. Monitoring

Add timing to each step:
```yaml
- name: Build
  run: |
    START_TIME=$(date +%s)
    dotnet build --no-restore -c Release
    END_TIME=$(date +%s)
    echo "Build time: $((END_TIME - START_TIME)) seconds"
```

### 7. Gradual Rollout

1. Test optimized workflow in a branch
2. Run both workflows in parallel initially
3. Monitor for any issues
4. Replace main workflow after validation