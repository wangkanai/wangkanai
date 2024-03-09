param(
    [Parameter(mandatory=$false)]
    [bool]$dryrun=$false,
    [Parameter(mandatory=$false)]
    [string]$name="Sarin Na Wangkanai"
)

remove-item -path .\signed\*.*    -Force -ErrorAction SilentlyContinue
remove-item -path .\artifacts\*.* -Force -ErrorAction SilentlyContinue

new-item -Path artifacts -ItemType Directory -Force | out-null
new-item -Path signed    -ItemType Directory -Force | out-null

dotnet --version
dotnet clean   Security.slnf -c Release -tl
dotnet restore Security.slnf
dotnet build   Security.slnf -c Release -tl
Get-ChildItem  .\src\ -Recurse Wangkanai.*.dll | where { $_.Name -like "*release*" } | foreach {
    signtool sign /fd SHA256 /tr http://ts.ssl.com /td sha256 /n "Sarin Na Wangkanai" $_.FullName
}

dotnet pack Security.slnf -c Release -tl -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg

dotnet nuget sign .\artifacts\*.nupkg  -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed
dotnet nuget sign .\artifacts\*.snupkg -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name "Sarin Na Wangkanai" -o .\signed

if ($dryrun)
{
    write-host "Dryrun: Security" -ForegroundColor Yellow;
    exit;
}
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:NUGET_API_KEY  -s https://api.nuget.org/v3/index.json
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:GITHUB_API_PAT -s https://nuget.pkg.github.com/wangkanai/index.json
