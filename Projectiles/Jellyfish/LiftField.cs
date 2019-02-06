using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Events.Projectiles.Jellyfish
{
    public class LiftField : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lift Field");
        } 
        public override void SetDefaults()
        {
            projectile.width = 50;
            projectile.height = 50;
            projectile.aiStyle = 1;
			projectile.timeLeft = 300;
        }
    public override void AI()
    {
        projectile.velocity.X = 0f;
        projectile.velocity.Y = 0f;
		Player player = Main.player[Main.myPlayer];
			for (int index3 = 0; index3 < 100; ++index3)
				{
					NPC npc = Main.npc[index3];
					if (Main.npc[index3].Hitbox.Intersects(projectile.Hitbox) && npc.life >= 1 && !npc.boss)
					{
						npc.velocity.Y -= 1;
					    npc.velocity.Y *= 1.3f;
						Main.PlaySound(SoundID.Item88, projectile.position);
						int d = Dust.NewDust(npc.position, npc.width, npc.height, 70, 0.0f, 0.0f, 200, new Color(), 0.5f);						
					}
				}
		{
          int index = Dust.NewDust(projectile.position, 128, 128, 173, 0.0f, 0.0f, 200, new Color(), 0.5f);
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
						Dust dust = Main.dust[Dust.NewDust(projectile.Left, 20, 20, 173, 0f, 0f, 0, default(Color), 1f)];
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