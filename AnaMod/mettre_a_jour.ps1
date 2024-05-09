$gameDefaultLocations = "C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley", "D:\SteamLibrary\steamapps\common\Stardew Valley"
$releaseZipTempDir = "/tmp/AnaModTmp/"

##### Si vous êtes utilisateur de ce script rien modifier en dessous de cette ligne
$gameLocation = $null
foreach ($path in $gameDefaultLocations)
{
    if (Test-Path -Path $path -PathType Container)
    {
        $gameLocation = $path
        break
    }
}

if ($gameLocation -eq $null)
{
    Write-Output "Dossier du jeu non trouvé, veuillez le rajouter aux chemins possibles au début du script SVP"
    exit -1
}

if ( [string]::IsNullOrEmpty($releaseZipTempDir))
{
    Write-Output "Il faut un répertoire temporaire, je peux pas bosser moi sinon"
    exit -1
}

if (Test-Path -Path $releaseZipTempDir -PathType Container)
{
    Write-Output "Veuillez vérifier que le répertoire $releaseZipTempDir ne contient rien d'important, et supprimez-le svp"
    exit -1
}

mkdir -Force $releaseZipTempDir > $null
$headers = @{
    'Accept' = 'application/vnd.github+json'
    'X-GitHub-Api-Version' = '2022-11-28'
}
Write-Output "Récupération des infos de la dernière version..."
$latestReleaseInfo = Invoke-RestMethod -Headers $headers -uri https://api.github.com/repos/gus3000/stardew-valley-ana-mod/releases/latest

$latestReleaseVersion = $latestReleaseInfo.tag_name
Write-Output "Dernière version : $latestReleaseVersion"
$releaseArtifactsUrl = $latestReleaseInfo.assets.browser_download_url

$releaseZipUrl = $null
foreach ($url in $releaseArtifactsUrl)
{
    Write-Output "on check $url"
    if ($url -match "AnaMod.*.zip")
    {
        $releaseZipUrl = $url
    }
}

if ($releaseZipUrl -eq $null)
{
    Write-Output "Archive du mod non trouvée :("
    exit -1
}

Write-Output "Téléchargement de $releaseZipUrl..."
$releaseZipTempLocation = "$releaseZipTempDir/anamod.zip"
Invoke-WebRequest $releaseZipUrl -OutFile $releaseZipTempLocation
Expand-Archive $releaseZipTempLocation -DestinationPath $releaseZipTempDir

$modsFolder = "$gameLocation/Mods"
$AnaModFolder = "$modsFolder/AnaMod"

if (Test-Path -Path $AnaModFolder -PathType Container)
{
    rm -Recurse $AnaModFolder
}

copy -Recurse "$releaseZipTempDir/AnaMod" -Destination $modsFolder

rm -Recurse $releaseZipTempDir