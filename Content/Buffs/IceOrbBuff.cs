using DevilsWarehouse.Content.Items.Weapons.Summon;
using DevilsWarehouse.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Buffs
{
    public class IceOrbBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Orb");
            Description.SetDefault("Ice Orb will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceOrb>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
