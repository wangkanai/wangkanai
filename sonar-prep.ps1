$buildDir    = $env:Agent_BuildDirectory
$sourceDir   = $env:Build_SourcesDirectory
$version     = 1.6

Write-Host "Env:                   " $env

write-host "Agent.BuildDirectory:  " $buildDir
write-host "Build.SourcesDirectoy: " $sourceDir
write-host "Version:               " $version

if ("main" -ne $env:Build_SourceBranchName)
{
    $pullrequest = $true
    $source      = $env:Build_SourceBranch
    $base        = $env:System_PullRequest_TargetBranch
    $branch      = $env:System_PullRequest_SourceBranch
    $key         = $env:System_PullRequest_Id

    Write-Host "PR Yes:                   " $pullrequest
    Write-Host "sonar.pullrequest.base:   " $base
    Write-Host "sonar.pullrequest.branch: " $branch
    Write-Host "sonar.pullrequest.key:    " $key

}
else
{
    $pullrequest = $false
    $base        = "main"

    Write-Host "PR Not:" $pullrequest
    Write-Host "sonar.branch.name: "   $base
} 
