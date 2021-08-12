using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Buffs
{
	public class slayerPower2 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slayer Power");
			Description.SetDefault("Multiplies your damage with greatswords");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			CanBeCleared = false;
		}
	}
}
