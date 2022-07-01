using Terraria.ModLoader;

namespace GearonArsenal.Common
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