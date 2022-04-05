using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace GearonArsenal {
    internal class Studies : ModMenu {


        public override int Music => 30;
    }

    internal class test : ModPrefix {

        public override string Name => "Berserker";

        public override PrefixCategory Category => PrefixCategory.Melee;

        public override float RollChance(Item item) {
            return 0.5f;
        }
    }
    internal class Artifact : ModRarity {
        public override string Name => "Artifact";
        public override Color RarityColor => new(240,0,Main.DiscoB,Main.DiscoB);

    }
}
