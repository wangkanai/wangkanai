push-location -path .\blazor\
dotnet --version
dotnet clean .\src\core
dotnet restore .\src\core
dotnet build .\src\core -c Release
signtool sign /fd SHA256 /n "Sarin Na Wangkanai" .\src\core\bin\release\net6.0\*.dll
Remove-Item .\artifacts\*.*
dotnet pack .\src\core -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg
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