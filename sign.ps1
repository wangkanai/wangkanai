$dirs = [ordered]@{
    1  = "System";
    2  = "Validation";
    3  = "Extensions";
    4  = "Hosting";
    5  = "Tools";
    6  = "Watcher";
    7  = "Mvc";
    8  = "Webserver";
    9  = "Webmaster";
    10 = "Detection";
    11 = "Responsive";
    12 = "Domain";
    13 = "EntityFramework";
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
    Push-Location $dirs[$i];
    try {
        Write-Host $dirs[$i] -ForegroundColor Red;
        [Xml]$xml = Get-Content -Path .\Directory.Build.props;
        $version = $xml.Project.PropertyGroup.VersionPrefix;
        if ($version.GetType().FullName -ne "System.String") {
            $version = $version[0];
        }
        $namespace = $xml.Project.PropertyGroup.PackageNamespace;
        $primary = $xml.Project.PropertyGroup.PackagePrimary;
        $name = "Wangkanai." + $dirs[$i];
        if (-not ([string]::IsNullOrEmpty($primary[0]))) {
            if ($namespace -like "true") {
                $name = $name + "." + $primary;
            }
            else {
                $name = "Wangkanai." + $primary; ;
            }
        }
        $name = $name.Trim();
        $package = Find-Package -Name $name -ProviderName NuGet -AllVersions
        $last = $package | Select-Object -First 1
        $latest = $last.Version;
        
        if ($latest -ne $version) {
            Write-Host $latest " < " $version " Update" -ForegroundColor Green;
            .\sign.ps1
        }
        else {
            Write-Host $latest " = " $version " Skip" -ForegroundColor DarkGray;
        }
    }
    catch {
        Write-Host "New " $latest -ForegroundColor Blue;
        .\sign.ps1
    }

    Pop-Location;
}