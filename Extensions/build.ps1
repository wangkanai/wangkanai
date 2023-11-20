dotnet --version

get-childitem .\ -directory | where { $_.Name -ne 'signed' } | where { $_.Name -ne 'artifacts' } | foreach {

    push-location -path $_.Name
	dotnet clean   .\src\ -c Release -tl
	dotnet restore .\src\
	dotnet build   .\src\ -c Release -tl

	dotnet clean   .\tests\ -c Release -tl
	dotnet restore .\tests\
	dotnet build   .\tests\ -c Release -tl
	pop-location
}
