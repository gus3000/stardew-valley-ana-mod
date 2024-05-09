using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewValley;
using StardewValley.Extensions;
using Object = StardewValley.Object;

namespace AnaMod;

public class Korogu : NPC
{
    private readonly NetColor _color = new();
    private readonly NetFloat _alpha = new(1f);
    private readonly NetFloat _alphaChange = new();
    private bool _interacted;

    public Korogu(Vector2 position) : base(new AnimatedSprite("Characters\\Junimo", 0, 16, 16), position, 2, nameof(Korogu))
    {
        SetRandomColor();
        Breather = false;
        forceUpdateTimer = 9999;
        Scale = 0.75f;
        _interacted = false;
    }

    public override bool IsVillager => false;

    public override void update(GameTime time, GameLocation location)
    {
        base.update(time, location);
        Sprite.Animate(time, 0, 8, 50f);
        handleAlpha();
        // facePlayer(Game1.MasterPlayer);
        // this.flip = Game1.random.NextBool();
    }

    private void handleAlpha()
    {
        _alpha.Value += _alphaChange.Value;
        if (_alpha.Value < 0)
        {
            _alpha.Value = 0;
            IsInvisible = true;
            HideShadow = true;
            
        }
    }

    public override void draw(SpriteBatch b, float alpha = 1f)
    {
        if (IsInvisible)
            return;
        Sprite.UpdateSourceRect();
        b.Draw(Sprite.Texture,
            getLocalPosition(Game1.viewport) +
            new Vector2(Sprite.SpriteWidth * 4 / 2,
                (float)(Sprite.SpriteHeight * 3.0 / 4.0 * 4.0 / Math.Pow(Sprite.SpriteHeight / 16, 2.0) + yJumpOffset - 8.0)) +
            (shakeTimer > 0 ? new Vector2(Game1.random.Next(-1, 2), Game1.random.Next(-1, 2)) : Vector2.Zero), Sprite.SourceRect,
            _color.Value * this._alpha.Value, rotation, new Vector2(Sprite.SpriteWidth * 4 / 2, (float)(Sprite.SpriteHeight * 4 * 3.0 / 4.0)) / 4f,
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
        if (_interacted)
            return false;
        _interacted = true;
        l.localSound("junimoMeep1");
        Game1.multipleDialogues(new[] { "Yahaha ! Tu m'as trouvé !", "Tiens, prends ma graine !" });

        Game1.afterDialogues += () =>
        {
            Game1.player.addItemByMenuIfNecessary(randomGift());
            FadeAway();
        };
        return true;
    }

    private Object randomGift()
    {
        const string ACORN = "309";
        const string MAPLE_SEED = "310";
        const string PINE_CONE = "311";

        string[] gifts = { ACORN, MAPLE_SEED, PINE_CONE };
        var gift = Game1.random.Choose(gifts);
        return new Object(gift, 1);
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
                    return Color.Red;
                case 1:
                    return Color.Goldenrod;
                case 2:
                    return Color.Yellow;
                case 3:
                    return Color.Lime;
                case 4:
                    return new Color(0, byte.MaxValue, 180);
                case 5:
                    return new Color(0, 100, byte.MaxValue);
                case 6:
                    return Color.MediumPurple;
                case 7:
                    return Color.Salmon;
            }

        switch (Game1.random.Next(8))
        {
            case 0:
                return Color.LimeGreen;
            case 1:
                return Color.Orange;
            case 2:
                return Color.LightGreen;
            case 3:
                return Color.Tan;
            case 4:
                return Color.GreenYellow;
            case 5:
                return Color.LawnGreen;
            case 6:
                return Color.PaleGreen;
            case 7:
                return Color.Turquoise;
        }

        return Color.White;
    }

    public void FadeAway()
    {
        collidesWithOtherCharacters.Value = false;
        _alphaChange.Value = -0.015f;
    }
}