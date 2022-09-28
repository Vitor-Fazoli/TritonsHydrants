using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DevilsWarehouse.Common.Abstract;
using DevilsWarehouse.Content.Dusts;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class HammerGod : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hammer God");
            Tooltip.SetDefault("when hit a enemy with 10% of life, kill it");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HammerGodPlayer>().hammerGod = true;
        }
    }

    internal class HammerGodPlayer : ModPlayer
    {
        Vector2 speed = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f));
        readonly int dust = ModContent.DustType<Hammer>();
        public bool hammerGod;
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (hammerGod)
            {
                if (target.life <= target.lifeMax / 10 && !target.boss && item.DamageType == DamageClass.Melee)
                {
                    target.life = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        Dust.NewDust(target.Center, 1, 1,dust, speed.X,speed.Y);
                    }
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (hammerGod)
            {
                if (target.life <= target.lifeMax / 10 && !target.boss && proj.DamageType == DamageClass.Melee)
                {
                    target.life = 0;
                    for(int i =0; i < 5; i++)
                    {
                        Dust.NewDust(target.Center, 1, 1,dust,speed.X,speed.Y);
                    }
                }
            }
        }
        public override void ResetEffects()
        {
            hammerGod = false;
        }
    }
}
