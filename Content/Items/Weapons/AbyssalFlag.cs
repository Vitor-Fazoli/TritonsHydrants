using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Weapons
{
    public class AbyssalFlag : ModItem
    {
        // Altura do pico da parábola (em pixels) acima do ponto mais alto entre origem e alvo. Ajuste para mudar o arco.
        private const float ArcHeight = 250f;

        public override void SetDefaults() {
            // Common Properties
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 999;
            // Use Properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;
            // Weapon Properties
            Item.damage = 33;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            // Projectile Properties
            Item.shoot = ModContent.ProjectileType<Projectiles.AbyssalFlag>();
            // shootSpeed removido: velocidade agora é calculada em Shoot() para acertar exatamente o mouse.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Vector2 target = Main.MouseWorld;
            float gravity = Projectiles.AbyssalFlag.Gravity;

            // Pico da parábola: ArcHeight acima do mais alto entre origem e destino (Y menor = mais alto).
            float peakY = Math.Min(position.Y, target.Y) - ArcHeight;

            float timeUp = (float)Math.Sqrt(2f * (position.Y - peakY) / gravity);
            float timeDown = (float)Math.Sqrt(2f * (target.Y - peakY) / gravity);
            float totalTime = timeUp + timeDown;

            Vector2 throwVelocity = new Vector2(
                (target.X - position.X) / totalTime,
                -gravity * timeUp
            );

            Projectile.NewProjectile(source, position, throwVelocity, type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}