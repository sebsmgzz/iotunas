
# Set working directory
$workDir = Split-Path -parent $PSScriptRoot

# Update changelog
$projPath = [IO.Path]::Combine($workDir, "src", "IoTunas.Core", "IoTunas.Core.csproj")
$changelogPath = [IO.Path]::Combine($workDir, "docs", "CHANGELOG.md")

## Get version
$projXml = [Xml] (Get-Content $projPath)
$version = $projXml.Project.PropertyGroup.Version

## Get git logs
($logs = git log --pretty="- %s")
$feats = [System.Collections.ArrayList]::new()
$fixes = [System.Collections.ArrayList]::new()
ForEach ($line in $($logs -split "`r`n"))
{
    if ($line.StartsWith("- feat")) 
    {
        $feats.Add($line)
    }
    elseif ($line.StartsWith("- fix")) 
    {
        $fixes.Add($line)
    }
}

Write-Host $($feats.Count)
return
Add-Content -Path $changelogPath -Value "`n"
Add-Content -Path $changelogPath -Value "Release v$version"
Add-Content -Path $changelogPath -Value "-----------------"

## Set features
Add-Content -Path $changelogPath -Value "Features:"
if($feats.Count > 0)
{
    ForEach ($feat in $feats)
    {
        Add-Content -Path $changelogPath -Value $feat
    }
}
else
{
    Add-Content -Path $changelogPath -Value "N/A"
}

## Set fixes
Add-Content -Path $changelogPath -Value "Fixes:"
if($fixes.Count > 0)
{
    ForEach ($fix in $fixes)
    {
        Add-Content -Path $changelogPath -Value $fix
    }
}
else
{
    Add-Content -Path $changelogPath -Value "N/A"
}
