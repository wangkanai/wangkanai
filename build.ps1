$dirs=[ordered]@{
    1="System";
    2="Validation";
    3="Annotations";
    4="Extensions";
    5="Testing";
    6="Cryptography";
    7="Hosting";
    8="Tools";
    9="Domain";
    10="Mvc";
    11="Webserver";
    12="Webmaster";
    13="Detection";
    14="Responsive";
    15="EntityFramework";
    16="Identity";
    17="Security";
    18="Federation";
    19="Markdown";
    20="Analytics";
    21="Blazor";
    22="Tabler";
    23="Solver";
    24="Microservice";
    25="Nation";
}

for ($i=0; $i -lt $dirs.count; $i++) {
	Push-Location $dirs[$i];
	$build = ".\" + $dirs[$i] + "\build.ps1";
	write-host $dirs[$i] -ForegroundColor Red;
	write-host $build    -ForegroundColor Green;
	.\build.ps1
	Pop-Location;
}
