using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using GearonArsenal.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using GearonArsenal.Common.Abstract;

namespace GearonArsenal.Content.Items.Artifacts
{
    internal class SteroidSyringe : Artifact
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steroid Serynge");
            Tooltip.SetDefault("each critical damage you drop two Supplies for you and allies spend all of your mana\n" +
                "defense increased\n" +
                "mana regen decreased");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<ArtifactR>();
            Item.accessory = true;
            Item.defense = 5;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<BattleMedic>().battleMedic = true;
            player.manaRegen -= 5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SharkFin, 10)
                .AddIngredient(ItemID.HealingPotion, 5)
                .AddRecipeGroup(RecipeGroupID.IronBar, 30)
                .AddIngredient(ItemID.WizardHat)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
    internal class BattleMedic : ModPlayer
    {
        public bool battleMedic = false;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.DamageType == DamageClass.Ranged && battleMedic && crit)
            {
                if (Player.statMana >= Player.statManaMax2)
                {
                    Player.statMana -= Player.statManaMax2;

                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X + 100, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SupplyCrate>(), 1);


                    Item.NewItem(new EntitySource_DropAsItem(default), new Vector2(Player.Center.X - 100, Player.Center.Y - 10), new Vector2(
                        0, -5), ModContent.ItemType<SupplyCrate>(), 1);
                }
            }
        }
    }
}
