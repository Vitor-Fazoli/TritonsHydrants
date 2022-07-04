using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace GearonArsenal.Common.Abstract
{
    public abstract class Artifact : ModItem
    {
        protected string name;
        protected string tooltip;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(name);
            Tooltip.SetDefault(tooltip);

            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.CanGetPrefixes[Item.type] = false;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactR>();
            Item.accessory = true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }
        public override bool AllowPrefix(int pre)
        {
            return false;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            bool ascend = true;

            if (Item.alpha >= 255)
            {
                ascend = false;
            }
            if (Item.alpha <= 80)
            {
                ascend = true;
            }

            if (ascend)
            {
                Item.alpha++;
            }
            else
            {
                Item.alpha--;
            }
        }
    }
    internal class ArtifactR : ModRarity
    {
        public override string Name => "Artifact";
        public override Color RarityColor => new(255, 255, 255, 0);
    }
}
