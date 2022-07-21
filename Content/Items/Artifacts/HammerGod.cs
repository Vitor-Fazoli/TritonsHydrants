using Terraria;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Artifacts
{
    internal class HammerGod : ModItem
    {
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HammerGodP>().hammerGod = true;
        }
    }

    internal class HammerGodP : ModPlayer
    {
        public bool hammerGod;
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (hammerGod)
            {
                if (target.life <= target.lifeMax / 10 && !target.boss && item.DamageType == DamageClass.Melee)
                {
                    target.life = 0;
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
                }
            }
        }
    }
}
