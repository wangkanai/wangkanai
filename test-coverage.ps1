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
    dotnet build -c $Configuration
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed"
        exit 1
    }
}

# Run tests with code coverage
$testArgs = @(
    "test",
    "--no-build",
    "-c", $Configuration,
    "--collect:`"Code Coverage;Format=cobertura`"",
    "--results-directory", "./TestResults",
    "--settings", "coverage.runsettings"
)

if ($Filter) {
    $testArgs += "--filter", $Filter
}

Write-Host "Running: dotnet $($testArgs -join ' ')" -ForegroundColor Cyan
& dotnet $testArgs

if ($LASTEXITCODE -ne 0) {
    Write-Error "Tests failed"
    exit 1
}

# Find and merge coverage files
Write-Host "Processing coverage results..." -ForegroundColor Yellow

$coverageFiles = Get-ChildItem -Path "./TestResults" -Filter "*.cobertura.xml" -Recurse
if ($coverageFiles.Count -eq 0) {
    Write-Warning "No coverage files found"
    exit 0
}

# Create coverage directory
New-Item -Path "./coverage" -ItemType Directory -Force | Out-Null

# If single file, just copy it
if ($coverageFiles.Count -eq 1) {
    Copy-Item $coverageFiles[0].FullName -Destination "./coverage/coverage.cobertura.xml"
    Write-Host "Coverage report: ./coverage/coverage.cobertura.xml" -ForegroundColor Green
}
else {
    # Multiple files - need to merge
    Write-Host "Found $($coverageFiles.Count) coverage files, merging..." -ForegroundColor Yellow
    
    # Install ReportGenerator if not present
    if (-not (Get-Command reportgenerator -ErrorAction SilentlyContinue)) {
        Write-Host "Installing ReportGenerator..." -ForegroundColor Yellow
        dotnet tool install -g dotnet-reportgenerator-globaltool
    }
    
    # Merge coverage files
    $inputFiles = $coverageFiles | ForEach-Object { $_.FullName }
    reportgenerator `
        -reports:$($inputFiles -join ';') `
        -targetdir:./coverage `
        -reporttypes:"Cobertura;HtmlInline_AzurePipelines;SonarQube" `
        -verbosity:Info
    
    Write-Host "Merged coverage report: ./coverage/Cobertura.xml" -ForegroundColor Green
    Write-Host "HTML report: ./coverage/index.html" -ForegroundColor Green
    Write-Host "SonarQube report: ./coverage/SonarQube.xml" -ForegroundColor Green
}

# Display summary
Write-Host "`nCoverage collection completed successfully!" -ForegroundColor Green
Write-Host "Reports are available in ./coverage/" -ForegroundColor Green