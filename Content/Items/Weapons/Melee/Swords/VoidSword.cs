using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace VoidArsenal.Content.Items.Weapons.Melee.Swords
{
    public class VoidSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Espada do Vazio");
            Tooltip.SetDefault("Uma Espada forjada no Vazio");
        }
        public override void SetDefaults()
        {
            Item.width = 78;
            Item.height = 76;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 10;
            Item.damage = 1500;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }
    }
}