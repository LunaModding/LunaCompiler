Param(
    [Parameter(Mandatory=$true)]
    $command="",

    [Parameter(Mandatory=$false)]
    [Switch] $help
)

function Write-Help {
    Write-Output "`"lunac [command]`" - A compiler for Luna projects."
    Write-Output "`n-- Commands --`n"
    Write-Output "`"init`" - Creates a new Luna project."
    Write-Output "`"build`" - Builds the Luna project in the current directory."
}

function Replace-Recursively {
    Param(
        [Parameter(Mandatory=$true)]
        [String] $path="",

        [Parameter(Mandatory=$true)]
        [String] $phrase="",

        [Parameter(Mandatory=$true)]
        [String] $replacement=""
    )

    Get-ChildItem $path -Include *.* -Recurse | 
        Select -expand FullName |
            foreach {
                (Get-Content $_) -Replace $phrase, $replacement |
                    Set-Content $_
            }
}

if ($help -eq $true) {
    Write-Help
}

if ($command -eq "init") {
    Write-Output "Downloading template..."
    Invoke-WebRequest -Uri "https://github.com/LunaModding/LunaTemplate/archive/refs/tags/v0.1.0.zip" -OutFile "$PSScriptRoot\template.zip"
    Expand-Archive "$PSScriptRoot\template.zip" -DestinationPath $PSScriptRoot -Force
    Copy-Item -Path "$PSScriptRoot\*\`$id`$\" -Destination "$PSScriptRoot\`$id`$\" -Recurse -Force
    Remove-Item (Get-Item "$PSScriptRoot\*\`$id`$").Parent.FullName -Recurse -Force
    Remove-Item ".\*.zip" -Force

    $id = Read-Host -Prompt "Please enter a value for `"id`""
    Rename-Item -Path "$PSScriptRoot\`$id`$\" -NewName $id -Force
    Remove-Item -Path "$PSScriptRoot\$id\*\.*" -Recurse -Force
    Replace-Recursively -Path $PSScriptRoot\$id\ -Phrase "`$id`$" -Replacement $id
}