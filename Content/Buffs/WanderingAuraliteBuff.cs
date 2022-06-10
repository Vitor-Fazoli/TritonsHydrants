using GearonArsenal.Common.Systems;
using Terraria;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Buffs
{
	public class WanderingAuraliteBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wandering Auralite");
			Description.SetDefault("The purity wisp will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Summoner p = player.GetModPlayer<Summoner>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.WanderingAuralite>()] > 0)
			{
				p.WanderingAuraliteMinion = true;
			}
			if (!p.WanderingAuraliteMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}