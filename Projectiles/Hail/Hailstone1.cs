using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Hail
{
	public class Hailstone1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hailstone");
		 ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.timeLeft = 6000;
			projectile.CloneDefaults(ProjectileID.WoodenArrowHostile);
			projectile.height = 14;
			projectile.penetrate = 3;
		}
		public override bool PreAI()
		{
			projectile.rotation += .3f;
			return true;
		}
		int dust1 = 0;
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
						 Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 48  );
				int d = 51;
				for (int k = 0; k < 6; k++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
				projectile.Kill();
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
					projectile.velocity.X = -oldVelocity.X;

				if (projectile.velocity.Y != oldVelocity.Y)
					projectile.velocity.Y = -oldVelocity.Y;

				projectile.velocity *= 0.15f;
			}
			return false;
		}
		public override void Kill(int timeLeft)
		{
			 Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 50);
				int d = 51;
				for (int k = 0; k < 6; k++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.White, 0.7f);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
	}
}