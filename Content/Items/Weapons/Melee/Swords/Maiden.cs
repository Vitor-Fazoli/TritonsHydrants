using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using DevilsWarehouse.Content.Projectiles;
using System;

namespace DevilsWarehouse.Content.Items.Weapons.Melee.Swords
{
    public class Maiden : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maiden");
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 14;
            Item.damage = 30;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<MaidenProj>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 6; // shoots 6 projectiles
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new((float)(player.position.X + player.width * 0.5 + (Main.rand.Next(201) * -player.direction) + (Main.mouseX + Main.screenPosition.X - player.position.X)), 
                    (float)(player.position.Y + player.height * 0.5 - 600.0));
                vector2_1.X = (float)((vector2_1.X + player.Center.X) / 2.0) + Main.rand.Next(-200, 201);
                vector2_1.Y -= (100 * index);
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(-40, 41) * 0.02f;
                float SpeedY = num17 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<MaidenProj>(), damage, knockback, Main.myPlayer, 0, Main.rand.Next(5)); ;
            }
            return false;
        }
    }
}
