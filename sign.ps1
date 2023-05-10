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
    23 = "Solver";
}

Set-Location -Path D:\Sources\Wangkanai\

for ($i = 0; $i -lt $dirs.count; $i++) {
    $error.clear()
    pushd $dirs[$i];
    try
    {
        Write-Host $dirs[$i] -ForegroundColor Red;
        [Xml]$xml = Get-Content -Path .\Directory.Build.props;
        $version = $xml.Project.PropertyGroup.VersionPrefix[0];
        $namespace = $xml.Project.PropertyGroup.PackageNamespace[0];
        $primary = $xml.Project.PropertyGroup.PackagePrimary[0];
        write-host $version + $namespace -foregroundcolor yellow;
        $name = "Wangkanai." + $dirs[$i];
        if($namespace -or (-not ([string]::IsNullOrEmpty($primary)))){
            $name += "."+$primary;
        }
        write-host $name -foregroundcolor green;
        $package = Find-Package -Name $name -ProviderName NuGet -AllVersions
        $last = $package | Select-Object -First 1
        $latest = $last.Version;
        Write-Host $latest -foregroundcolor Magenta;
        if($latest -ne $version){
            write-host "Update" -foregroundcolor Cyan;
            .\sign.ps1
        }
        else{
            write-host "Skip" -foregroundcolor DarkGray;
        }
    }
    catch{
        write-host "New" -foregroundcolor Blue;
        .\sign.ps1
    }
    popd;
}