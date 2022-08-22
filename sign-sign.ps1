$suffix = "alpha";

push-location -path .\sign\
dotnet --version
dotnet clean .\src\
dotnet restore .\src\
dotnet build .\src\ -c Release #--version-suffix $suffix
signtool sign /n "Sarin Na Wangkanai" .\src\bin\release\net6.0\Wangkanai.Sign.dll
Remove-Item .\artifacts\*.*
dotnet pack .\src\ -c Release -o .\artifacts --include-symbols -p:SymbolPackageFormat = snupkg #--version-suffix $suffix
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