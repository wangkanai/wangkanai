#!/usr/bin/env pwsh
#Requires -Version 7.0

<#
.SYNOPSIS
    Enhanced build script for the Wangkanai project with comprehensive error handling and optimization
.DESCRIPTION
    Build, compile, and package projects with support for different build types and parallel execution
.PARAMETER Target
    Project or specific component to build. Default is all projects.
.PARAMETER Type
    Build type: dev (Development), prod (Production), test (Test). Default is Release.
.PARAMETER Clean
    Clean build artifacts before building
.PARAMETER Optimize
    Enable build optimizations and parallel execution
.PARAMETER Verbose
    Enable detailed build output
.PARAMETER SkipRestore
    Skip package restore step
.PARAMETER SkipTests
    Skip building test projects
.PARAMETER Configuration
    Direct configuration override (Debug/Release)
.EXAMPLE
    ./build-enhanced.ps1 -Type prod -Clean -Optimize
    ./build-enhanced.ps1 -Target Analytics -Type dev -Verbose
#>

param(
    [Parameter(Position=0)]
    [string]$Target="all",

    [Parameter()]
    [ValidateSet("dev", "prod", "test")]
    [string]$Type="prod",

    [switch]$Clean,
    [switch]$Optimize,
    [switch]$Verbose,
    [switch]$SkipRestore,
    [switch]$SkipTests,

    [Parameter()]
    [ValidateSet("Debug", "Release")]
    [string]$Configuration
)

# Script configuration
$ErrorActionPreference="Stop"
$InformationPreference=if ($Verbose)
{
    "Continue"
}
else
{
    "SilentlyContinue"
}

# Build configuration mapping
$buildConfigs=@{
    "dev"=@{ Config="Debug"; Opts=@() }
    "prod"=@{ Config="Release"; Opts=@("/p:Optimize=true", "/p:DebugSymbols=false") }
    "test"=@{ Config="Debug"; Opts=@("/p:CollectCoverage=true", "/p:CoverletOutputFormat=opencover") }
}

# Override configuration if directly specified
if ($Configuration)
{
    $buildConfig=$Configuration
    $buildOpts=@()
}
else
{
    $buildConfig=$buildConfigs[$Type].Config
    $buildOpts=$buildConfigs[$Type].Opts
}

# Core functions
function Write-BuildHeader
{
    param([string]$Message)
    Write-Host "`n$( '=' * 80 )" -ForegroundColor Cyan
    Write-Host " $Message" -ForegroundColor White
    Write-Host "$( '=' * 80 )`n" -ForegroundColor Cyan
}

function Write-BuildInfo
{
    param([string]$Message)
    Write-Information "[BUILD] $Message" -InformationAction Continue
}

function Write-BuildSuccess
{
    param([string]$Message)
    Write-Host "✓ $Message" -ForegroundColor Green
}

function Write-BuildError
{
    param([string]$Message)
    Write-Host "✗ $Message" -ForegroundColor Red
}

function Write-BuildWarning
{
    param([string]$Message)
    Write-Host "⚠ $Message" -ForegroundColor Yellow
}

function Test-DotNetSdk
{
    Write-BuildInfo "Checking .NET SDK..."

    if (-not (Get-Command dotnet -ErrorAction SilentlyContinue))
    {
        Write-BuildError ".NET SDK not found. Please install from https://dot.net"
        exit 1
    }

    $sdkVersion=dotnet --version
    $requiredVersion=[version]"9.0.0"
    $currentVersion=[version]($sdkVersion -replace '-.*$', '')

    if ($currentVersion -lt $requiredVersion)
    {
        Write-BuildError "Required .NET SDK version $requiredVersion or higher. Current: $sdkVersion"
        exit 1
    }

    Write-BuildSuccess ".NET SDK $sdkVersion detected"

    # List installed SDKs and runtimes if verbose
    if ($Verbose)
    {
        Write-BuildInfo "Installed SDKs:"
        dotnet --list-sdks | ForEach-Object { Write-Information "  $_" -InformationAction Continue }
        Write-BuildInfo "Installed Runtimes:"
        dotnet --list-runtimes | ForEach-Object { Write-Information "  $_" -InformationAction Continue }
    }
}

function Get-ProjectList
{
    param([string]$Filter="*")

    $projects=@()

    if ($Target -eq "all")
    {
        # Get all project directories
        $projectDirs=Get-ChildItem -Directory | Where-Object {
            Test-Path (Join-Path $_.FullName "src")
        }

        foreach ($dir in $projectDirs)
        {
            $srcProjects=Get-ChildItem -Path (Join-Path $dir.FullName "src") -Filter "*.csproj" -Recurse
            $projects+=$srcProjects

            if (-not $SkipTests)
            {
                $testProjects=Get-ChildItem -Path $dir.FullName -Filter "*Tests.csproj" -Recurse
                $projects+=$testProjects
            }
        }
    }
    else
    {
        # Build specific target
        if (Test-Path $Target)
        {
            $targetPath=$Target
        }
        elseif (Test-Path (Join-Path $Target "src"))
        {
            $targetPath=Join-Path $Target "src"
        }
        else
        {
            Write-BuildError "Target '$Target' not found"
            exit 1
        }

        $projects=Get-ChildItem -Path $targetPath -Filter "*.csproj" -Recurse

        if (-not $SkipTests -and (Test-Path (Join-Path $Target "tests")))
        {
            $testProjects=Get-ChildItem -Path (Join-Path $Target "tests") -Filter "*.csproj" -Recurse
            $projects+=$testProjects
        }
    }

    return $projects
}

function Invoke-BuildStep
{
    param(
        [string]$StepName,
        [string]$Command,
        [string[]]$Arguments,
        [string]$WorkingDirectory=$PWD
    )

    $startTime=Get-Date
    Write-BuildInfo "Starting: $StepName"

    try
    {
        Push-Location $WorkingDirectory

        if ($Verbose)
        {
            Write-BuildInfo "Command: $Command $( $Arguments -join ' ' )"
        }

        $result=& $Command @Arguments 2>&1
        $exitCode=$LASTEXITCODE

        if ($exitCode -ne 0)
        {
            throw "Command failed with exit code $exitCode"
        }

        $duration=(Get-Date) - $startTime
        Write-BuildSuccess "$StepName completed in $($duration.TotalSeconds.ToString('F2') )s"

        if ($Verbose -and $result)
        {
            $result | ForEach-Object { Write-Information $_ -InformationAction Continue }
        }
    }
    catch
    {
        Write-BuildError "$StepName failed: $_"
        if ($result)
        {
            $result | Where-Object { $_ -match "(error|warning)" } | ForEach-Object {
                Write-Host $_ -ForegroundColor Red
            }
        }
        throw
    }
    finally
    {
        Pop-Location
    }
}

function Invoke-ProjectBuild
{
    param(
        [System.IO.FileInfo]$Project,
        [string]$Configuration,
        [string[]]$AdditionalArgs=@()
    )

    $projectName=[System.IO.Path]::GetFileNameWithoutExtension($Project.Name)
    $projectDir=$Project.DirectoryName

    Write-BuildHeader "Building: $projectName"
    Write-BuildInfo "Project: $( $Project.FullName )"
    Write-BuildInfo "Configuration: $Configuration"

    try
    {
        # Clean if requested
        if ($Clean)
        {
            Invoke-BuildStep -StepName "Clean $projectName" `
                -Command "dotnet" `
                -Arguments @("clean", $Project.FullName, "-c", $Configuration, "--nologo", "-v:minimal") `
                -WorkingDirectory $projectDir
        }

        # Restore packages
        if (-not $SkipRestore)
        {
            $restoreArgs=@("restore", $Project.FullName, "--nologo", "-v:minimal")
            if ($Optimize)
            {
                $restoreArgs+="--use-lock-file"
                $restoreArgs+="--locked-mode"
            }

            Invoke-BuildStep -StepName "Restore $projectName" `
                -Command "dotnet" `
                -Arguments $restoreArgs `
                -WorkingDirectory $projectDir
        }

        # Build project
        $buildArgs=@("build", $Project.FullName, "-c", $Configuration, "--no-restore", "--nologo", "-v:minimal")

        if ($Optimize)
        {
            $buildArgs+="-tl"  # Terminal logger for better output
            $buildArgs+="/p:TreatWarningsAsErrors=true"
            $buildArgs+="/p:RunAnalyzers=true"
        }

        $buildArgs+=$AdditionalArgs

        Invoke-BuildStep -StepName "Build $projectName" `
            -Command "dotnet" `
            -Arguments $buildArgs `
            -WorkingDirectory $projectDir

        Write-BuildSuccess "$projectName build completed successfully"
    }
    catch
    {
        Write-BuildError "Failed to build $projectName"
        throw
    }
}

function Show-BuildSummary
{
    param(
        [System.Collections.ArrayList]$Results,
        [TimeSpan]$TotalDuration
    )

    Write-BuildHeader "Build Summary"

    $succeeded=$Results | Where-Object { $_.Success }
    $failed=$Results | Where-Object { -not $_.Success }

    Write-Host "Total Projects: $( $Results.Count )" -ForegroundColor White
    Write-Host "Succeeded: $( $succeeded.Count )" -ForegroundColor Green
    Write-Host "Failed: $( $failed.Count )" -ForegroundColor $( if ($failed.Count -gt 0)
    {
        "Red"
    }
    else
    {
        "Gray"
    } )
    Write-Host "Total Time: $($TotalDuration.ToString('mm\:ss') )" -ForegroundColor White

    if ($failed.Count -gt 0)
    {
        Write-Host "`nFailed Projects:" -ForegroundColor Red
        $failed | ForEach-Object {
            Write-Host "  - $( $_.Project ): $( $_.Error )" -ForegroundColor Red
        }
    }

    if ($Verbose -and $succeeded.Count -gt 0)
    {
        Write-Host "`nSuccessful Projects:" -ForegroundColor Green
        $succeeded | ForEach-Object {
            Write-Host "  - $( $_.Project ) ($($_.Duration.TotalSeconds.ToString('F2') )s)" -ForegroundColor Green
        }
    }
}

# Main execution
function Main
{
    $startTime=Get-Date

    Write-Host @"
╔══════════════════════════════════════════════════════════════════════════════╗
║                        Wangkanai Enhanced Build System                       ║
╚══════════════════════════════════════════════════════════════════════════════╝
"@ -ForegroundColor Cyan

    Write-BuildInfo "Build Type: $Type"
    Write-BuildInfo "Configuration: $buildConfig"
    Write-BuildInfo "Target: $Target"
    Write-BuildInfo "Options: Clean=$Clean, Optimize=$Optimize, SkipRestore=$SkipRestore, SkipTests=$SkipTests"

    # Validate environment
    Test-DotNetSdk

    # Get projects to build
    Write-BuildInfo "Discovering projects..."
    $projects=Get-ProjectList

    if ($projects.Count -eq 0)
    {
        Write-BuildWarning "No projects found to build"
        exit 0
    }

    Write-BuildInfo "Found $( $projects.Count ) project(s) to build"

    # Build results tracking
    $results=New-Object System.Collections.ArrayList

    # Build projects
    if ($Optimize -and $projects.Count -gt 1)
    {
        Write-BuildInfo "Building projects in parallel (max degree: $( [Environment]::ProcessorCount ))"

        $projects | ForEach-Object -Parallel {
            $project=$_
            $buildConfig=$using:buildConfig
            $buildOpts=$using:buildOpts
            $Clean=$using:Clean
            $SkipRestore=$using:SkipRestore
            $Optimize=$using:Optimize
            $Verbose=$using:Verbose

            # Import functions in parallel scope
            ${function:Write-BuildHeader}=$using:Write-BuildHeader
            ${function:Write-BuildInfo}=$using:Write-BuildInfo
            ${function:Write-BuildSuccess}=$using:Write-BuildSuccess
            ${function:Write-BuildError}=$using:Write-BuildError
            ${function:Invoke-BuildStep}=$using:Invoke-BuildStep
            ${function:Invoke-ProjectBuild}=$using:Invoke-ProjectBuild

            $result=@{
                Project=[System.IO.Path]::GetFileNameWithoutExtension($project.Name)
                Success=$true
                Error=$null
                Duration=[TimeSpan]::Zero
            }

            $projectStart=Get-Date

            try
            {
                Invoke-ProjectBuild -Project $project -Configuration $buildConfig -AdditionalArgs $buildOpts
            }
            catch
            {
                $result.Success=$false
                $result.Error=$_.Exception.Message
            }
            finally
            {
                $result.Duration=(Get-Date) - $projectStart
            }

            $result
        } -ThrottleLimit $( [Environment]::ProcessorCount ) | ForEach-Object {
            [void]$results.Add($_)
        }
    }
    else
    {
        # Sequential build
        foreach ($project in $projects)
        {
            $result=@{
                Project=[System.IO.Path]::GetFileNameWithoutExtension($project.Name)
                Success=$true
                Error=$null
                Duration=[TimeSpan]::Zero
            }

            $projectStart=Get-Date

            try
            {
                Invoke-ProjectBuild -Project $project -Configuration $buildConfig -AdditionalArgs $buildOpts
            }
            catch
            {
                $result.Success=$false
                $result.Error=$_.Exception.Message

                if (-not $ContinueOnError)
                {
                    [void]$results.Add($result)
                    break
                }
            }
            finally
            {
                $result.Duration=(Get-Date) - $projectStart
            }

            [void]$results.Add($result)
        }
    }

    # Show summary
    $totalDuration=(Get-Date) - $startTime
    Show-BuildSummary -Results $results -TotalDuration $totalDuration

    # Exit with appropriate code
    $failedCount=($results | Where-Object { -not $_.Success }).Count
    if ($failedCount -gt 0)
    {
        Write-BuildError "Build failed with $failedCount error(s)"
        exit $failedCount
    }

    Write-BuildSuccess "Build completed successfully!"
    exit 0
}

# Execute main function
try
{
    Main
}
catch
{
    Write-BuildError "Unexpected error: $_"
    exit 1
}
