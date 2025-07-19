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

# Run tests with Coverlet coverage
$testArgs = @(
    "test",
    "--no-build",
    "-c", $Configuration,
    "/p:CollectCoverage=true",
    "/p:CoverletOutputFormat=opencover",
    "/p:CoverletOutput=./coverage/",
    "/p:MergeWith=./coverage/coverage.json",
    "/p:SkipAutoProps=true"
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
        -reports:"./coverage/coverage.cobertura.xml" `
        -targetdir:"./coverage/html" `
        -reporttypes:"HtmlInline_AzurePipelines" `
        -verbosity:Error
    Write-Host "  - HTML: ./coverage/html/index.html" -ForegroundColor Green
}

# Display summary
Write-Host "`nCoverage collection completed successfully!" -ForegroundColor Green
Write-Host "Reports are available in ./coverage/" -ForegroundColor Green