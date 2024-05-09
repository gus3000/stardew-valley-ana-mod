using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewValley;

namespace AnaMod;

public class Korogu : NPC
{
    public Korogu(Vector2 position) : base(new AnimatedSprite("Characters\\Junimo", 0, 16, 16), position, 2, nameof(Korogu))
    {
        SetRandomColor();
        Breather = false;
        forceUpdateTimer = 9999;
        Scale = 0.75f;
    }

    private NetColor _color = new();

    public override bool IsVillager => false;

    public override void update(GameTime time, GameLocation location)
    {
        base.update(time, location);
        Sprite.Animate(time, 0, 8, 50f);

        // facePlayer(Game1.MasterPlayer);
        // this.flip = Game1.random.NextBool();
    }

    public override void draw(SpriteBatch b, float alpha = 1f)
    {
        if (IsInvisible)
            return;
        Sprite.UpdateSourceRect();
        b.Draw(Sprite.Texture,
            getLocalPosition(Game1.viewport) +
            new Vector2(Sprite.SpriteWidth * 4 / 2,
                (float)(Sprite.SpriteHeight * 3.0 / 4.0 * 4.0 / Math.Pow(Sprite.SpriteHeight / 16, 2.0) + yJumpOffset -
                        8.0)) + (shakeTimer > 0 ? new Vector2(Game1.random.Next(-1, 2), Game1.random.Next(-1, 2)) : Vector2.Zero),
            Sprite.SourceRect, _color.Value, rotation,
            new Vector2(Sprite.SpriteWidth * 4 / 2, (float)(Sprite.SpriteHeight * 4 * 3.0 / 4.0)) / 4f,
            Math.Max(0.2f, scale.Value) * 4f, flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            Math.Max(0.0f, drawOnTop ? 0.991f : StandingPixel.Y / 10000f));
    }

    protected override void initNetFields()
    {
        base.initNetFields();
        NetFields.AddField(_color);
    }

    public override bool checkAction(Farmer who, GameLocation l)
    {
        Game1.showGlobalMessage("Coucou !");
        return true;
    }

    public void SetRandomColor()
    {
        _color.Value = getRandomColor();
    }

    private Color getRandomColor()
    {
        if (Game1.random.NextDouble() < 0.01)
            switch (Game1.random.Next(8))
            {
                case 0:
                    return Microsoft.Xna.Framework.Color.Red;
                case 1:
                    return Microsoft.Xna.Framework.Color.Goldenrod;
                case 2:
                    return Microsoft.Xna.Framework.Color.Yellow;
                case 3:
                    return Microsoft.Xna.Framework.Color.Lime;
                case 4:
                    return new Color(0, byte.MaxValue, 180);
                case 5:
                    return new Color(0, 100, byte.MaxValue);
                case 6:
                    return Microsoft.Xna.Framework.Color.MediumPurple;
                case 7:
                    return Microsoft.Xna.Framework.Color.Salmon;
            }

        switch (Game1.random.Next(8))
        {
            case 0:
                return Microsoft.Xna.Framework.Color.LimeGreen;
            case 1:
                return Microsoft.Xna.Framework.Color.Orange;
            case 2:
                return Microsoft.Xna.Framework.Color.LightGreen;
            case 3:
                return Microsoft.Xna.Framework.Color.Tan;
            case 4:
                return Microsoft.Xna.Framework.Color.GreenYellow;
            case 5:
                return Microsoft.Xna.Framework.Color.LawnGreen;
            case 6:
                return Microsoft.Xna.Framework.Color.PaleGreen;
            case 7:
                return Microsoft.Xna.Framework.Color.Turquoise;
        }

        return Microsoft.Xna.Framework.Color.White;
    }
}