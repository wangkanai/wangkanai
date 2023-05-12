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
    24 = "IdentityAdmin";
}
$e = [char]27
$root = "D:\Sources\Wangkanai\"
$result = @()
Set-Location -Path $root

for ($i = 0; $i -lt $dirs.count; $i++){
    $error.clear()
    Push-Location $dirs[$i];

    try
    {
        $path = $root + $dirs[$i] + "\Directory.Build.props";
        [Xml]$xml = Get-Content -Path $path;
        $node = $xml.SelectSingleNode("/Project/PropertyGroup/VersionPrefix");
        $old = [System.Version]$node.InnerText;
        $new = [System.Version]::new($old.Major, $old.Minor + 1, 0);
        $node.InnerText = $new.ToString();
        $xml.Save($path);

        $result += [PSCustomObject]@{ NuGet = "$e[36m" + $dirs[$i] + "$e[0m"; Version = "$e[32m$old => $new $e[0m" }
    }
    catch
    {
        $result += [PSCustomObject]@{ NuGet = "$e[31m" + $dirs[$i] + "$e[0m"; Version = "$e[31mError $e[0m" }
    }

    Pop-Location
}
$result
