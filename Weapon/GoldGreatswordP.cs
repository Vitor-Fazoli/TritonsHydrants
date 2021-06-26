using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GreatswordsMod.Slash;

namespace GreatswordsMod.Weapon
{
    public class GoldGreatswordP : Greatsword
    {
        public override void SetDefaults()
        {
            projectile.width = 0;
            projectile.height = 0;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 124;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates = 1;

            dmg = 6;
            cooldown = 45;
        } 
    }
}
