using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Buffs
{
	public class slayerPower2 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slayer Power");
			Description.SetDefault("Increase your critical chance in greatswords");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			CanBeCleared = false;
		}
	}
}
