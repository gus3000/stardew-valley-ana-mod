using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Characters;

namespace AnaMod;

public class ExampleMethods
{
    public static void SpawnGold()
    {
        const string itemId = StardewValley.Object.goldID;
        bool spawned = Game1.getLocationFromName("Farm").dropObject(
            new StardewValley.Object(itemId, 1),
            Game1.player.Position + Vector2.One * 100,
            Game1.viewport,
            true
        );
        if (spawned)
        {
            Game1.showGlobalMessage("spawning gold...");
        }
        else
        {
            Game1.showRedMessage("Cannot spawn gold");
        }
    }

    public static Junimo SpawnSprite()
    {
        var pos = Game1.player.Position + Vector2.One * 64;
        var junimo = new Junimo(pos, -1);
        Game1.currentLocation.addCharacter(junimo);
        return junimo;
    }
}