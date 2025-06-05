using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common.Systems;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class MeteorFork : BaseTridentItem
{

    public override void SetDefaults()
    {
        // Item settings
        Item.damage = 25;
        Item.knockBack = 6.5f;
        Item.mana = 9;
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(silver: 10);
        Item.shoot = ModContent.ProjectileType<Projectiles.Tridents.MeteorFork>();
        Item.useAnimation = 31;
        Item.useTime = 31;

        // Texture settings
        Item.width = 44;
        Item.height = 44;
        Item.scale = 1f;

        // Default Stats
        Item.shootSpeed = 10f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item71;
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        MeteorForkPlayer p = player.GetModPlayer<MeteorForkPlayer>();
        p.AddMeteorStack();
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.MeteoriteBar, 25)
            .AddTile(TileID.Anvils)
            .Register();
    }
}

public class MeteorForkPlayer : ModPlayer
{
    private static Asset<Texture2D> OrbTexture;
    public int meteorStacks = 0;
    public int maxMeteorStacks = 5;
    public int stackTimer = 0;
    public List<Vector2> orbPositions = [];

    override public void Load()
    {
        OrbTexture = ModContent.Request<Texture2D>("TritonsHydrants/Assets/UI/WaterIcon");
    }

    public override void ResetEffects()
    {
        if (meteorStacks > 0)
        {
            stackTimer--;
            if (stackTimer <= 0)
            {
                meteorStacks = 0;
                orbPositions.Clear();
            }
        }
    }

    public override void PostUpdate()
    {
        if (meteorStacks > 0)
        {
            float angleStep = MathHelper.TwoPi / meteorStacks;
            orbPositions.Clear();
            for (int i = 0; i < meteorStacks; i++)
            {
                float angle = angleStep * i + Main.GameUpdateCount * 0.05f;
                Vector2 offset = new Vector2(30, 0).RotatedBy(angle);
                orbPositions.Add(Player.Center + offset);
            }

            ApplyBuffs();
        }
    }

    private void ApplyBuffs()
    {
        Player.GetAttackSpeed(DamageClass.Melee) += 0.05f * meteorStacks;
        if (meteorStacks >= 2)
            Player.GetDamage(DamageClass.Melee) += 0.05f;
        if (meteorStacks >= 3)
            Player.GetCritChance(DamageClass.Melee) += 10;
        if (meteorStacks >= 4)
            Player.AddBuff(BuffID.Shine, 2); // efeito visual temporário
        if (meteorStacks == 5)
            Player.endurance += 0.1f; // ignora 10% do dano como “defesa extra”
    }

    public void AddMeteorStack()
    {
        if (meteorStacks < maxMeteorStacks)
        {
            meteorStacks++;
        }
        stackTimer = 600; // dura 10 segundos
    }

    public override void PostUpdateMiscEffects()
    {
        if (meteorStacks > 0)
        {
            foreach (var pos in orbPositions)
            {
                Main.spriteBatch.Draw(
                    (Texture2D)OrbTexture,
                    pos - Main.screenPosition,
                    null,
                    Color.Lime * 0.8f,
                    0f,
                    OrbTexture.Size() * 0.5f,
                    0.5f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
