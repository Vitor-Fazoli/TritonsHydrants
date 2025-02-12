using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Common.Systems;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class UnholyTrident : BaseTridentItem
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
        Item.shoot = ModContent.ProjectileType<Projectiles.Tridents.UnholyTrident>();
    }
}