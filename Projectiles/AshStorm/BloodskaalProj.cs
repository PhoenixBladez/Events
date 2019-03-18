using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.AshStorm
{
    public class BloodskaalProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodskaal Blade");
			            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
			projectile.melee = true;
			projectile.tileCollide = true;
			projectile.alpha = 150;
            projectile.timeLeft = 300;
            projectile.height = 22;
            projectile.width = 40;
            aiType = ProjectileID.Bullet;
        }
		   public override void Kill(int timeLeft)
		   {
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 235, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
  
                    Main.dust[num622].scale = 0.5f;
            
            }
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
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			projectile.ai[1] += 1f;
			if (projectile.ai[1] >= 7200f)
			{
				projectile.alpha += 5;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
					projectile.Kill();
				}
			}

			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] >= 10f)
			{
				projectile.localAI[0] = 0f;
				int num416 = 0;
				int num417 = 0;
				float num418 = 0f;
				int num419 = projectile.type;
				for (int num420 = 0; num420 < 1000; num420++)
				{
					if (Main.projectile[num420].active && Main.projectile[num420].owner == projectile.owner && Main.projectile[num420].type == num419 && Main.projectile[num420].ai[1] < 3600f)
					{
						num416++;
						if (Main.projectile[num420].ai[1] > num418)
						{
							num417 = num420;
							num418 = Main.projectile[num420].ai[1];
						}
					}
				}
				if (num416 > 3)
				{
					Main.projectile[num417].netUpdate = true;
					Main.projectile[num417].ai[1] = 36000f;
					return;
				}
			}
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}