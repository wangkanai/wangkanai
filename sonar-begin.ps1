if ("$( sonar.pullrequest.base )" -ne "$( sonar.pullrequest.branch )")
{
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

    dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /v:$version /s:$source/SonarQube.Analysis.xml /d:sonar.host.url=https://sonarcloud.io /d:sonar.cs.vscoveragexml.reportsPaths=$source/coverage.xml /d:sonar.pullrequest.base=$base /d:sonar.pullrequest.branch=$branch /d:sonar.pullrequest.key=$key
}
else
{
    Write-Host "PR Not:" $pullrequest

    dotnet sonarscanner begin /k:wangkanai_wangkanai /o:wangkanai /v:$version /s:$source/SonarQube.Analysis.xml /d:sonar.host.url=https://sonarcloud.io /d:sonar.cs.vscoveragexml.reportsPaths=$source/coverage.xml /d:sonar.branch.name=$base
}
