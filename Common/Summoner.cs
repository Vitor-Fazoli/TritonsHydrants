using Terraria.ModLoader;

namespace DevilsWarehouse.Common
{
	public class Summoner : ModPlayer
	{
		public bool auraliteMinion;

		public override void ResetEffects()
		{
			auraliteMinion = false;
		}
	}
}