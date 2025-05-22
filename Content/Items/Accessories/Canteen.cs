using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Items.Accessories;

public class Canteen : ModItem
{
    private static Asset<Texture2D> uiTexture;
    private int WaterType = 0;

    public static LocalizedText ForestText { get; private set; }
    public static LocalizedText CorruptionText { get; private set; }
    public static LocalizedText JungleText { get; private set; }
    public static LocalizedText HallowText { get; private set; }
    public static LocalizedText SnowText { get; private set; }
    public static LocalizedText DesertText { get; private set; }
    public static LocalizedText CavernText { get; private set; }
    public static LocalizedText BloodMoonText { get; private set; }
    public static LocalizedText CrimsomText { get; private set; }
    private static LocalizedText GetWaterName(int waterType)
    {
        return waterType switch
        {
            0 => ForestText,
            2 => CorruptionText,
            3 => JungleText,
            4 => HallowText,
            5 => SnowText,
            6 => DesertText,
            7 => CavernText,
            8 => CavernText,
            9 => BloodMoonText,
            10 => CrimsomText,
            12 => DesertText,
            _ => ForestText
        };
    }

    public override void Load()
    {
        uiTexture = ModContent.Request<Texture2D>("TritonsHydrants/Assets/UI/WaterIcon");
    }
    public override void SetStaticDefaults()
    {
        ForestText = this.GetLocalization("WaterName.Forest");
        CorruptionText = this.GetLocalization("WaterName.Corruption");
        JungleText = this.GetLocalization("WaterName.Jungle");
        HallowText = this.GetLocalization("WaterName.Hallow");
        SnowText = this.GetLocalization("WaterName.Snow");
        DesertText = this.GetLocalization("WaterName.Desert");
        CavernText = this.GetLocalization("WaterName.Cavern");
        BloodMoonText = this.GetLocalization("WaterName.BloodMoon");
        CrimsomText = this.GetLocalization("WaterName.Crimson");
    }

    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 20;
        Item.accessory = true;
        Item.rare = ItemRarityID.Blue;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        Main.waterStyle = WaterType;
    }

    public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        if (Item.wet)
        {
            WaterType = Water.GetWater(Main.waterStyle);
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Add(new TooltipLine(Mod, "WaterSyleDetails", $"Fill with {GetWaterName(WaterType).Value} water"));
    }

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        var backSourceRectangle = uiTexture.Frame(verticalFrames: 1, frameY: 0);
        var backOrigin = backSourceRectangle.Size();

        drawColor = Water.GetWaterColor(WaterType);

        spriteBatch.Draw(uiTexture.Value, position + new Vector2(17, -7), backSourceRectangle, drawColor, 0, backOrigin, scale, SpriteEffects.None, 1);

        return true;
    }
}