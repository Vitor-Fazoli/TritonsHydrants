using Terraria;
using Terraria.ModLoader;

namespace GreatswordsMod.Buffs
{
	public class slayerPower2 : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slayer Power");
<<<<<<< HEAD
			Description.SetDefault("Increase your critical chance in greatswords");
=======
			Description.SetDefault("Multiplies your damage with greatswords");
>>>>>>> 337fab235ffe1f67df12154b5ced31d62c4d2c99
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
			CanBeCleared = false;
		}
	}
}
