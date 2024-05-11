using StardewValley;

namespace AnaMod;

public class ModUpdater
{
    private const string ModFolder = @"D:\SteamLibrary\steamapps\common\Stardew Valley\Mods";
    private const string ModName = "AnaMod";

    // private const string ModPath = $"{ModFolder}/{ModName}";
    
    // private const string ModPath = @"D:\SteamLibrary\steamapps\common\Stardew Valley\Mods\AnaMod"; //TODO change this
    private string _modPath;
    // private const string BackupPath = $"{ModFolder}/{BackupFolderName}";
    private const string BackupPath = @"C:\tmp\Backup\StardewMods\AnaMod";

    public ModUpdater(string modPath)
    {
        _modPath = modPath;
        ModEntry.Log($"mod path : {_modPath}");
    }

    public void UpdateMod()
    {
        BackupCurrent();
        // DownloadNew();
        // AskForRestart();
    }

    private void BackupCurrent()
    {
        DeleteOldBackup();

        Directory.CreateDirectory(BackupPath);
        // Directory.Move(ModPath, BackupPath);
    }

    private void DeleteOldBackup()
    {
        if (!Directory.Exists(BackupPath))
            return;
        Directory.Delete(BackupPath, true);
    }

    private void DownloadNew()
    {
    }

    private void AskForRestart() //TODO maybe restart by itself ?
    {
    }
}