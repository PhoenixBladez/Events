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
			projectile.width = 16;
			projectile.height = 16;
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
		public float counter = -1440;
		public override void AI()
		{
			counter++;
			if (counter >= 1440)
			{
				counter = -1440;
			}
			{
				
				int num = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter)*8).RotatedBy(projectile.rotation), 8, 8, 226, 0.0f, 0.0f, 200, new Color(), 0.5f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= 1.4f;
								
				Main.dust[num].noGravity = true;
			}
		}
	}
}
