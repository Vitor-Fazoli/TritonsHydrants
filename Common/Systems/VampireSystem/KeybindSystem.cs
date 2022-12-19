using DevilsWarehouse.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common.Systems.VampireSystem
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind LifeDrainKeybind { get; private set; }

        public override void Load()
        {
            LifeDrainKeybind = KeybindLoader.RegisterKeybind(Mod, "LifeDrainKeybind", "q");
        }

        public override void Unload()
        {
            LifeDrainKeybind = null;
        }
    }
    public class KybindPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            int blood = Player.GetModPlayer<Vampire>().blood;
            int cost = 10;

            if (KeybindSystem.LifeDrainKeybind.JustPressed && blood >= cost)
            {
                Player.GetModPlayer<Vampire>().blood -= cost;
                Projectile.NewProjectileDirect(new EntitySource_TileBreak(2, 2), Player.position, Vector2.Zero, ModContent.ProjectileType<WarriorWraithProj>(), 0, 0, Player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item26, Player.position);
                Player.AddBuff(BuffID.Inferno, Readability.ToTicks(5));
            }
        }
    }
}
