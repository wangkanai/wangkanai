$dirs = [ordered]@{
    1 = "System";
    2 = "Validation";
    3 = "Extensions";
    4 = "Hosting";
    5 = "Tools";
    6 = "Watcher";
    7 = "Mvc";
    8 = "Webserver";
    9 = "Webmaster";
    10 = "Detection";
    11 = "Responsive";
    12 = "EntityFramework";
    13 = "Domain";
    14 = "Identity";
    15 = "Security";
    16 = "Federation";
    17 = "Markdown";
    18 = "Graph";
    19 = "Universal";
    20 = "Analytics";
    21 = "Blazor";
    22 = "Tabler";

}

for ($i = 0; $i -lt $dirs.count + 1; $i++) {
    Write-Host $dirs[$i] -ForegroundColor Red;
    pushd $dirs[$i];
    .\sign.ps1;
    popd;
}