using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Characters;
using Object = StardewValley.Object;

namespace AnaMod;

public class ExampleMethods
{
    public static void SpawnGold()
    {
        const string itemId = Object.goldID;
        var spawned = Game1.getLocationFromName("Farm").dropObject(
            new Object(itemId, 1),
            Game1.player.Position + Vector2.One * 100,
            Game1.viewport,
            true
        );
        if (spawned)
            Game1.showGlobalMessage("spawning gold...");
        else
            Game1.showRedMessage("Cannot spawn gold");
    }

    public static Junimo SpawnSprite()
    {
        var pos = Game1.player.Position + Vector2.One * 64;
        var junimo = new Junimo(pos, -1);
        Game1.currentLocation.addCharacter(junimo);
        return junimo;
    }
}