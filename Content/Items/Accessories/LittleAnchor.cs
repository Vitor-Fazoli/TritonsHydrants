using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Accessories;

public class LittleAnchor : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 17;
        Item.height = 19;

        Item.accessory = true;
        Item.rare = ItemRarityID.Pink;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<LittleAnchorPlayer>().isLittleAnchor = true;
    }
}

internal class LittleAnchorPlayer : ModPlayer
{
    private const int MAX_TIDE_POINTS = 150;
    private const int HIGH_TIDE_THRESHOLD = 50;
    private const int MIN_TIDE_POINTS = -150;
    private const int LOW_TIDE_THRESHOLD = -50;
    private int _tidePoints = 0;
    private bool _isHighTide = false;

    public bool isLittleAnchor = false;

    public override void ResetEffects() => isLittleAnchor = false;

    public override void UpdateBadLifeRegen()
    {
        if (!isLittleAnchor)
            return;

        if (_isHighTide)
            Player.AddBuff(ModContent.BuffType<Buffs.HighTide>(), 2);
        else
            Player.AddBuff(ModContent.BuffType<Buffs.LowTide>(), 2);

    }
    public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
        if (!isLittleAnchor)
            return;

        if (_tidePoints <= MAX_TIDE_POINTS)
            _tidePoints += 10;

        if (_tidePoints > HIGH_TIDE_THRESHOLD)
        {
            _isHighTide = true;
        }
    }
    public override void OnHitByProjectile(Projectile projectile, Player.HurtInfo hurtInfo)
    {
        if (!isLittleAnchor)
            return;


        if (_tidePoints <= MAX_TIDE_POINTS)
            _tidePoints += 10;

        if (_tidePoints > HIGH_TIDE_THRESHOLD)
        {
            _isHighTide = true;
        }
    }
    public override void OnHurt(Player.HurtInfo info)
    {
        if (!isLittleAnchor)
            return;

        if (_tidePoints >= MIN_TIDE_POINTS)
            _tidePoints -= 10;

        if (_tidePoints < LOW_TIDE_THRESHOLD)
        {
            _isHighTide = false;
        }
    }
}