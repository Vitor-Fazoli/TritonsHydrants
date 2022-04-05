using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using GearonArsenal;
using Terraria.Audio;

namespace GearonArsenal {
    public class BorealWoodSabre : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Boreal Saber");
        }
        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 6; 
            Item.knockBack = 3f;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.useTime = 30 / 4;
            Item.useAnimation = 25;

            Item.rare = ItemRarityID.White;
            Item.value = 0;
        }

        public override void HoldItem(Player player) {
            if (ModSabres.HoldItemManager(player, Item, ModContent.ProjectileType<BorealWoodSabreSlash>(),
                 default(Color), 0.9f, player.itemTime == 0 ? 0f : 1f)) { player.AddBuff(BuffID.Archery, 100); }
        }

        public override void UseItemFrame(Player player) {
            ModSabres.UseItemFrame(player, 0.9f, Item.beingGrabbed);
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox) {
            int height = 70;
            int length = 72;
            ModSabres.UseItemHitboxCalculate(player, Item, ref hitbox, ref noHitbox, 0.9f, height, length);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) {
            Color colour = new Color(255, 89, 0, 119);
            ModSabres.OnHitFX(player, target, crit, colour);
        }
    }
    public class BorealWoodSabreSlash : ModProjectile {
        public static Texture2D specialSlash;
        public const int specialProjFrames = 5;
        bool sndOnce = true;
        int chargeSlashDirection = 1;
        public override void SetStaticDefaults() {
            Main.projFrames[Projectile.type] = specialProjFrames;
            //if (Main.netMode == 2) return;
            //specialSlash = mod.GetTexture("Items/Weapons/Sabres/" + GetType().Name + "_Special");
        }
        public override void SetDefaults() {
            Projectile.height = 70;
            Projectile.width = 72;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 60;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;
        }
        public override bool? CanCutTiles() { return false; }
        public int FrameCheck {
            get { return (int)Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }
        public int SlashLogic {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void AI() {
            Player player = Main.player[Projectile.owner];
            if (ModSabres.AINormalSlash(Projectile, SlashLogic)) { } else {

                ModSabres.AISetChargeSlashVariables(player, chargeSlashDirection);
                ModSabres.NormalSlash(Projectile, player);

                if (sndOnce) { SoundEngine.PlaySound(SoundID.Item5.WithVolume(0.5f), Projectile.Center); sndOnce = false; }
            }
            Projectile.damage = 0;
            Projectile.ai[0] += 1f; 
        }
        public bool PreDraw(SpriteBatch spriteBatch, ref Color lightColor) {
            Player player = Main.player[Projectile.owner];
            int weaponItemID = ModContent.ItemType<BorealWoodSabre>();
            Color lighting = Lighting.GetColor((int)(player.MountedCenter.X / 16), (int)(player.MountedCenter.Y / 16));
            return ModSabres.PreDrawSlashAndWeapon(spriteBatch, Projectile, weaponItemID, lighting,
                null,
                lighting,
                specialProjFrames,
                SlashLogic == 0f ? chargeSlashDirection : SlashLogic);
        }
    }
}