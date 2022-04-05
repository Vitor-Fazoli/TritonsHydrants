using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace GearonArsenal {
    public class PlayerFX : ModPlayer {

        public struct SoundData {
            public int Type;
            public int x;
            public int y;
            public int Style;
            public float volumeScale;
            public float pitchOffset;
            public SoundData(int Type) { this.Type = Type; x = -1; y = -1; Style = 1; volumeScale = 1f; pitchOffset = 0f; }
        }

        public static void ItemFlashFX(Player player, int dustType = 45, SoundData sDat = default(SoundData)) {
            if (sDat.Type == 0) { sDat = new SoundData(25); }
            if (player.whoAmI == Main.myPlayer) { SoundEngine.PlaySound(sDat.Type, sDat.x, sDat.y, sDat.Style, sDat.volumeScale, sDat.pitchOffset); }
            for (int i = 0; i < 5; i++) {
                int d = Dust.NewDust(
                    player.position, player.width, player.height, dustType, 0f, 0f, 255,
                    default(Color), (float)Main.rand.Next(20, 26) * 0.1f);
                Main.dust[d].noLight = true;
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f;
            }
        }


    }
}