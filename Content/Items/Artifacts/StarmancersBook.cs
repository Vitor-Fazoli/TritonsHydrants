using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace GearonArsenal.Content.Items.Artifacts
{
    public class StarmancersBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starmancer's Book");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.starCloakItem.active = true;
            ModContent.GetInstance<Starmancer>().StarActive = true;
        }
    }
    public class Starmancer : ModPlayer
    {
        public bool StarActive = false;
    }
    public class StarWeapons : GlobalItem
    {
        public override bool AllowPrefix(Item item, int pre)
        {
            return ModContent.GetInstance<Starmancer>().StarActive;
        }
    }
    public class Stellar : ModPrefix
    {
        public override void Apply(Item item)
        {
            item.damage += item.damage / 5; //20%
            item.crit += 30; //30%
            item.shootSpeed += 5f;
            item.rare = ItemRarityID.Red; //custom rare
        }
        public override bool CanRoll(Item item)
        {
            return ModContent.GetInstance<Starmancer>().StarActive;
        }
    }
}
