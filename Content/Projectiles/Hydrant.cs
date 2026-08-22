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
            Projectile.netUpdate = true;
        }
        public override bool MinionContactDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player pOwner = Main.player[Projectile.owner];
            CanisterPlayer owner = pOwner.GetModPlayer<CanisterPlayer>();

            // CheckActive só roda no cliente dono
            if (Main.myPlayer == Projectile.owner)
            {
                if (!CheckActive(pOwner))
                    return;
            }
            else
            {
                // Outros clientes só checam se o dono está vivo e ativo
                if (pOwner.dead || !pOwner.active)
                    return;
            }

            AuraEffect(Projectile, owner.AuraRadius);
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
        private static void AuraEffect(Projectile proj, float auraSize)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 offset = Main.rand.NextVector2CircularEdge((int)auraSize, (int)auraSize);
                Dust d = Dust.NewDustPerfect(proj.position + offset, Main.rand.NextBool(4) ? 264 : 66, Vector2.Zero, Scale: 1.0f);
                d.color = Main.rand.NextBool() ? Color.Lerp(Water.GetWaterColor(), Color.White, 0.5f) : Water.GetWaterColor();
                d.noGravity = true;
            }

            foreach(var player in Main.player)
            {
                if (!proj.active || proj.type != ModContent.ProjectileType<Hydrant>())
                    continue;

                float dist = Vector2.Distance(player.Center, proj.Center);

                if (dist < auraSize)
                    player.AddBuff((int)proj.ai[0], 2, false);
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
