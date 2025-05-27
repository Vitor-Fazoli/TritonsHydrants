using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
        Item.shoot = ModContent.ProjectileType<Projectiles.Tridents.Galacticite>();
        Item.useAnimation = 17;
        Item.useTime = 17;

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

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 offset = new(velocity.X * 3, velocity.Y * 3);
        position += offset;

        const int MINIMUM_RADIAN = -20;
        int radians = 20;
        int amountProjectiles = 2;

        for (int i = MINIMUM_RADIAN; i < radians * amountProjectiles; i += radians)
        {
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(i));
            Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
        }

        return base.Shoot(player, source, position, velocity, type, damage, knockback);
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

