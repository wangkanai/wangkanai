$dirs=[ordered]@{
    1="System";
    2="Validation";
    3="Extensions";
    4="Testing";
    5="Cryptography";
    6="Hosting";
    7="Tools";
    8="Domain";
    9="Mvc";
    10="Webserver";
    11="Webmaster";
    12="Detection";
    13="Responsive";
    14="EntityFramework";
    15="Identity";
    16="Security";
    17="Federation";
    18="Markdown";
    19="Analytics";
    20="Blazor";
    21="Tabler";
    22="Solver";
}

for ($i=0; $i -lt $dirs.count; $i++) {
	Push-Location $dirs[$i];
	$build = ".\" + $dirs[$i] + "\build.ps1";
	write-host $dirs[$i] -ForegroundColor Red;
	write-host $build    -ForegroundColor Green;
	.\build.ps1
	Pop-Location;
}
