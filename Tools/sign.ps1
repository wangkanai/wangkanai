get-childitem .\ -directory | foreach {
    push-location -path $_.Name
    if (Test-Path .\sign.ps1) {
        .\sign.ps1
    }
    pop-location
}