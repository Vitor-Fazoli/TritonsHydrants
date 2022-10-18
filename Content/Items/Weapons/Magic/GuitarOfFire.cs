using IL.Terraria;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Weapons.Magic
{
    public class GuitarOfFire : ModItem
    {
        private int note;
        public override void Load()
        {
            note = 0;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Guitar Of Fire");
        }
        public override void SetDefaults()
        {
            Item.shoot = ProjectileID.BallofFire;
            Item.shootSpeed = 6f;
            Item.useStyle = ItemUseStyleID.Guitar;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.LightRed;
            Item.damage = 55;
            Item.knockBack = 5.5f;
            Item.crit = 6;
            Item.mana = 17;
            Item.UseSound = SoundID.GuitarD;
        }
        public override bool CanUseItem(Terraria.Player player)
        {
            switch (note)
            {
                case 0:
                    Item.UseSound = SoundID.GuitarD;
                    note++;
                    break;
                case 1:
                    Item.UseSound = SoundID.GuitarD;
                    note++;
                    break;
                case 2:
                    Item.UseSound = SoundID.GuitarEm;
                    note++;
                    break;
                case 3:
                    Item.UseSound = SoundID.GuitarF;
                    note++;
                    break;
                case 4:
                    Item.UseSound = SoundID.GuitarF;
                    note++;
                    break;
            }

            if (note >= 4)
            {
                note = 0;
            }

            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FlowerofFire, 1)
                .AddIngredient(ItemID.Wood, 100)
                .AddIngredient(ItemID.LeafWand, 1)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddTile(TileID.Hellforge)
                .Register();
        }
    }
}
