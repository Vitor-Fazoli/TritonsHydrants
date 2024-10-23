using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TritonsHydrants.Content.Items.Accessories
{
    public class BookOfKnowledge : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;   
            
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.defense = 15;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BookOfKnowledgePlayer>().BookOfKnowledge = true;
        }
    }
    public class BookOfKnowledgePlayer : ModPlayer
    {
        public bool BookOfKnowledge;
        private const int KnowledgeStatMax = 3;
        private int _knowledgeStat;
        
        private bool _knowledgePower;

        public override void ResetEffects()
        {
            BookOfKnowledge = false;
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            _knowledgeStat++;

            if (_knowledgeStat <= KnowledgeStatMax)
                return;
            
            _knowledgeStat = 0;
            _knowledgePower = true;
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!_knowledgePower || modifiers.DamageType != DamageClass.Magic)
                return;
            
            _knowledgePower = false;
            modifiers.FinalDamage *= 2f;
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!_knowledgePower || modifiers.DamageType != DamageClass.Magic)
                return;
            
            _knowledgePower = false;
            modifiers.FinalDamage *= 2f;
        }
        public override void PostUpdateMiscEffects()
        {
            switch (_knowledgeStat)
            {
                case 1:
                    Dust d1 = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.Firework_Blue, new Vector2(0,-Main.rand.Next(4)), Scale: 1f);
                    d1.noGravity = true;
                    break;
                case 2:
                    Dust d2 = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.Firework_Pink, new Vector2(0,-Main.rand.Next(4)), Scale: 1f);
                    d2.noGravity = true;
                    break;
                case 3:
                    Dust d3 = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.Firework_Red, new Vector2(0,-Main.rand.Next(4)), Scale: 1f);
                    d3.noGravity = true;
                    break;
            }
        }
    }
}