remove-item .\signed\*.*
new-item -Path signed -ItemType Directory -Force

get-childitem .\ -directory | where { $_.Name -ne 'signed' } | foreach{

    push-location -path $_.Name

    Remove-Item .\artifacts\*.*
    Remove-Item .\signed\*.*

    dotnet --version
    dotnet clean .\src\
    dotnet restore .\src\
    dotnet build .\src\ -c Release
    Get-ChildItem .\src\ -Recurse Wangkanai.*.dll | where { $_.Name -like "*release*" } | foreach {
        signtool sign /fd SHA256 /tr http://ts.ssl.com /td sha256 /n "Sarin Na Wangkanai" $_.FullName
    }

    dotnet pack .\src\ -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg

    dotnet nuget sign .\artifacts\*.nupkg -v diag --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed
    dotnet nuget sign .\artifacts\*.snupkg -v diag --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed
    
    copy-item -path .\signed\*.* -destination .\..\signed\ -force

    pop-location
}

dotnet nuget push .\signed\*.nupkg -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate
# dotnet nuget push .\signed\*.snupkg -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate
