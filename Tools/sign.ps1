param(
    [Parameter(mandatory = $false)]
    [switch]$dryrun = $false
)

Get-ChildItem .\ -Directory | foreach {
    Push-Location -Path $_.Name
    if (Test-Path .\sign.ps1)
    {
        .\sign.ps1 -dryrun $dryrun
    }
    Pop-Location
}
