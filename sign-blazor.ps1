push-location -path .\blazor\

remove-item -path .\signed\*.*

dotnet --version
dotnet clean Blazor.slnf
dotnet restore Blazor.slnf  
dotnet build -c Release Blazor.slnf
Get-ChildItem .\src\ -Recurse Wangkanai.*.dll  | foreach {
    signtool sign /fd SHA256 /n "Sarin Na Wangkanai" $_.FullName
}
Remove-Item .\artifacts\*.*
dotnet pack Blazor.slnf -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg
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

dotnet nuget push .\signed\*.nupkg -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate
dotnet nuget push .\signed\*.snupkg -k $env:NUGET_API_KEY -s https://api.nuget.org/v3/index.json --skip-duplicate

pop-location