using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Content.Buffs;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Content.Items.Weapons.Gushers;

[AutoloadEquip(EquipType.Back)]
public class StellarSiphon : GusherBase
{
    override protected int ManaCost => 25;
    override protected int BurstDamage => 490;
    override protected int BurstKnockback => 10;
    protected override int BuffType => ModContent.BuffType<Refreshed>();

    public override void SetDefaults()
    {
        Item.mana = 0;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.shootSpeed = 10f;
        Item.damage = BurstDamage;
        Item.knockBack = BurstKnockback;
        Item.noUseGraphic = false;
        Item.shoot = ModContent.ProjectileType<AquaBurst>();
        Item.DamageType = DamageClass.Summon;
        Item.buffType = 0;
        Item.UseSound = SoundID.Item21;
    }
    public override bool? UseItem(Player player)
    {
        return base.UseItem(player);
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse is 2)
        {
            if (player.statMana < Item.mana)
                return false;

            Item.mana = ManaCost;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 0.1f;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<Hydrant>();
            Item.noMelee = true;
            Item.buffType = ModContent.BuffType<HydrantBuff>();
            Item.UseSound = SoundID.Item25;
        }
        else
        {
            Item.mana = ManaCost;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.useAnimation = 50;
            Item.useTime = 50;
            Item.damage = BurstDamage;
            Item.knockBack = BurstKnockback;
            Item.noUseGraphic = false;
            Item.shoot = ModContent.ProjectileType<AquaBurst>();
            Item.buffType = 0;
            Item.UseSound = SoundID.Item21;
        }

        return base.CanUseItem(player);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (player.altFunctionUse is not 2)
        {
            Vector2 offset = new(velocity.X * 4, velocity.Y * 4);
            position += offset;
        }

        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-15, 0);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.FragmentStardust, 10)
            .AddIngredient(ItemID.LunarBar, 5)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }
}
