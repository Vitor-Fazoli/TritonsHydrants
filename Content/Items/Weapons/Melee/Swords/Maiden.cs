using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace VoidArsenal.Content.Items.Weapons.Melee.Swords
{
    public class Maiden : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Maiden");
        }
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 80;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 14;
            Item.damage = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }
        //fazer uma evolução dessa arma para ser um acessório que te dá ela como summon
    }
}
