if (!(Get-Module -ListAvailable -Name ps2exe)) {
    Install-Module ps2exe
}

if (!(Test-Path -Path ".\build\")) {
    New-Item -Path ".\build\" -ItemType Directory
}

ps2exe .\lunac.ps1 .\build\lunac.exe -verbose