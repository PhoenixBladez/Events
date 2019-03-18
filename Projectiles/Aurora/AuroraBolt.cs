using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Aurora
{
	public class AuroraBolt : ModProjectile
	{
		int target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurora Bolt");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;       //projectile width
			projectile.height = 10;  //projectile height
			projectile.friendly = false;      //make that the projectile will not damage you
			projectile.hostile = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 2;      //how many npc will penetrate
			projectile.timeLeft = 180;   //how many time projectile projectile has before disepire // projectile light
			projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.aiStyle = -1;
		}

		public float counter = -1440;
		public int dustTimer = 0;
		int d = 68;
		int d1 = 173;
		public override void AI()
		{
			float num12 = 2.5f;
			float num23 = 2.5f;
			float num34 = 10f;
			counter++;
			dustTimer++;
			if (dustTimer <= 120)
			{
				d = 68;
				d1 = 110;
			}
			if (dustTimer >= 121 && dustTimer <= 240)
			{
				d = 110;
				d1 = 173;
			}
			if (dustTimer >= 241)
			{
				d = 173;
				d1 = 68;
			}
			if (dustTimer >= 360)
			{
				dustTimer = 0;
			}
			if (counter >= 1440)
			{
				counter = -1440;
			}
			for (int i = 0; i < 10; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
				
				int num = Dust.NewDust(projectile.Center, 6, 6, d, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
		}
		public override void Kill(int timeLeft)
		{
           for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), projectile.width, projectile.height, d1, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
				
				int num623 = Dust.NewDust(projectile.Center - new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), projectile.width, projectile.height, d, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num623].velocity *= 1f;
				Main.dust[num623].scale = 0.5f;
				Main.dust[num623].noGravity = true;
            
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next (3) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 300);
			}
			projectile.Kill();
		}
	}
}
