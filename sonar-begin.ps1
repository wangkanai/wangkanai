if ("main" -ne $env:Build_SourceBranchName) {
    $version     = 1.6
    $pullrequest = $true
    $buildDir    = $env:Agent_BuildDirectory
    $sourceDir   = $env:Build_SourcesDirectory
    $source      = $env:Build_SourceBranch
    $base        = $env:System_PullRequest_TargetBranch
    $branch      = $env:System_PullRequest_SourceBranch
    $key         = $source.Split("/")[2]

    Write-Host "PR Yes:"                     $pullrequest
    Write-Host "sonar.pullrequest.base : "   $base
    Write-Host "sonar.pullrequest.branch : " $branch
    Write-Host "sonar.pullrequest.key : "    $key

    dotnet sonarscanner begin `
            /k:wangkanai_wangkanai `
            /o:wangkanai `
            /v:$version `
            /s:$sourceDir/SonarQube.Analysis.xml `
            /d:sonar.host.url=https://sonarcloud.io `
            /d:sonar.cs.vscoveragexml.reportsPaths=$buildDir/coverage.xml `
            /d:sonar.pullrequest.base=$base `
            /d:sonar.pullrequest.branch=$branch `
            /d:sonar.pullrequest.key=$key
}
else
{
    $base      = "main"
    $buildDir  = $env:Agent_BuildDirector

    Write-Host "PR Not:"              $pullrequest
    Write-Host "sonar.branch.name : " $base

    dotnet sonarscanner begin `
            /k:wangkanai_wangkanai `
            /o:wangkanai `
            /v:$version `
            /s:$sourceDir/SonarQube.Analysis.xml `
            /d:sonar.host.url=https://sonarcloud.io `
            /d:sonar.cs.vscoveragexml.reportsPaths=$buildDir/coverage.xml `
          #  /d:sonar.branch.name=$base
}
