@echo off

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey% -Source https://www.nuget.org/api/v2/package
 
@echo ---------------------------------------------------
.nuget\nuget pack LAN.Core.Logging.NLog\LAN.Core.Logging.NLog.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
@echo Finished Building: LAN.Core.Logging.NLog
@echo ---------------------------------------------------

@echo Pushing To Nuget and SymbolSource...
set /p nlogVersion=Enter LAN.Core.Logging.NLog Package Version:%=%
.nuget\nuget push LAN.Core.Logging.NLog.%nlogVersion%.nupkg -Source https://www.nuget.org/api/v2/package
@echo ---------------------------------------------------