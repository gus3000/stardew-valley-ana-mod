using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace AnaMod;

internal sealed class ModEntry : Mod
{
    private ModConfig Config { get; set; } = new ModConfig();

    private Random _random = new Random();

    public override void Entry(IModHelper helper)
    {
        helper.Events.Input.ButtonPressed += OnButtonPressed;
        helper.Events.Content.AssetRequested += OnAssetRequested;
        helper.Events.GameLoop.DayStarted += OnDayStarted;
        // helper.Events.GameLoop.GameLaunched += OnGameLaunched;
    }

    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/mail"))
        {
            e.Edit(EditImpl);
        }
    }

    public void EditImpl(IAssetData asset)
    {
        var data = asset.AsDictionary<string, string>().Data;

        data["AnaMod-Mail-1"] =
            "Ma chérie, ^Tu travailles dur à la ferme, prends une pause pour manger ce petit fromage < ^^  - ton chéri %item object 424 1%%";

        // // "MyModMail1" is referred to as the mail Id.  It is how you will uniquely identify and reference your mail.
        // // The @ will be replaced with the player's name.  Other items do not seem to work (''i.e.,'' %pet or %farm)
        // // %item object 388 50 %%   - this adds 50 pieces of wood when added to the end of a letter.
        // // %item tools Axe Hoe %%   - this adds tools; may list any of Axe, Hoe, Can, Scythe, and Pickaxe
        // // %item money 250 601  %%  - this sends a random amount of gold from 250 to 601 inclusive.
        // // For more details, see: https://stardewvalleywiki.com/Modding:Mail_data 
        // data["AnaModMail1"] = "Hello @... ^A single carat is a new line ^^Two carats will double space.";
        // data["AnaModMail2"] = "This is how you send an existing item via email! %item object 388 50 %%";
        // data["AnaModMail3"] = "Coin $   Star =   Heart <   Dude +  Right Arrow >   Up Arrow `";
        // data["AnaModMail4Wizard"] = "Include Wizard in the mail Id to use the special background on a letter";
    }

    private void OnDayStarted(object? sender, DayStartedEventArgs e)
    {
        Game1.addMailForTomorrow("AnaMod-Mail-1");
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
        if (!Context.IsWorldReady || Game1.currentLocation == null)
            return;

        if (_random.Next(1000) == 0)
        {
            Game1.showGlobalMessage("<! - le chéri");
        }
        // Monitor.Log($"Pressed button {e.Button}", LogLevel.Debug);
        // switch (e.Button)
        // {
        //     case SButton.NumPad0:
        //         Game1.chatBox.addMessage("<<<<<<", Color.White);
        //         break;
        //     case SButton.NumPad1:
        //         Game1.player.mailbox.Add("AnaMod-Mail-1");
        //         break;
        //     // case SButton.NumPad2:
        //     //     Game1.player.mailbox.Add("AnaModMail2");
        //     //     break;
        //     // case SButton.NumPad3:
        //     //     Game1.player.mailbox.Add("AnaModMail3");
        //     //     break;
        //     // case SButton.NumPad4:
        //     //     Game1.player.mailbox.Add("AnaModMail4Wizard");
        //     //     break;
        //     case SButton.NumPad5:
        //         ShowLove();
        //         break;
        // }
    }
}