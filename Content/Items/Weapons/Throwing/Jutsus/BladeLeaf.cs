using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using DevilsWarehouse.Common.Abstract;
using DevilsWarehouse.Common.Systems.JutsuSystem;

namespace DevilsWarehouse.Content.Items.Weapons.Throwing.Jutsus
{
    public class BladeLeaf : Jutsu
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade Leaf");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 34;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing; 
            Item.noMelee = true; 
            Item.knockBack = 2;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Leaf; 
            Item.shootSpeed = 7; 
            Item.noUseGraphic = true;
            chakraCost = 6;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }
    }
}