using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using DevilsWarehouse.Content.Buffs.Vampire;

namespace DevilsWarehouse.Content.Mounts
{
    public class BatForm : ModMount
    {
        // Since only a single instance of ModMountData ever exists, we can use player.mount._mountSpecificData to store additional data related to a specific mount.
        // Using something like this for gameplay effects would require ModPlayer syncing, but this example is purely visual.
        protected class CarSpecificData
        {
            internal static float[] offsets = new float[] { 0, 14, -14 };

            internal int count; // Tracks how many balloons are still left.
            internal float[] rotations;

            public CarSpecificData()
            {
                count = 3;
                rotations = new float[count];
            }
        }

        public override void SetStaticDefaults()
        {
            // Movement
            MountData.jumpHeight = 5; // How high the mount can jump.
            MountData.acceleration = 0.05f; // The rate at which the mount speeds up.
            MountData.jumpSpeed = 4f; // The rate at which the player and mount ascend towards (negative y velocity) the jump height when the jump button is presssed.
            MountData.blockExtraJumps = false; // Determines whether or not you can use a double jump (like cloud in a bottle) while in the mount.
            MountData.constantJump = true; // Allows you to hold the jump button down.
            MountData.heightBoost = 20; // Height between the mount and the ground
            MountData.fallDamage = 0.5f; // Fall damage multiplier.
            MountData.runSpeed = 11f; // The speed of the mount
            MountData.dashSpeed = 8f; // The speed the mount moves when in the state of dashing.
            MountData.flightTimeMax = Readability.ToTicks(30); // The amount of time in frames a mount can be in the state of flying.
            // Misc
            MountData.fatigueMax = 0;
            MountData.buff = ModContent.BuffType<BatFormBuff>();
            
            
            // Frame data and player offsets
            MountData.totalFrames = 4;
            MountData.playerYOffsets = Enumerable.Repeat(20, MountData.totalFrames).ToArray();
            MountData.xOffset = 13;
            MountData.yOffset = -12;
            MountData.playerHeadOffset = 22;
            MountData.bodyFrame = 3;
            // Standing
            MountData.standingFrameCount = 4;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            // Running
            MountData.runningFrameCount = 4;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;
            // Flying
            MountData.flyingFrameCount = 4;
            MountData.flyingFrameDelay = 12;
            MountData.flyingFrameStart = 0;
            // In-air
            MountData.inAirFrameCount = 4;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 0;
            // Idle
            MountData.idleFrameCount = 4;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            // Swim
            MountData.swimFrameCount = MountData.inAirFrameCount;
            MountData.swimFrameDelay = MountData.inAirFrameDelay;
            MountData.swimFrameStart = MountData.inAirFrameStart;

            if (!Main.dedServ)
            {
                MountData.textureWidth = MountData.backTexture.Width() + 20;
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }

        public override void UpdateEffects(Player player)
        {
            

            // This code simulates some wind resistance for the balloons.
            var balloons = (CarSpecificData)player.mount._mountSpecificData;
            float ballonMovementScale = 0.05f;

            for (int i = 0; i < balloons.count; i++)
            {
                ref float rotation = ref balloons.rotations[i]; // This is a reference variable. It's set to point directly to the 'i' index in the rotations array, so it works like an alias here.

                if (Math.Abs(rotation) > MathHelper.PiOver2)
                    ballonMovementScale *= -1;

                rotation += -player.velocity.X * ballonMovementScale * Main.rand.NextFloat();
                rotation = rotation.AngleLerp(0, 0.05f);
            }

            // This code spawns some dust if we are moving fast enough.
            if (Math.Abs(player.velocity.X) > 4f)
            {
                Rectangle rect = player.getRect();

                Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.Adamantite);
            }
        }

        public override void SetMount(Player player, ref bool skipDust)
        {
            player.mount._mountSpecificData = new CarSpecificData();
        }
    }
}
