using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace GearonArsenal
{
    internal class Artifact : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(240, 0, Main.DiscoB / 2, Main.DiscoB / 2);
    }
}
