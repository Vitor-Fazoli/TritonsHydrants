using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using static Terraria.ModLoader.Core.TmodFile;
using System.Reflection;
using Terraria.ModLoader.Core;
using System.Linq;

namespace VoidArsenal
{
	public class VoidArsenal : Mod
	{
        public static VoidArsenal Instance { get; set; }

        public VoidArsenal()
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