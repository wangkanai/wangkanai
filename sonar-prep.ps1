if ("$(sonar.pullrequest.base)" -ne "$(sonar.pullrequest.branch)") {
    $version     = 1.6
    $pullrequest = $true
    $source      = $(Build.SourcesDirectory)
    $base        = $(System.PullRequest.TargetBranch)
    $branch      = $(System.PullRequest.SourceBranch)
    $key         = "$(Build.SourceBranch)".Split("/")[2]
    
    Write-Host "PR Yes:" $pullrequest
    Write-Host "sonar.pullrequest.base : "   $base
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key
    
} else {
    $pullrequest = $false
    
    Write-Host "PR Not:" $pullrequest
} 
