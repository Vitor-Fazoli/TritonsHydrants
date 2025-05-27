using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common.Systems;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class NebulaSpike : BaseTridentItem
{
    public override void SetDefaults()
    {
        // Item settings
        Item.damage = 156;
        Item.knockBack = 6.5f;
        Item.mana = 5;
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(silver: 10);
        Item.shoot = ModContent.ProjectileType<Projectiles.Tridents.NebulaSpike>();
        Item.useAnimation = 31;
        Item.useTime = 31;

        // Texture settings
        Item.width = 44;
        Item.height = 44;
        Item.scale = 1f;

        // Default Stats
        Item.shootSpeed = 9f;
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
            .AddIngredient(ItemID.FragmentNebula, 21)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }
}