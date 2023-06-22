if ("main" -ne $env:Build_SourceBranchName) {
    $version     = 1.6
    $pullrequest = $true
    $directory   = $env:Build_SourcesDirectory
    $source      = $env:Build_SourceBranch
    $base        = $env:System_PullRequest_TargetBranch
    $branch      = $env:System_PullRequest_SourceBranch
    $key         = $source.Split("/")[2]
    
    Write-Host "PR Yes:" $pullrequest
    Write-Host "sonar.pullrequest.base : "   $base
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key
    
} else {
    $pullrequest = $false
    
    Write-Host "PR Not:" $pullrequest
} 
