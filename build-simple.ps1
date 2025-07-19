#!/usr/bin/env pwsh
# Simple build wrapper for common scenarios

param(
    [Parameter(Position=0)]
    [ValidateSet("dev", "prod", "test", "clean", "restore")]
    [string]$Action="prod"
)

$buildScript=Join-Path $PSScriptRoot "build-enhanced.ps1"

switch ($Action)
{
    "dev" {
        & $buildScript -Type dev -Verbose
    }
    "prod" {
        & $buildScript -Type prod -Clean -Optimize
    }
    "test" {
        & $buildScript -Type test -Verbose
    }
    "clean" {
        Write-Host "Cleaning all build artifacts..." -ForegroundColor Yellow
        Get-ChildItem -Recurse -Include bin,obj | Remove-Item -Recurse -Force
        Write-Host "Clean completed!" -ForegroundColor Green
    }
    "restore" {
        Write-Host "Restoring all packages..." -ForegroundColor Yellow
        dotnet restore --force
        Write-Host "Restore completed!" -ForegroundColor Green
    }
}
