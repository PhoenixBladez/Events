using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.AshStorm
{
	public class FirefrostProj : ModProjectile
	{
		int target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Frostfall");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;       //projectile width
			projectile.height = 10;  //projectile height
			projectile.friendly = true;      //make that the projectile will not damage you
			projectile.magic = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 2;      //how many npc will penetrate
			projectile.timeLeft = 460;   //how many time projectile projectile has before disepire // projectile light
			projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.aiStyle = -1;
		}

		public float counter = -1440;
		public override void AI()
		{
			counter++;
			if (counter >= 1440)
			{
				counter = -1440;
			}
			for (int i = 0; i < 10; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
				
				int num = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
			for (int f = 0; f < 10; f++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)f;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)f;
				
				int num = Dust.NewDust(projectile.Center - new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
			for (int j = 0; j < 6; j++)
			{

				int num2 = Dust.NewDust(projectile.Center, 6, 6, 6, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num2].alpha = projectile.alpha;
				Main.dust[num2].velocity *= 0f;
				Main.dust[num2].scale *= 1.4f;	
				Main.dust[num2].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
				
				int num623 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 68, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num623].velocity *= 1f;
				Main.dust[num623].scale = 0.5f;
				Main.dust[num623].noGravity = true;
            
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
				target.AddBuff(BuffID.OnFire, 300);
			else
				target.AddBuff(BuffID.Frostburn, 300);
		}
	}
}
