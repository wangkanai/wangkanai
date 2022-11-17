push-location -path .\blazor\
dotnet --version
dotnet clean 
dotnet restore   
dotnet build -c Release
Get-ChildItem .\src\ -Recurse Wangkanai.*.dll  | foreach {
    signtool sign /fd SHA256 /n "Sarin Na Wangkanai" $_.FullName
}
Remove-Item .\artifacts\*.*
dotnet pack -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg
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

pop-location