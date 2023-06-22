if ("main" -ne $env:Build.SourceBranchName) {
    $version     = 1.6
    $pullrequest = $true
    $directory   = $env:Build.SourcesDirectory
    $source      = $env:Build.SourceBranch
    $base        = $env:System.PullRequest.TargetBranch
    $branch      = $env:System.PullRequest.SourceBranch
    $key         = $source.Split("/")[2]
    
    Write-Host "PR Yes:" $pullrequest
    Write-Host "sonar.pullrequest.base : "   $base
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key
    
} else {
    $pullrequest = $false
    
    Write-Host "PR Not:" $pullrequest
} 
