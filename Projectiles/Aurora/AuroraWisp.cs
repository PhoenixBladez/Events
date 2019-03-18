using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Aurora
{
	public class AuroraWisp : ModProjectile
	{
		int target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurora Veil");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;       //projectile width
			projectile.height = 10;  //projectile height
			projectile.hostile = false;      //make that the projectile will not damage you
			projectile.friendly = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 2;      //how many npc will penetrate
			projectile.timeLeft = 460;   //how many time projectile projectile has before disepire // projectile light
			projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.magic = true;
			projectile.alpha = 255;
			projectile.aiStyle = -1;
		}

		public float counter = -1440;
		public int dustTimer = 0;
		int d = 68;
		int d1 = 173;
		public override void AI()
		{
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
				
				int num = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, d, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
			
			bool flag25 = false;
			int jim = 1;
			for (int index1 = 0; index1 < 200; index1++)
			{
				if (Main.npc[index1].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[index1].Center, 1, 1))
				{
					float num23 = Main.npc[index1].position.X + (float)(Main.npc[index1].width / 2);
					float num24 = Main.npc[index1].position.Y + (float)(Main.npc[index1].height / 2);
					float num25 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num23) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num24);
					if (num25 < 500f)
					{
						flag25 = true;
						jim = index1;
					}

				}
			}
			if (flag25)
			{


				float num1 = 6f;
				Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num2 = Main.npc[jim].Center.X - vector2.X;
				float num3 = Main.npc[jim].Center.Y - vector2.Y;
				float num4 = (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
				float num5 = num1 / num4;
				float num6 = num2 * num5;
				float num7 = num3 * num5;
				int num8 = 10;
				projectile.velocity.X = (projectile.velocity.X * (float)(num8 - 1) + num6) / (float)num8;
				projectile.velocity.Y = (projectile.velocity.Y * (float)(num8 - 1) + num7) / (float)num8;
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
	}
}
