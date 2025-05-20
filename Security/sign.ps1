param(
    [Parameter(mandatory=$false)]
    [bool]$publish=$false,
    [Parameter(mandatory=$false)]
    [string]$name="Open Source Developer, Sarin Na Wangkanai"
)

remove-item -path .\signed\*.*    -Force -ErrorAction SilentlyContinue
remove-item -path .\artifacts\*.* -Force -ErrorAction SilentlyContinue

new-item -Path artifacts -ItemType Directory -Force | out-null
new-item -Path signed    -ItemType Directory -Force | out-null

dotnet --version
dotnet clean   Security.slnf -c Release -tl
dotnet restore Security.slnf
dotnet build   Security.slnf -c Release -tl
Get-ChildItem  .\src\ -Recurse Wangkanai.*.dll | where { $_.Directory -like "*Release*" } | foreach {
    signtool sign /fd SHA256 /t http://timestamp.digicert.com /n $name $_.FullName
}

dotnet pack Security.slnf -c Release -tl -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg

dotnet nuget sign .\artifacts\*.nupkg  -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name $name -o .\signed
dotnet nuget sign .\artifacts\*.snupkg -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name $name -o .\signed

if (!$publish)
{
    write-host "Skip update: Security" -ForegroundColor Yellow;
    exit;
}
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:NUGET_API_KEY  -s https://api.nuget.org/v3/index.json
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:GITHUB_API_PAT -s https://nuget.pkg.github.com/wangkanai/index.json
