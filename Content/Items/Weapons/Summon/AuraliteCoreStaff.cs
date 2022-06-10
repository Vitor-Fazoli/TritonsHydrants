using GearonArsenal.Content.Buffs;
using GearonArsenal.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GearonArsenal.Content.Items.Weapons.Summon
{
	public class AuraliteCoreStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a purity wisp to fight for you.");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 110;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 26;
			Item.height = 28;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<WanderingAuralite>();
			Item.buffType = ModContent.BuffType<WanderingAuraliteBuff>(); //The buff added to player after used the Item
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}
	}
}
