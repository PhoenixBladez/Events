using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Lightning
{
	public class Volt : ModProjectile
	{
		private bool init = false;
		Vector2 initialVel = Vector2.Zero;
		public static int _type;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Beam");
		}

		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			aiType = ProjectileID.Bullet;
			projectile.alpha = 255;
			projectile.timeLeft = 300;
			projectile.penetrate = -1;
			projectile.magic = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(4) == 0)
			{
				target.AddBuff(BuffID.Electrified, 180, true);
			}
		}
		public override void AI()
		{
			if (!init)
			{
				initialVel = projectile.velocity;
				init = true;
			}
			projectile.velocity = Vector2.Zero;
			projectile.Center = projectile.Center + initialVel.RotatedByRandom(MathHelper.ToRadians(30));

			for (int i = 0; i < 5; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 8f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 8f * (float)i;
				int num = Dust.NewDust(new Vector2(x, y), 6, 6, 226);
				Main.dust[num].velocity = Vector2.Zero;
				Main.dust[num].scale *= .5f;
									Main.dust[num].velocity*=0.4f;
				Main.dust[num].noGravity = true;
			}
		}
	}
}
