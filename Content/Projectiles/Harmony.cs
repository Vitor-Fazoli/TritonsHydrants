using DevilsWarehouse.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Projectiles
{
    public class Harmony : ModProjectile
    {
        private int count = 0;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Throwing; 
            Projectile.friendly = true;
            Projectile.hostile = false; 
            Projectile.ignoreWater = true; 
            Projectile.light = 1f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {

            if (Projectile.velocity == Vector2.Zero)
            {
                count++;
            }
            if (count == 30)
            {
                Projectile.Kill();
            }

            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(Projectile.position - new Vector2(2f, 2f), Projectile.width, Projectile.height, DustID.AncientLight, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.White, 1.2f);
                Main.dust[dust].noGravity = true;
            }

            float maxDetectRadius = 400f; 
            float projSpeed = 5f; 

            NPC closestNPC = Helper.FindClosestNPC(maxDetectRadius, this.Projectile);
            if (closestNPC == null)
                return;

            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(0.5f, 0.5f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.AncientLight, speed * 5);
                d.noGravity = true;

                Lighting.AddLight(Projectile.position, d.color.R / 255, d.color.G / 255, d.color.B / 255);
            }
        }
    }
}
