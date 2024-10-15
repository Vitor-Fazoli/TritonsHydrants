using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TritonsHydrants.Common;

public class CanisterBase : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 14; // Largura do canister
        Projectile.height = 20; // Altura do canister
        Projectile.aiStyle = 0; // Sem IA, vamos controlar a física manualmente
        Projectile.friendly = true; // Não causa dano ao jogador
        Projectile.hostile = false;
        Projectile.tileCollide = true; // Permite colisão com o chão e outros tiles
        Projectile.ignoreWater = true; // Ignora a física da água
        Projectile.penetrate = -1; // Não se destrói ao colidir
        Projectile.timeLeft = 18000; // Dura bastante tempo (5 minutos)
        Projectile.light = 0f; // Sem iluminação
        Projectile.scale = 1f; // Tamanho padrão
    }

    public override void AI()
    {
        // Se o projétil já colidiu com um tile, ele para de se mover e fica em pé
        if (Projectile.velocity.X == 0 && Projectile.velocity.Y == 0)
        {
            Projectile.position.Y = (float)(Math.Floor(Projectile.position.Y / 16) * 16); // Alinha no eixo Y com o tile
            Projectile.position.X = (float)(Math.Floor(Projectile.position.X / 16) * 16); // Alinha no eixo X com o tile
            Projectile.velocity = Vector2.Zero; // Zera a velocidade
            Projectile.netUpdate = true; // Sincroniza com outros jogadores (em multiplayer)
        }
        else
        {
            // Se ainda está no ar, reduz a velocidade e simula a física de queda suave
            Projectile.velocity.Y += 0.2f; // Gravidade leve até colidir com o chão
            if (Projectile.velocity.Y > 10f)
                Projectile.velocity.Y = 10f; // Limita a velocidade de queda
        }

        Projectile.rotation = 0f; // Não rotaciona o canister
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        // Quando colidir com um tile, o canister para imediatamente
        Projectile.velocity = Vector2.Zero; // Zera a velocidade para que ele não se mova mais
        return false; // Não destrua o projétil na colisão
    }
}
