using DevilsWarehouse.Common.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace DevilsWarehouse.Content.Items.Artifacts
{
    public class GraniteEye : Artifact
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Granite Eye");
            Tooltip.SetDefault("Summon and Ranged Damage now works together on all weapons with any damage type\n" +
                "Ammo inflicts its debuff through minions, bullets and arrows work together");
            ItemID.Sets.CanGetPrefixes[Item.type] = false;
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ModContent.RarityType<ArtifactRarity>();
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GraniteEyePlayer>().graniteEye = true;
        }
    }
    public class GraniteEyePlayer : ModPlayer
    {
        public bool graniteEye = false;

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (graniteEye)
            {
                if (item.DamageType == DamageClass.Summon || item.DamageType == DamageClass.Ranged)
                {
                    damage = Player.GetDamage(DamageClass.Summon).
                        CombineWith(Player.GetDamage(DamageClass.Ranged)
                        );
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (graniteEye)
            {
                if (proj.DamageType == DamageClass.Summon)
                {
                    #region Arrows
                    if (Player.HasItem(ItemID.CursedArrow))
                    {
                        target.AddBuff(BuffID.CursedInferno, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.IchorArrow))
                    {
                        target.AddBuff(BuffID.Ichor, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.VenomArrow))
                    {
                        target.AddBuff(BuffID.Venom, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.FrostburnArrow))
                    {
                        target.AddBuff(BuffID.Frostburn, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.FlamingArrow))
                    {
                        target.AddBuff(BuffID.OnFire, Helper.Ticks(5));
                    }
                    #endregion

                    #region Bullets
                    if (Player.HasItem(ItemID.CursedBullet))
                    {
                        target.AddBuff(BuffID.CursedInferno, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.IchorBullet))
                    {
                        target.AddBuff(BuffID.Ichor, Helper.Ticks(5));
                    }
                    else if (Player.HasItem(ItemID.VenomBullet))
                    {
                        target.AddBuff(BuffID.Venom, Helper.Ticks(5));
                    }
                    #endregion
                }
            }
        }
        public override void ResetEffects()
        {
            graniteEye = false;
        }
    }
}