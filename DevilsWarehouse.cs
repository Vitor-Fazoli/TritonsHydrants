using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace DevilsWarehouse
{
	public class DevilsWarehouse : Mod
	{
        public static DevilsWarehouse Instance { get; set; }

        public DevilsWarehouse()
        {
            Instance = this;
        }
        public override void Load()
		{
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> screenRef = new(Instance.Assets.Request<Effect>("Effects/Shockwave", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();
            }
        }
        public override void Unload()
        {
            if (!Main.dedServ)
            {
                Instance = null;
            }
        }
    }
}