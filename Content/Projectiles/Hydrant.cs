using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Content.Buffs;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Projectiles
{
    public class Hydrant : ModProjectile
    {
        public int BuffType { get; set; }

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this proje
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 30;
            Projectile.damage = 0;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = TritonsHelper.Ticks(18000);
            Projectile.light = 0f;
            Projectile.scale = 1f;
            Projectile.minion = true;
            Projectile.netImportant = true;
            Projectile.minionSlots = 1;
            Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
        }
        public override bool? CanCutTiles()
        {
            return true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity += new Vector2(0, -1.5f);
            Projectile.damage = 0;

            BuffType = (int)Projectile.ai[0];
        }
        public override bool MinionContactDamage()
        {
            return false;
        }
        public override void AI()
        {
            CanisterPlayer owner = Main.player[Projectile.owner].GetModPlayer<CanisterPlayer>();
            Player pOwner = Main.player[Projectile.owner];


            if (!CheckActive(pOwner))
            {
                return;
            }

            AuraEffect(Projectile.Center, owner.AuraRadius);
            PlayersBuff(Projectile, owner.AuraRadius, pOwner);
            Gravity(Projectile);
        }
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<HydrantBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<HydrantBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }
        private static void Gravity(Projectile proj)
        {
            proj.velocity.Y += 0.1f;
            proj.rotation = 0;
        }
        private static void PlayersBuff(Projectile projectile, float auraSize, Player owner)
        {


            Player[] players = Main.player;

            Parallel.ForEach(players, player =>
            {
                if (Vector2.Distance(player.position, projectile.Center) < (int)auraSize)
                {
                    player.AddBuff((int)projectile.ai[0], 1, false);
                }
            });
        }
        private static void AuraEffect(Vector2 pos, float auraSize)
        {
            for (int i = 0; i < 10; i++)
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

    public class CanisterPlayer : ModPlayer
    {
        private const float AuraRadiusBase = 200;

        public float AuraRadius;
        private float _auraRadius = AuraRadiusBase;

        public override void PostUpdate()
        {
            AuraRadius = Player.GetDamage(DamageClass.Summon).ApplyTo(_auraRadius);
        }

        public override void ResetEffects()
        {
            _auraRadius = AuraRadiusBase;
        }
    }
}
