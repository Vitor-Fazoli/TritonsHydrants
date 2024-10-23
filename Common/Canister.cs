using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Buffs;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Common
{
    public class Canister : ModProjectile
    {
        private float _auraSize = 250;
        
        public override void SetDefaults()
        {
            Projectile.width = 18; 
            Projectile.height = 40;
            Projectile.damage = 0;
            Projectile.aiStyle = 0; 
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.tileCollide = true; 
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1; 
            Projectile.timeLeft = Helper.Ticks(60);
            Projectile.light = 0f;
            Projectile.scale = 1f;
            Projectile.minion = true;
            Projectile.netImportant = true;
        }
        public override bool MinionContactDamage()
        {
            return false;
        }
        public override void AI()
        {
            _auraSize *= Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Multiplicative;
            
            AuraEffect(Projectile.Center, _auraSize);

            PlayersBuff(Projectile.Center, _auraSize);
            
            // Gravity
            Projectile.velocity.Y += 0.1f;

            Projectile.rotation = 0;
        }
        private static void PlayersBuff(Vector2 projPosition, float auraSize)
        {
            var players = Main.player;
            Parallel.ForEach(players, player =>
            {
                if (Vector2.Distance(player.position, projPosition) < (int)auraSize)
                {
                    player.AddBuff(ModContent.BuffType<HydroCanisterBoost>(), 1, false);
                }
            });
        }
        private static void AuraEffect(Vector2 pos, float auraSize)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 offset = Main.rand.NextVector2CircularEdge((int)auraSize, (int)auraSize);
                Dust d = Dust.NewDustPerfect(pos + offset, Main.rand.NextBool(4) ? 264 : 66, Vector2.Zero, Scale: 1.0f);
                d.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();

                d.noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}
