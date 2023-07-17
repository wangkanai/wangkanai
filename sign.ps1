$dirs = [ordered]@{
    1  = "System";
    2  = "Validation";
    3  = "Extensions";
    4  = "Testing";
    5  = "Hosting";
    6  = "Tools";
    8  = "Mvc";
    9  = "Webserver";
    10 = "Webmaster";
    11 = "Detection";
    12 = "Responsive";
    13 = "Domain";
    14 = "EntityFramework";
    15 = "Identity";
    16 = "Security";
    17 = "Federation";
    18 = "Markdown";
    19 = "Analytics";
    20 = "Blazor";
    21 = "Tabler";
    22 = "Solver";
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