push-location -path .\validation\

dotnet --version
dotnet clean .\src\
dotnet restore .\src\
dotnet build .\src\ -c Release
signtool sign /n "Sarin Na Wangkanai" .\src\bin\Release\net6.0\Wangkanai.Validation.dll
Remove-Item .\artifacts\*.*
dotnet pack .\src\ -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat = snupkg
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