# Entity Framework Core Migrations for all DbContext in the project

[CmdletBinding()]
param(
	[Parameter(Mandatory=$false)]
	[switch] $DropOnly = $false,

	[Parameter(Mandatory=$false)]
	[switch] $DropCreateSeed = $false,

	[Parameter(Mandatory=$false)]
	[switch] $RemoveMigrationOnly = $false,

	[Parameter(Mandatory=$false)]
	[switch] $NoCreateDb = $false,

	[Parameter(Mandatory=$false)]
	[switch] $NoSeed = $false
)

function database-drop {
	dotnet ef database drop -c ApplicationDbContext -f
	dotnet ef database drop -c PersistedGrantDbContext -f
	dotnet ef database drop -c ConfigurationDbContext -f
}
function migration-remove {
	dotnet ef migrations remove -c PersistedGrantDbContext
	dotnet ef migrations remove -c ConfigurationDbContext
	dotnet ef migrations remove -c ApplicationDbContext
}
function migration-add {
	dotnet ef migrations add InitialPersistedGrant -c PersistedGrantDbContext -o Data/Configuration/PersistedGrantDb
	dotnet ef migrations add InitialConfiguration -c ConfigurationDbContext -o Data/Configuration/ConfigurationDb
	dotnet ef migrations add InitialAspNetCoreIdentity -c ApplicationDbContext -o Data/Users/Migrations
}
function database-update {
	dotnet ef database update -c PersistedGrantDbContext
	dotnet ef database update -c ConfigurationDbContext
	dotnet ef database update -c ApplicationDbContext
}

write-host "IdentityServer Database Droping" -ForegroundColor Yellow
database-drop

if($DropCreateSeed){
	database-update
	dotnet run /seed
}
else{
	if(!$DropOnly) {
		write-host "IdentityServer Migration Remove old snapshot" -ForegroundColor Yellow
		migration-remove
		if(!$RemoveMigrationOnly){
			write-host "IdentityServer Migration Add initial" -ForegroundColor Yellow
			migration-add
			if(!$NoCreateDb){
				write-host "IdentityServer Database Updaing" -ForegroundColor Yellow
				database-update
				if(!$NoSeed) {
					write-host "IdentityServer Database Seeding" -ForegroundColor Yellow
					dotnet run /seed
				}
			}
		}
	}
}