using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Tremor
{
	public class BoneArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone Arrow");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.penetrate = 1;
			aiType = ProjectileID.Bullet;
			projectile.melee = true;
			
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.Kill();
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 1);
		}
	   public override void Kill(int timeLeft)
		{
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 10, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= projectile.velocity * .5f;
  
                    Main.dust[num622].scale = 0.5f;
            
            }
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
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
