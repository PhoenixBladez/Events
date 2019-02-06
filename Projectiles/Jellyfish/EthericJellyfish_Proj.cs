using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Events.Projectiles.Jellyfish
{
    public class EthericJellyfish_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ethereal Mist");
        } 
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 1;
			projectile.timeLeft = 300;
        }
    public override void AI()
    {
        projectile.velocity.X = 0f;
        projectile.velocity.Y = 0f;
		Player player = Main.player[Main.myPlayer];
		if (player == Main.player[Main.myPlayer])
            {
                for (int index2 = 0; index2 < 1; ++index2)
				{
                    if (player.active && (double) Vector2.Distance(projectile.Center, player.Center) <= (double) 20f)
                    {		
                      player.AddBuff(BuffID.Suffocation, 200);
                    }
                }
            }
		{
          int index = Dust.NewDust(projectile.position, 128, 128, 108, 0.0f, 0.0f, 200, new Color(), 0.5f);
          Main.dust[index].noGravity = true;
          Main.dust[index].velocity *= 0.75f;
          Main.dust[index].fadeIn = 1.3f;
          Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
          vector2_1.Normalize();
          Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
          Main.dust[index].velocity = vector2_2;
          vector2_2.Normalize();
          Vector2 vector2_3 = vector2_2 * 34f;
          Main.dust[index].position = projectile.Center - vector2_3;
		  for (int dustNumber = 0; dustNumber < 3; dustNumber++)
					{
						Dust dust = Main.dust[Dust.NewDust(projectile.Left, projectile.width, projectile.height / 2, 173, 0f, 0f, 0, default(Color), 1f)];
						dust.position = projectile.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(projectile.width * 2.5f, projectile.height * 2.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
						dust.velocity.X = 0f;
						dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + projectile.velocity.Y - 4f) * 1f;
						dust.noGravity = true;
						dust.fadeIn = 1f;
						dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
					}
        }
	}
	public override void Kill(int timeLeft)
	{
          Main.PlaySound(SoundID.Item88, projectile.position);
    }
}
}