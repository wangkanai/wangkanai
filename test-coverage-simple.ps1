#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Runs tests with code coverage for specific modules or entire solution
.DESCRIPTION
    This script runs tests with Coverlet code coverage collection
.PARAMETER Module
    Specific module to test (e.g., Extensions, Detection). If not specified, runs all tests.
.PARAMETER Configuration
    Build configuration (Debug/Release). Default: Release
.PARAMETER NoBuild
    Skip building before running tests
#>
param(
    [string]$Module = "",
    [string]$Configuration = "Release",
    [switch]$NoBuild
)

$ErrorActionPreference = "Stop"

Write-Host "Running tests with code coverage..." -ForegroundColor Green

# Clean previous coverage results
if (Test-Path "./coverage") {
    Remove-Item -Path "./coverage" -Recurse -Force
}

# Find test projects
Write-Host "Discovering test projects..." -ForegroundColor Yellow
if ($Module) {
    $searchPath = Join-Path -Path (Get-Location).Path -ChildPath $Module
    if (Test-Path $searchPath) {
        $testProjects = Get-ChildItem -Path $searchPath -Filter "*.Tests.csproj" -Recurse -ErrorAction SilentlyContinue | 
            Where-Object { 
                -not ($_.FullName -match "[\\/]bin[\\/]") -and 
                -not ($_.FullName -match "[\\/]obj[\\/]")
            }
    } else {
        Write-Warning "Module path not found: $searchPath"
        $testProjects = @()
    }
} else {
    $testProjects = Get-ChildItem -Path (Get-Location).Path -Filter "*.Tests.csproj" -Recurse | 
        Where-Object { 
            -not ($_.FullName -match "[\\/]bin[\\/]") -and 
            -not ($_.FullName -match "[\\/]obj[\\/]") -and
            $_.FullName.StartsWith((Get-Location).Path)
        }
}

if ($testProjects.Count -eq 0) {
    Write-Warning "No test projects found"
    exit 0
}

Write-Host "Found $($testProjects.Count) test project(s)" -ForegroundColor Green

# Build if needed
if (-not $NoBuild) {
    foreach ($project in $testProjects) {
        Write-Host "Building: $($project.Name)" -ForegroundColor Yellow
        dotnet build $project.FullName -c $Configuration
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Build failed for $($project.Name)"
            exit 1
        }
    }
}

# Create coverage directory
New-Item -Path "./coverage" -ItemType Directory -Force | Out-Null

# Run tests with coverage
$firstProject = $true
foreach ($project in $testProjects) {
    Write-Host "`nRunning tests for: $($project.Name)" -ForegroundColor Cyan
    
    # For subsequent projects, we need to merge with existing coverage
    if ($firstProject) {
        dotnet test $project.FullName `
            --no-build `
            -c $Configuration `
            /p:CollectCoverage=true `
            /p:CoverletOutputFormat=opencover `
            /p:CoverletOutput=../../../coverage/
        $firstProject = $false
    } else {
        dotnet test $project.FullName `
            --no-build `
            -c $Configuration `
            /p:CollectCoverage=true `
            /p:CoverletOutputFormat=json `
            /p:CoverletOutput=../../../coverage/ `
            /p:MergeWith=../../../coverage/coverage.json
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Tests failed for $($project.Name)"
        exit 1
    }
}

# For the last project, generate final opencover report
if ($testProjects.Count -gt 1) {
    $lastProject = $testProjects[-1]
    Write-Host "`nGenerating final coverage report..." -ForegroundColor Yellow
    dotnet test $lastProject.FullName `
        --no-build `
        -c $Configuration `
        /p:CollectCoverage=true `
        /p:CoverletOutputFormat=opencover `
        /p:CoverletOutput=../../../coverage/ `
        /p:MergeWith=../../../coverage/coverage.json `
        /p:SkipAutoProps=true `
        -- RunConfiguration.DisableAppDomain=true
}

# Check results
if (Test-Path "./coverage/coverage.opencover.xml") {
    Write-Host "`nCoverage report generated:" -ForegroundColor Green
    Write-Host "  - OpenCover: ./coverage/coverage.opencover.xml" -ForegroundColor Green
    
    # Generate HTML report if ReportGenerator is available
    if (Get-Command reportgenerator -ErrorAction SilentlyContinue) {
        Write-Host "`nGenerating HTML report..." -ForegroundColor Yellow
        reportgenerator `
            -reports:"./coverage/coverage.opencover.xml" `
            -targetdir:"./coverage/html" `
            -reporttypes:"HtmlInline_AzurePipelines;Cobertura" `
            -verbosity:Error
        Write-Host "  - HTML: ./coverage/html/index.html" -ForegroundColor Green
        
        if (Test-Path "./coverage/html/Cobertura.xml") {
            Copy-Item "./coverage/html/Cobertura.xml" "./coverage/coverage.cobertura.xml" -Force
            Write-Host "  - Cobertura: ./coverage/coverage.cobertura.xml" -ForegroundColor Green
        }
    }
} else {
    Write-Warning "No coverage report was generated"
}

Write-Host "`nCoverage collection completed!" -ForegroundColor Green