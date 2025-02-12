using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common.Systems;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class Galacticite : BaseTridentItem
{
    public override void SetDefaults()
    {
        // Item settings
        Item.damage = 200;
        Item.knockBack = 4.5f;
        Item.mana = 20;
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(gold: 23);
        Item.shoot = ModContent.ProjectileType<Content.Projectiles.Tridents.Galacticite>();
        Item.useAnimation = 11;
        Item.useTime = 11;

        // Texture settings
        Item.width = 44;
        Item.height = 44;
        Item.scale = 1f;

        // Default Stats
        Item.shootSpeed = 11f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item71;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<NebulaSpike>(), 1)
            .AddIngredient(ItemID.LunarBar, 15)
            .AddIngredient(ItemID.FragmentNebula, 10)
            .AddIngredient(ItemID.FragmentSolar, 10)
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.FragmentVortex, 10)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }
}

