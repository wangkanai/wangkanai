if ("$( sonar.pullrequest.base )" -ne "$( sonar.pullrequest.branch )")
{
    $pullrequest = $true
    $branch      = $( System.PullRequest.SourceBranch )
    $key         = "$( Build.SourceBranch )".Split("/")
    $key

    Write-Host "PR Yes:" $pullrequest
    Write-Host "sonar.pullrequest.base : "   $(sonar.pullrequest.base)
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key

    dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /v:1.5 /s:$( Build.SourcesDirectory )/SonarQube.Analysis.xml /d:sonar.host.url = https://sonarcloud.io /d:sonar.cs.vscoveragexml.reportsPaths = $( Build.SourcesDirectory )/coverage.xml /d:sonar.pullrequest.base = $( sonar.pullrequest.base ) /d:sonar.pullrequest.branch = $branch /d:sonar.pullrequest.key = $key

}
else
{
    Write-Host "PR Not:" $pullrequest

    dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /v:1.5 /s:$( Build.SourcesDirectory )/SonarQube.Analysis.xml /d:sonar.host.url = https://sonarcloud.io /d:sonar.cs.vscoveragexml.reportsPaths = $( Build.SourcesDirectory )/coverage.xml /d:sonar.branch.name = main
}
