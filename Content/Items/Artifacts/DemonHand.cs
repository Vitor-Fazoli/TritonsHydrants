//using Terraria.ModLoader;
//using Terraria;
//using DevilsWarehouse.Common.Abstract;
//using System.Collections.Generic;
//using System;
//using Terraria.ID;
//using Microsoft.Xna.Framework;
//using Terraria.DataStructures;
//using Terraria.GameContent.Creative;

//namespace DevilsWarehouse.Content.Items.Artifacts
//{
//    public class DemonHand : Artifact
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Demon Hand");
//            ItemID.Sets.CanGetPrefixes[Item.type] = false;
//            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
//        }
//        public override void SetDefaults()
//        {
//            Item.value = Item.sellPrice(gold: 1);
//            Item.rare = ModContent.RarityType<ArtifactRarity>();
//            Item.accessory = true;
//            Item.buffType = ModContent.BuffType<DemonHandBuff>();
//        }
//        public override void UpdateAccessory(Player player, bool hideVisual)
//        {
//            player.AddBuff(Item.buffType, 2);

//        }
//    }
//    public class DemonHandBuff : ModBuff
//    {
//        private bool hasDemon = false;
//        public override void SetStaticDefaults()
//        {
//            Main.buffNoSave[Type] = true;
//            Main.buffNoTimeDisplay[Type] = true;
//        }
//        public override void Update(Player player, ref int buffIndex)
//        {
//            if (!hasDemon)
//            {
//                Projectile.NewProjectile(new EntitySource_Buff(player, Type, buffIndex),
//                    Main.MouseScreen,
//                    Vector2.Zero,
//                    ModContent.ProjectileType<DemonHandProjectile>(), 0, 0);

//                hasDemon = true;
//            }

//            if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonHandProjectile>()] > 0)
//            {
//                player.buffTime[buffIndex] = 18000;
//            }
//            else
//            {
//                hasDemon = false;
//                player.DelBuff(buffIndex);
//                buffIndex--;
//            }
//        }
//    }
//    public class DemonHandProjectile : ModProjectile
//    {
//        public override void SetStaticDefaults()
//        {
//            DisplayName.SetDefault("Demon Hand");
//        }
//    }
//}
