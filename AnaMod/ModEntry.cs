using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Extensions;
using Object = StardewValley.Object;

namespace AnaMod;

internal sealed class ModEntry : Mod
{
    private readonly Random _random = new();
    private ModConfig Config { get; set; } = new();

    public override void Entry(IModHelper helper)
    {
        helper.Events.Input.ButtonPressed += OnButtonPressed;
        helper.Events.Content.AssetRequested += OnAssetRequested;
        helper.Events.GameLoop.DayStarted += OnDayStarted;
        // helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        helper.Events.World.ObjectListChanged += OnObjectListChanged;
    }

    private void Log(string message, LogLevel logLevel = LogLevel.Debug)
    {
        Monitor.Log(message, logLevel);
    }

    private void OnObjectListChanged(object? sender, ObjectListChangedEventArgs e)
    {
        // Log($"Object list changed : {e}");
        // foreach (var o in e.Added)
        // {
        //     Log($"Added object : {o.Value.Type} at pos {o.Key}");
        // }
        foreach (var o in e.Removed)
            // Log($"Removed object : {o.Value.Type} at pos {o.Key}");
            if (o.Value.Category == Object.litterCategory)
            {
                Log($"removed litter : {o.Value.Name} at {o.Key}");
                if (Game1.random.NextBool(0.01)) ExampleMethods.SpawnKorogu(o.Key * 64);
            }
    }


    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/mail")) e.Edit(EditImpl);
        if (e.Name.IsEquivalentTo("Portraits/Korogu")) e.LoadFromModFile<Texture2D>("assets/korogu-portraits.png", AssetLoadPriority.Medium);
    }

    public void EditImpl(IAssetData asset)
    {
        var data = asset.AsDictionary<string, string>().Data;

        data["AnaMod-Mail-1"] =
            "Ma chérie, ^Tu travailles dur à la ferme, prends une pause pour manger ce petit fromage < ^^  - ton chéri %item object 424 1%%";
    }

    private void OnDayStarted(object? sender, DayStartedEventArgs e)
    {
        Game1.addMailForTomorrow("AnaMod-Mail-1");
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (!Context.IsWorldReady || Game1.currentLocation == null)
            return;

        if (_random.Next(1000) == 0) Game1.showGlobalMessage("<! - le chéri");

        if (e.Button == SButton.Space) ExampleMethods.SpawnKorogu(Game1.player.Position + Vector2.One * 64);
    }
}