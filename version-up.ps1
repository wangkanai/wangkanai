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
        $xml = New-Object System.Xml.XmlDocument
        $xml.PreserveWhitespace = $true
        $xml.Load($path);
        $node = $xml.SelectSingleNode("/Project/PropertyGroup/VersionPrefix");
        $old = [System.Version]$node.InnerText;
        $new = [System.Version]::new($old.Major, $old.Minor + 1, 0); #, $old.Build + 1); #0);
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
