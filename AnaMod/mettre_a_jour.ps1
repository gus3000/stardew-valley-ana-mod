$gameLocation = "C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley"
#$gameLocation = "D:\SteamLibrary\steamapps\common\Stardew Valley"
$releaseZipTempDir = "/tmp/AnaModTmp/"

if ([string]::IsNullOrEmpty($releaseZipTempDir))
{
    Write-Output "Il faut un répertoire temporaire, je peux pas bosser moi sinon"
    exit -1
}

if(Test-Path -Path $releaseZipTempDir -PathType Container) {
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
$releaseZipUrl = $latestReleaseInfo.assets.browser_download_url

$releaseZipTempLocation = "$releaseZipTempDir/anamod.zip"
Invoke-WebRequest $releaseZipUrl -OutFile $releaseZipTempLocation
Expand-Archive $releaseZipTempLocation -DestinationPath $releaseZipTempDir

$modsFolder = "$gameLocation/Mods"
$AnaModFolder = "$modsFolder/AnaMod"

if(Test-Path -Path $AnaModFolder -PathType Container)
{
    rm -Recurse $AnaModFolder
}

copy -Recurse "$releaseZipTempDir/AnaMod" -Destination $modsFolder

rm -Recurse $releaseZipTempDir