$buildDir    = $env:AGENT_BUILDDIRECTORY
$sourceDir   = $env:BUILD_SOURCESDIRECTORY
$version     = 1.8

# Get-ChildItem -Path Env:\ | Format-List

write-host "Agent.BuildDirectory:  " $buildDir
write-host "Build.SourcesDirectoy: " $sourceDir
write-host "Version:               " $version

if ("main" -ne $env:BUILD_SOURCEBRANCHNAME)
{
    $pullrequest = $true
    $source      = $env:BUILD_SOURCEBRANCH
    $base        = $env:SYSTEM_PULLREQUEST_TARGETBRANCH
    $branch      = $env:SYSTEM_PULLREQUEST_SOURCEBRANCH
    $key         = $env:SYSTEM_PULLREQUEST_PULLREQUESTNUMBER

    Write-Host "PR Yes:                   " $pullrequest
    Write-Host "soruce:                   " $source
    Write-Host "sonar.pullrequest.base:   " $base
    Write-Host "sonar.pullrequest.branch: " $branch
    Write-Host "sonar.pullrequest.key:    " $key
}
else
{
    $pullrequest = $false
    $base        = "main"

    Write-Host "PR Not:            "   $pullrequest
    Write-Host "sonar.branch.name: "   $base
} 
