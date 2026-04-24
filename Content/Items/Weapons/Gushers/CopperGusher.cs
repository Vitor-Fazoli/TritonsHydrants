using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Content.Buffs;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Content.Items.Weapons.Gushers;

[AutoloadEquip(EquipType.Back)]
public class CopperGusher : GusherBase
{
    override protected int ManaCost => 10;
    override protected int BurstDamage => 20;
    override protected int BurstKnockback => 6;
    protected override int BuffType => ModContent.BuffType<Refreshed>();
    protected override int MaxChargeTicks => 90;
    protected override float MaxDamageMultiplier => 3f;

    public override void SetDefaults()
    {
        Item.mana = 0;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.shootSpeed = 10f;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.channel = true;          // mantém channel ativo enquanto segura
        Item.noUseGraphic = true;     // esconde o item — o held proj desenha a arma
        Item.damage = BurstDamage;
        Item.knockBack = BurstKnockback;
        Item.noUseGraphic = true;
        Item.shoot = ModContent.ProjectileType<AquaBurst>(); // fallback, sobrescrito no Shoot
        Item.DamageType = DamageClass.Summon;
        Item.buffType = 0;
        Item.UseSound = SoundID.Item21;
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
            Item.channel = false;
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
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.channel = true;
            Item.damage = BurstDamage;
            Item.knockBack = BurstKnockback;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<AquaBurst>();
            Item.buffType = 0;
            Item.UseSound = SoundID.Item21;
        }

        return base.CanUseItem(player);
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (player.altFunctionUse is not 2)
            target.AddBuff(BuffID.Slow, 500);

        base.OnHitNPC(player, target, hit, damageDone);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity,
        ref int type, ref int damage, ref float knockback)
    {
        if (player.altFunctionUse is not 2)
        {
            Vector2 offset = new(velocity.X * 4, velocity.Y * 4);
            position += offset;
        }
        base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
    }

    public override Vector2? HoldoutOffset() => new Vector2(-15, 0);

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 10)
            .AddIngredient(ItemID.Wood, 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}