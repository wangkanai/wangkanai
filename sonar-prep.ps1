[bool] $pullrequest = $false

if ("$(sonar.pullrequest.base)" -ne "$(sonar.pullrequest.branch)") {
    $pullrequest = $true
    Write-Host "PR Yes:" $pullrequest
} else {
    $pullrequest = $false
    Write-Host "PR Not:" $pullrequest
} 

Write-Host "sonar.pullrequest.base : "   $(sonar.pullrequest.base)
Write-Host "sonar.pullrequest.branch : " $(sonar.pullrequest.branch)
Write-Host "sonar.pullrequest.key : "    $(sonar.pullrequest.key)
Write-Host "sonar.pullrequest : "        $(sonar.pullrequest)