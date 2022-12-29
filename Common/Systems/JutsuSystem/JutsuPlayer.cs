using DevilsWarehouse.Common.Abstract;
using DevilsWarehouse.Content.Projectiles;
using DevilsWarehouse.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DevilsWarehouse.Common.Systems.JutsuSystem
{
    public class JutsuPlayer : ModPlayer
    {
        public bool jutsu = false;
        public int chakraCurrent;
        public int defaultChakraMax = 250;
        public int chakraMax;
        public int chakraMax2;
        public float chakraRegenRate;
        public int chakraRegenTimer;
        public bool criticalHappen;

        private int timer = 0;

        public override void Initialize()
        {
            chakraMax = defaultChakraMax;
            criticalHappen = false;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }
        private void ResetVariables()
        {
            chakraRegenRate = 1f;
            chakraMax2 = chakraMax;
            criticalHappen = false;
        }
        public override void PostUpdateMiscEffects()
        {
            UpdateResource();
        }
        private void UpdateResource()
        {
            if (!Player.HasBuff<NinjaScrollBuff>())
            {
                Player.AddBuff(ModContent.BuffType<NinjaScrollBuff>(), 2);
                Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), Player.position, Vector2.Zero, ModContent.ProjectileType<NinjaScroll>(), 10, 5, Player.whoAmI);
            }


            chakraRegenTimer++;

            if (Player.statLife != Player.statLifeMax2)
            {
                if (chakraRegenTimer > 80 / chakraRegenRate)
                {
                    chakraCurrent += 1;
                    chakraRegenTimer = 0;
                }
            }
            else
            {
                if (chakraRegenTimer > 5 / chakraRegenRate)
                {
                    chakraCurrent += 1;
                    chakraRegenTimer = 0;
                }
            }

            chakraCurrent = Utils.Clamp(chakraCurrent, 0, chakraMax2);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is Jutsu)
            {
                if (crit)
                {
                   criticalHappen= true;
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is Jutsu)
            {
                if (crit && Player.ownedProjectileCounts[ModContent.ProjectileType<NinjaScroll>()] > 0)
                {
                    var p = Main.projectile[ModContent.ProjectileType<NinjaScroll>()];

                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), p.position, p.velocity, ModContent.ProjectileType<Harmony>(), 10, 5, Player.whoAmI);
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is Jutsu)
            {
                if (crit && Player.ownedProjectileCounts[ModContent.ProjectileType<NinjaScroll>()] > 0)
                {
                    var p = Main.projectile[ModContent.ProjectileType<NinjaScroll>()];

                    Projectile.NewProjectile(new EntitySource_TileBreak(2, 2), p.position, p.velocity, ModContent.ProjectileType<Harmony>(), 10, 5, Player.whoAmI);
                }
            }
        }
        public override void PostUpdate()
        {
            timer++;

            if (timer >= 80)
            {
                criticalHappen = false;
                timer = 0;
            }
        }
    }
}
