using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Weapons.Throwing.Hatchets
{
    public class CopperHatchet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Hatchet");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.useTime = 16;
            Item.useAnimation = 16;

            Item.damage = 6;
            Item.knockBack = 8;
            Item.crit = 6;

            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<CopperHatchetP>();
            Item.shootSpeed = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
            Item.consumable = true;
        }
        public override bool AllowPrefix(int pre)
        {
            return false;
        }
        public override void AddRecipes()
        {
              CreateRecipe(200)
                .AddIngredient(ItemID.CopperBar)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    internal class CopperHatchetP : ModProjectile
    {
        public override string Texture => base.Texture.RemoveEnd(1);
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Hatchet");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 2;
        }
        public override void AI()
        {
            Projectile.ai[0]++;

            Projectile.rotation += 0.4f * Projectile.direction;
            Projectile.spriteDirection = Projectile.direction;

            if (Projectile.ai[0] >= Helper.Ticks(0.3f))
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
            }
            else if (Projectile.velocity.Y >= 16)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.8f;
            }
        }
        public override void Kill(int timeLeft)
        {
            const int MAX_DUST = 10;

            for (int i = 0; i < MAX_DUST; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Copper, speed * 5);
                d.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
