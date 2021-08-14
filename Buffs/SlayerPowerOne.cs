using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Buffs
{
	public class SlayerPowerOne : ModBuff
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Slayer Power");
			Description.SetDefault("Increase your critical chance in greatswords");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			CanBeCleared = false;
		}
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Melee) += 10;
		}
    }
}
