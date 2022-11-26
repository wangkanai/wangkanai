push-location -path .\extensions\

remove-item .\signed\*.*
new-item -Path signed -ItemType Directory -Force

get-childitem .\ -directory | where {$_.Name -ne 'signed'} | foreach{

    push-location -path $_.Name
    
    Remove-Item .\artifacts\*.*
    remove-item .\signed\*.*

    dotnet --version
    dotnet clean .\src\
    dotnet restore .\src\
    dotnet build .\src\ -c Release
    Get-ChildItem .\src\ -Recurse Wangkanai.*.dll  | foreach {
        signtool sign /fd SHA256 /n "Sarin Na Wangkanai" $_.FullName
    }

    dotnet pack .\src\ -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg
    nuget sign .\artifacts\*.nupkg `
      -CertificateStoreLocation CurrentUser `
      -CertificateStoreName My `
      -CertificateSubjectName 'Sarin Na Wangkanai' `
      -Timestamper http://ts.ssl.com `
      -OutputDirectory .\signed
    nuget sign .\artifacts\*.snupkg `
      -CertificateStoreLocation CurrentUser `
      -CertificateStoreName My `
      -CertificateSubjectName 'Sarin Na Wangkanai' `
      -Timestamper http://ts.ssl.com `
      -OutputDirectory .\signed
    
    copy-item -path .\signed\*.* -destination .\..\signed\ -force
    
    pop-location
}
pop-location