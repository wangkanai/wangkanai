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
dotnet clean   .\src\Core\ -c Release -tl
dotnet restore .\src\Core\
dotnet build   .\src\Core\ -c Release -tl
Get-ChildItem  .\src\Core\ -Recurse Wangkanai.*.dll | where { $_.Directory -like "*Release*" } | foreach {
    signtool sign /fd SHA256 /t http://timestamp.digicert.com /n $name $_.FullName
}

dotnet pack .\src\Core\ -c Release -tl -o .\artifacts --include-symbols -p:SymbolPackageFormat=snupkg

dotnet nuget sign .\artifacts\*.nupkg  -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name $name -o .\signed
dotnet nuget sign .\artifacts\*.snupkg -v normal --timestamper http://timestamp.digicert.com --certificate-subject-name $name -o .\signed

if ($dryrun)
{
    write-host "Dryrun: Blazor" -ForegroundColor Yellow;
    exit;
}
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:NUGET_API_KEY  -s https://api.nuget.org/v3/index.json
dotnet nuget push .\signed\*.nupkg --skip-duplicate -k $env:GITHUB_API_PAT -s https://nuget.pkg.github.com/wangkanai/index.json
