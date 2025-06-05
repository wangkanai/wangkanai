$buildDir    = $env:AGENT_BUILDDIRECTORY
$sourceDir   = $env:GITHUB_WORKSPACE
$version     = 1.0

write-host "Agent.BuildDirectory:  " $buildDir
write-host "Build.SourcesDirectoy: " $sourceDir
write-host "Version:               " $version

if ("main" -ne $env:BUILD_SOURCEBRANCHNAME) {
    $pullrequest = $true
#    $source      = $env:BUILD_SOURCEBRANCH
#    $base        = $env:SYSTEM_PULLREQUEST_TARGETBRANCH
#    $branch      = $env:SYSTEM_PULLREQUEST_SOURCEBRANCH
#    $key         = $env:SYSTEM_PULLREQUEST_PULLREQUESTNUMBER

    Write-Host "PR Yes:                   " $pullrequest
#    Write-Host "soruce:                   " $source
#    Write-Host "sonar.pullrequest.base:   " $base
#    Write-Host "sonar.pullrequest.branch: " $branch
#    Write-Host "sonar.pullrequest.key:    " $key

    dotnet sonarscanner begin `
            /k:wangkanai_wangkanai `
            /o:wangkanai `
            /v:$version `
            /s:$sourceDir/SonarQube.Analysis.xml `
            /d:sonar.host.url=https://sonarcloud.io `
            /d:sonar.cs.vscoveragexml.reportsPaths=$sourceDir/coverage.xml
#            /d:sonar.pullrequest.base=$base `
#            /d:sonar.pullrequest.branch=$branch `
#            /d:sonar.pullrequest.key=$key
}
else
{
    $base      = "main"

    Write-Host "PR Not:             " $pullrequest
#    Write-Host "sonar.branch.name : " $base

    dotnet sonarscanner begin `
            /k:wangkanai_wangkanai `
            /o:wangkanai `
            /v:$version `
            /s:./SonarQube.Analysis.xml `
            /d:sonar.host.url=https://sonarcloud.io `
            /d:sonar.cs.vscoveragexml.reportsPaths=$sourceDir/coverage.xml
#            /d:sonar.branch.name=$base
}
