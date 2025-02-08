using TritonsHydrants.Content.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using TritonsHydrants.Content.Buffs;

namespace TritonsHydrants.Common
{
    public class TitaniumTridentModification
    {
        /// <summary>
        /// 
        /// </summary>
        public class TitaniumTridentProjectile : GlobalProjectile
        {
            public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.TitaniumTrident;
            public override void SetDefaults(Projectile projectile)
            {
                projectile.DamageType = DamageClass.Magic;
            }
            public override void AI(Projectile projectile)
            {
                if (projectile.timeLeft <= projectile.timeLeft / 3)
                {
                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), new Vector2(projectile.Center.X, projectile.Center.Y), projectile.velocity * 1.5f, ModContent.ProjectileType<AquaticArrow>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class TitaniumTridentItem : GlobalItem
        {
            public override bool AppliesToEntity(Item entity, bool lateInstantiation)
            {
                return entity.type == ItemID.TitaniumTrident;
            }
            public override void SetDefaults(Item item)
            {
                item.DamageType = DamageClass.Magic;
                item.mana = 35;
                item.UseSound = SoundID.Item71;

                item.StatsModifiedBy.Add(Mod);
            }

            public override void HoldItem(Item item, Player player)
            {
                player.AddBuff(ModContent.BuffType<WaterAffinity>(), 2);
            }
        }
    }
}
