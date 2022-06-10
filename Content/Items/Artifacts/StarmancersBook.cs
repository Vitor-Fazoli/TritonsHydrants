using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace GearonArsenal.Content.Items.Artifacts
{
    internal class StarmancersBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.starCloakItem.active = true;
        }
    }
    public class Starmancer : ModPlayer
    {
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
           // item.DamageType = Terraria.ModLoader.ModContent.GetInstance<>()
        }
    }
}
