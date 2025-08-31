param(
    [Parameter(mandatory=$false)]
    [bool]$publish=$true,
    [Parameter(mandatory=$false)]
    [bool]$skip=$false,
    [Parameter(mandatory=$false)]
    [string]$certicate="Open Source Developer, Sarin Na Wangkanai"
)

$dirs=[ordered]@{
#    1="System";
#    2="Validation";
#    3="Annotations";
#    4="Extensions";
#    5="Testing";
#    6="Cryptography";
#    7="Hosting";
#    8="Tools";
#    9="Domain";
#    10="Audit";
#    11="Mvc";
#    12="Webserver";
#    13="Webmaster";
#    14="Detection";
#    15="Responsive";
#    16="EntityFramework";
#    18="Analytics";
}

$env:OneDriveConsumer + "\powershell-env.ps1" | out-null

Write-Host "NuGet Certificate: $certicate"  -ForegroundColor Magenta

$e=[char]27
$root="D:\Sources\wangkanai\"

Set-Location -Path $root

for ($i=0; $i -lt $dirs.count; $i++) {
    $error.clear()
    Push-Location $dirs[$i];
    try
    {
        [Xml]$xml=Get-Content -Path .\Directory.Build.props;
        $version=$xml.Project.PropertyGroup.VersionPrefix;
        if ($version.GetType().FullName -ne "System.String")
        {
            $version=$version[0];
        }
        write-host $dirs[$i] ":" $version -ForegroundColor Red;

        $namespace=$xml.Project.PropertyGroup.PackageNamespace;
        $primary=$xml.Project.PropertyGroup.PackagePrimary;
        $name="Wangkanai." + $dirs[$i];
        if (-not ([string]::IsNullOrEmpty($primary[0])))
        {
            if ($namespace -like "true")
            {
                $name=$name + "." + $primary;
            }
            else
            {
                $name="Wangkanai." + $primary; ;
            }
        }
        $name=$name.Trim();
        $package=Find-Package -Name $name -ProviderName NuGet -AllVersions
        $last=$package | Select-Object -First 1
        $latest=$last.Version;

        if ($latest -ne $version)
        {
            Write-Host $latest " < " $version " Update" -ForegroundColor Green;
            .\sign.ps1 -publish $publish -name $certicate
        }
        else
        {
            Write-Host $latest " = " $version " Skip" -ForegroundColor DarkGray;
        }
    }
    catch
    {
        Write-Host "New " $latest -ForegroundColor Blue;
        .\sign.ps1 -publish $publish -name $certicate
    }

    Pop-Location;
}

if ($skip)
{
    write-host "Skip version update" -ForegroundColor Yellow;
    exit;
}

$result=@()
Set-Location -Path $root

for ($i=0; $i -lt $dirs.count; $i++){
    $error.clear()
    Push-Location $dirs[$i];

    try
    {
        $path=$root + $dirs[$i] + "\Directory.Build.props";
        $xml=New-Object System.Xml.XmlDocument
        $xml.PreserveWhitespace=$true
        $xml.Load($path);
        $node=$xml.SelectSingleNode("/Project/PropertyGroup/VersionPrefix");
        $old=[System.Version]$node.InnerText;
        $new=[System.Version]::new($old.Major, $old.Minor + 1, 0); #, $old.Build + 1); #0);
        $node.InnerText=$new.ToString();
        $xml.Save($path);

        $result+=[PSCustomObject]@{ NuGet="$e[36m" + $dirs[$i] + "$e[0m"; Version="$e[32m$old => $new $e[0m" }
    }
    catch
    {
        $result+=[PSCustomObject]@{ NuGet="$e[31m" + $dirs[$i] + "$e[0m"; Version="$e[31mError $e[0m" }
    }

    Pop-Location
}
$result
