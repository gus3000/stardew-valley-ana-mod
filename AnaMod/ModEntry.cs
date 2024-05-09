using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;

namespace AnaMod;

internal sealed class ModEntry : Mod
{
    private Junimo? _junimo;

    private readonly Random _random = new();
    private ModConfig Config { get; set; } = new();

    public override void Entry(IModHelper helper)
    {
        helper.Events.Input.ButtonPressed += OnButtonPressed;
        helper.Events.Content.AssetRequested += OnAssetRequested;
        helper.Events.GameLoop.DayStarted += OnDayStarted;
        // helper.Events.GameLoop.GameLaunched += OnGameLaunched;
    }

    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/mail")) e.Edit(EditImpl);
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

        if (e.Button == SButton.Space)
            // ExampleMethods.SpawnGold();
            if (_junimo == null)
                _junimo = ExampleMethods.SpawnSprite();
    }
}