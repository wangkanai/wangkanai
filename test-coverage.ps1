#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Runs tests with code coverage for the entire solution
.DESCRIPTION
    This script runs all tests with code coverage collection using Microsoft Testing Platform
    and generates reports compatible with SonarQube
.PARAMETER Configuration
    Build configuration (Debug/Release). Default: Release
.PARAMETER NoBuild
    Skip building before running tests
.PARAMETER Filter
    Test filter expression
#>
param(
    [string]$Configuration = "Release",
    [switch]$NoBuild,
    [string]$Filter = ""
)

$ErrorActionPreference = "Stop"

Write-Host "Running tests with code coverage..." -ForegroundColor Green

# Clean previous coverage results
if (Test-Path "./TestResults") {
    Remove-Item -Path "./TestResults" -Recurse -Force
}
if (Test-Path "./coverage") {
    Remove-Item -Path "./coverage" -Recurse -Force
}

# Build if not skipping
if (-not $NoBuild) {
    Write-Host "Building solution..." -ForegroundColor Yellow
    # Check for solution file
    $solutionFile = Get-ChildItem -Path . -Filter "*.slnx" | Select-Object -First 1
    if (-not $solutionFile) {
        $solutionFile = Get-ChildItem -Path . -Filter "*.sln" | Select-Object -First 1
    }
    
    if ($solutionFile) {
        dotnet build $solutionFile.FullName -c $Configuration
    } else {
        dotnet build -c $Configuration
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed"
        exit 1
    }
}

# Run tests with Coverlet coverage
Write-Host "Discovering test projects..." -ForegroundColor Yellow
$repoRoot = (Get-Location).Path

# Only search within current directory, not parent directories
$testProjects = Get-ChildItem -Path "." -Filter "*.Tests.csproj" -Recurse | 
    Where-Object { 
        -not ($_.FullName -match "[\\/]bin[\\/]") -and 
        -not ($_.FullName -match "[\\/]obj[\\/]")
    }

if ($testProjects.Count -eq 0) {
    Write-Warning "No test projects found"
    exit 0
}

Write-Host "Found $($testProjects.Count) test project(s)" -ForegroundColor Green

# Create coverage directory
New-Item -Path "./coverage" -ItemType Directory -Force | Out-Null

# Run tests on each project
$totalProjects = $testProjects.Count
$currentProject = 0

foreach ($project in $testProjects) {
    $currentProject++
    Write-Host "`nRunning tests for: $($project.Name) ($currentProject/$totalProjects)" -ForegroundColor Cyan
    
    # Calculate relative path to coverage directory
    $projectDir = Split-Path $project.FullName -Parent
    $relativePath = [System.IO.Path]::GetRelativePath($projectDir, $repoRoot)
    $coverageOutput = Join-Path $relativePath "coverage/"
    
    $testArgs = @(
        "test",
        $project.FullName,
        "--no-build",
        "-c", $Configuration,
        "/p:CollectCoverage=true"
    )
    
    # For all but the last project, output JSON for merging
    # For the last project, merge and output final OpenCover format
    if ($currentProject -eq $totalProjects) {
        # Last project - generate final OpenCover report
        $testArgs += "/p:CoverletOutputFormat=opencover"
        if ($totalProjects -gt 1) {
            $testArgs += "/p:MergeWith=$coverageOutput`coverage.json"
        }
    } else {
        # Intermediate projects - output JSON for merging
        $testArgs += "/p:CoverletOutputFormat=json"
        if ($currentProject -gt 1) {
            $testArgs += "/p:MergeWith=$coverageOutput`coverage.json"
        }
    }
    
    $testArgs += "/p:CoverletOutput=$coverageOutput"
    $testArgs += "/p:SkipAutoProps=true"
    
    if ($Filter) {
        $testArgs += "--filter", $Filter
    }
    
    Write-Host "Running: dotnet $($testArgs -join ' ')" -ForegroundColor DarkGray
    & dotnet $testArgs
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Tests failed for $($project.Name)"
        exit 1
    }
}

# Check coverage results
Write-Host "Checking coverage results..." -ForegroundColor Yellow

# Coverlet generates files in the root coverage directory
if (-not (Test-Path "./coverage/coverage.opencover.xml")) {
    Write-Warning "No coverage files found in ./coverage/"
    exit 0
}

Write-Host "Coverage reports generated:" -ForegroundColor Green
Write-Host "  - OpenCover: ./coverage/coverage.opencover.xml" -ForegroundColor Green

# Generate HTML report if ReportGenerator is available
if (Get-Command reportgenerator -ErrorAction SilentlyContinue) {
    Write-Host "Generating HTML report..." -ForegroundColor Yellow
    reportgenerator `
        -reports:"./coverage/coverage.opencover.xml" `
        -targetdir:"./coverage/html" `
        -reporttypes:"HtmlInline_AzurePipelines;Cobertura" `
        -verbosity:Error
    
    # Copy the generated Cobertura file for compatibility
    if (Test-Path "./coverage/html/Cobertura.xml") {
        Copy-Item "./coverage/html/Cobertura.xml" "./coverage/coverage.cobertura.xml" -Force
        Write-Host "  - Cobertura: ./coverage/coverage.cobertura.xml (generated from OpenCover)" -ForegroundColor Green
    }
    Write-Host "  - HTML: ./coverage/html/index.html" -ForegroundColor Green
}

# Display summary
Write-Host "`nCoverage collection completed successfully!" -ForegroundColor Green
Write-Host "Reports are available in ./coverage/" -ForegroundColor Green