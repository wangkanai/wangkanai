$buildDir    = $env:Agent_BuildDirectory
$sourceDir   = $env:Build_SourcesDirectory

write-host "Agent.BuildDirectory : "  $buildDir
write-host "Build.SourcesDirectoy : " $sourceDir

if ("main" -ne $env:Build_SourceBranchName)
{
    $version     = 1.6
    $pullrequest = $true
    $source      = $env:Build_SourceBranch
    $base        = $env:System_PullRequest_TargetBranch
    $branch      = $env:System_PullRequest_SourceBranch
    $key         = $source.Split("/")[2]

    Write-Host "PR Yes:" $pullrequest
    Write-Host "sonar.pullrequest.base : "   $base
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key

}
else
{
    $pullrequest = $false
    $base        = "main"

    Write-Host "PR Not:" $pullrequest
    Write-Host "sonar.branch.name : "   $base
} 
