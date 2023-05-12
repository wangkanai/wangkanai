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
    24 = "IdentityAdmin";
}

Set-Location -Path D:\Sources\Wangkanai\

Push-Location .\System

try
{
    [Xml]$xml = Get-Content -Path .\Directory.Build.props;
    $version = $xml.Project.PropertyGroup.VersionPrefix;
    if ($version.GetType().FullName -ne "System.String") {
        $version = $version[0];
    }
}
catch
{

}

Pop-Location