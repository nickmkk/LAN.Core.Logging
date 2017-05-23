@echo off

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey%

@echo ---------------------------------------------------
.nuget\nuget pack LAN.Core.Logging\LAN.Core.Logging.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
@echo Finished Building: LAN.Core.Logging
@echo ---------------------------------------------------

@echo Pushing To Nuget and SymbolSource...
set /p loggingVersion=Enter LAN.Core.Logging Package Version:%=%
.nuget\nuget push LAN.Core.Logging.%loggingVersion%.nupkg -Source https://www.nuget.org/api/v2/package
@echo ---------------------------------------------------