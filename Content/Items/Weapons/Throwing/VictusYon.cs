using DevilsWarehouse.Common;
using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace DevilsWarehouse.Content.Items.Weapons.Throwing
{
    public class VictusYon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victus and Yon");
        }
        public override void SetDefaults()
        {
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.damage = 10;
            Item.crit = 6;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<Victus>();
            Item.shootSpeed = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)//Sets what happens on right click(special ability)
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.damage = 10;
                Item.crit = 6;
                Item.rare = ItemRarityID.Red;
                Item.shoot = ModContent.ProjectileType<Yon>();
                Item.shootSpeed = 6;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.DamageType = DamageClass.Throwing;

            }
            else
            {
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 10;
                Item.crit = 6;
                Item.rare = ItemRarityID.Red;
                Item.shoot = ModContent.ProjectileType<Victus>();
                Item.shootSpeed = 6;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.DamageType = DamageClass.Throwing;
            }
            return base.CanUseItem(player);
        }
    }
    internal class Victus : ModProjectile
    {
        public int distance = 50;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Victus");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 24;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            Projectile.velocity *= 3;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player p = Main.player[Projectile.owner];
            
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Gold, speed * 2);
            }
        }
    }
    internal class Yon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yon");
        }
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.damage = 100;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (!target.friendly)
                {
                    Projectile.position = target.Center + new Vector2(0, -200);
                }
            }
        }
        public override void AI()
        {
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = -6;
        }
        public override void Kill(int timeLeft)
        {
            for(int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.Center, Projectile.width, 5, DustID.YellowStarDust, 3, -3);
                Dust.NewDustDirect(Projectile.Center, Projectile.width, 5, DustID.YellowStarDust, -3, -3);
            }
        }
    }
}