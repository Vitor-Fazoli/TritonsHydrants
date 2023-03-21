using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace DevilsWarehouse.Content.Items.Weapons.Melee.Swords
{
    public class BaseballBat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Minha Arma");
            Tooltip.SetDefault("Rebate projéteis ao atacar.");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.mana = 10;
        }
        public override void OnConsumeMana(Player player, int manaConsumed)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.hostile && !proj.friendly && proj.getRect().Intersects(Item.getRect()))
                {
                    proj.velocity = -proj.velocity;
                    proj.friendly = true;
                    proj.hostile = false;
                    CombatText.NewText(player.getRect(), Color.Red, 10, true);
                }
            }
        }
    }
}
