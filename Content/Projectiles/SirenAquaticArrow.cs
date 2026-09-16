using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using TritonsHydrants.Utils;

namespace TritonsHydrants.Content.Projectiles;

public class SirenAquaticArrow : AquaticArrow
{
    public override string Texture => "TritonsHydrants/Content/Projectiles/AquaticArrow";

    public override void SetDefaults()
    {
        base.SetDefaults();
        // The texture is transparent; collision follows the visible dust core.
        Projectile.width = 12;
        Projectile.height = 12;
        Projectile.scale = 1.6f;
        Projectile.npcProj = true;
        Projectile.timeLeft = 300;
    }

    // NPC arrows have no player owner or biome-dependent player bonuses.
    public override void OnSpawn(IEntitySource source) { }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) { }

    public override Color? GetAlpha(Color lightColor) => Color.Cyan;

    public override void AI()
    {
        int targetIndex = (int)Projectile.ai[0] - 1;
        if (targetIndex < 0 || targetIndex >= Main.maxNPCs || !Main.npc[targetIndex].CanBeChasedBy(Projectile))
        {
            targetIndex = -1;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                float nearestDistance = 600f * 600f;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC target = Main.npc[i];
                    float distance = Vector2.DistanceSquared(Projectile.Center, target.Center);
                    if (target.CanBeChasedBy(Projectile) && distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        targetIndex = i;
                    }
                }

                if (Projectile.ai[0] != targetIndex + 1)
                {
                    Projectile.ai[0] = targetIndex + 1;
                    Projectile.netUpdate = true;
                }
            }
        }

        if (targetIndex >= 0)
        {
            Vector2 desiredVelocity = (Main.npc[targetIndex].Center - Projectile.Center)
                .SafeNormalize(Vector2.UnitY) * 8f;
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 0.15f);
        }

        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
        if (!Main.dedServ)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = Projectile.Center - Projectile.velocity * (i / 3f)
                    + Main.rand.NextVector2Circular(7f, 7f);
                Dust dust = Dust.NewDustPerfect(position, TritonsDusts.GetWaterDust(), -Projectile.velocity * 0.2f,
                    newColor: Water.GetColor(), Scale: Main.rand.NextFloat(0.9f, 1.2f));
                dust.noGravity = true;
            }

            Lighting.AddLight(Projectile.Center, 0f, 0.5f, 0.65f);
        }
    }

    public override void OnKill(int timeLeft)
    {
        if (Main.dedServ)
            return;

        for (int i = 0; i < 32; i++)
        {
            Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2f, 5f);
            Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.BlueTorch, velocity,
                newColor: Color.Cyan, Scale: Main.rand.NextFloat(1.8f, 2.6f));
            dust.noGravity = true;
        }
    }
}
