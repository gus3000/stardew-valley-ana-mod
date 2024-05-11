using Microsoft.Xna.Framework;
using StardewValley;
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

    public static Korogu SpawnKorogu(Vector2 pos)
    {
        var korogu = new Korogu(pos);
        Game1.currentLocation.addCharacter(korogu);
        return korogu;
    }
}