using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.AcidRain
{
	public class AcidRain : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acid Rain");
		}

		public override void SetDefaults()
		{
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.height = 30;
			projectile.width = 3;
			aiType = ProjectileID.Bullet;
			projectile.extraUpdates = 1;

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		if(Main.rand.Next(2) == 0)
			target.AddBuff(mod.BuffType("Acid"), 7200);
			target.AddBuff(BuffID.Poisoned, 600);
		}

		public override void Kill(int timeLeft)
		{
			int num624 = Dust.NewDust(projectile.position, projectile.width, projectile.height,
				107, 0f, 0f, 100, default(Color), 3f);
			Main.dust[num624].velocity = Vector2.Zero;
			Main.dust[num624].scale *= 0.3f;
			Main.dust[num624].noGravity = true;
		}
	}
}
