using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TritonsHydrants.Common;
using TritonsHydrants.Content.Projectiles;

namespace TritonsHydrants.Content.Items.Weapons.Tridents;

public class Galacticite : TridentBaseItem
{
    public override void SetDefaults()
    {
        // Item settings
        Item.damage = 200;
        Item.knockBack = 4.5f;
        Item.mana = 20;
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(gold: 23);
        Item.shoot = ModContent.ProjectileType<GalacticiteProj>();
        Item.useAnimation = 11;
        Item.useTime = 11;

        // Texture settings
        Item.width = 44;
        Item.height = 44;
        Item.scale = 1f;

        // Default Stats
        Item.shootSpeed = 11f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item71;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<NebulaSpike>(), 1)
            .AddIngredient(ItemID.LunarBar, 10)
            .AddIngredient(ItemID.FragmentNebula, 8)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
    }

    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }

    public override bool? UseItem(Player player)
    {
        if (!Main.dedServ && Item.UseSound.HasValue)
        {
            SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
        }

        return null;
    }
}

public class GalacticiteProj : TridentBaseProj
{
    protected override float HoldoutRangeMax => 150f;
    protected override int Proj => ModContent.ProjectileType<AquaBurst>();
    public override void SetDefaults()
    {
        Projectile.width = 15;
        Projectile.height = 15;
        Projectile.aiStyle = ProjAIStyleID.Spear;
        Projectile.penetrate = -1;
        Projectile.scale = 1.3f;
        Projectile.alpha = 0;
        Projectile.hide = true;
        Projectile.ownerHitCheck = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
    }
}