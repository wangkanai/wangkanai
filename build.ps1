$dirs=[ordered]@{
    1="System";
    2="Validation";
    3="Annotations";
    4="Extensions";
    5="Testing";
    6="Cryptography";
    7="Hosting";
    8="Tools";
    9="Mvc";
    10="Webserver";
    11="Webmaster";
    12="Detection";
    13="Responsive";
}

for ($i=0; $i -lt $dirs.count; $i++) {
    Push-Location $dirs[$i];
    $build=".\" + $dirs[$i] + "\build.ps1";
    write-host $dirs[$i] -ForegroundColor Red;
    write-host $build    -ForegroundColor Green;
    .\build.ps1
    Pop-Location;
}
