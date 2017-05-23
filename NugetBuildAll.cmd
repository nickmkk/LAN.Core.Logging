@echo off
Setlocal EnableDelayedExpansion

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey%

SET /p deployLogging=Would you like to deploy the LAN.Core.Logging Project? (y/n) %=%
IF (!deployLogging!) EQU (y) (
    .nuget\nuget pack LAN.Core.Logging\LAN.Core.Logging.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
    @echo Finished Building: LAN.Core.Logging
    set /p loggingVersion=Enter LAN.Core.Logging Package Version:%=%
    .nuget\nuget push LAN.Core.Logging.!loggingVersion!.nupkg -Source https://www.nuget.org/api/v2/package
)

@echo ---------------------------------------------------
SET /p deployNlogLogger=Would you like to deploy the LAN.Core.Logging.NLog Project? (y/n) %=%
IF (!deployNlogLogger!) EQU (y) (
    .nuget\nuget pack LAN.Core.Logging.NLog\LAN.Core.Logging.NLog.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
    @echo Finished Building: LAN.Core.Logging.Nlog
    set /p nlogVersion=Enter LAN.Core.Logging.NLog Package Version:%=%
    .nuget\nuget push LAN.Core.Logging.NLog.!nlogVersion!.nupkg -Source https://www.nuget.org/api/v2/package
)
