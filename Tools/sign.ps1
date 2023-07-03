Get-ChildItem .\ -Directory | foreach {
    Push-Location -Path $_.Name
    if (Test-Path .\sign.ps1)
    {
        .\sign.ps1
    }
    Pop-Location
}