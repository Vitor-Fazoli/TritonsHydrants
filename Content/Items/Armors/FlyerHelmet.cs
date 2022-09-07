using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace VoidArsenal.Content.Items.Armors
{
	[AutoloadEquip(EquipType.Head)]
	public class FlyerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a modded hood.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 22; // Width of the item
			Item.height = 28; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Green; // The rarity of the item
			Item.defense = 4; // The amount of defense the item will give when equipped
		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		//public override bool IsArmorSet(Item head, Item body, Item legs)
		//{
		//	return body.type == ModContent.ItemType<ExampleBreastplate>() && legs.type == ModContent.ItemType<ExampleLeggings>();
		//}

		// UpdateArmorSet allows you to give set bonuses to the armor.
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "reduces mana cost by 10%";  // This is the setbonus tooltip
			player.manaCost -= 0.1f; // Reduces mana cost by 10%
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	}
}