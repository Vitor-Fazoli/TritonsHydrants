using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class UnholyTrident : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 38;
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 18;
        Item.useTime = 30;
        Item.shootSpeed = 3.7f;
        Item.knockBack = 6.5f;
        Item.width = 44;
        Item.height = 44;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(silver: 10);

        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.mana = 10;
        Item.shoot = ModContent.ProjectileType<UnholyTridentProj>();
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}

public class UnholyTridentProj : TridentBase
{
    protected override float HoldoutRangeMax => 120f;
    public override void SetDefaults()
    {

        Projectile.width = 15;
        Projectile.height = 15;
        Projectile.aiStyle = ProjAIStyleID.Spear;
        Projectile.penetrate = -1;
        Projectile.scale = 1.3f;
        Projectile.alpha = 0;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
    }
}