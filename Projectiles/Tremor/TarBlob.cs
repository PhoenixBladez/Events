using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Tremor
{
	public class TarBlob : ModProjectile
	{
		int target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ash Ball");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;       //projectile width
			projectile.height = 10;  //projectile height
			projectile.friendly = false;      //make that the projectile will not damage you
			projectile.hostile = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 2;      //how many npc will penetrate
			projectile.timeLeft = 1200;   //how many time projectile projectile has before disepire // projectile light
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
			Player player = Main.player[Main.myPlayer];
			if (player == Main.player[Main.myPlayer])
            {
                for (int index2 = 0; index2 < 1; ++index2)
				{
                    if (player.active && (double) Vector2.Distance(projectile.Center, player.Center) <= (double) 20f)
                    {		
                      player.AddBuff(BuffID.Slow, 200);
                    }
                }
            }
			for (int i = 0; i < 10; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
				
				int num = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, 191, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
			for (int f = 0; f < 10; f++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)f;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)f;
				
				int num = Dust.NewDust(projectile.Center - new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, 191, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
			for (int j = 0; j < 6; j++)
			{

				int num2 = Dust.NewDust(projectile.Center, 6, 6, 191, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num2].alpha = projectile.alpha;
				Main.dust[num2].velocity *= 0f;
				Main.dust[num2].scale *= 1.4f;	
				Main.dust[num2].noGravity = true;
			}
			projectile.rotation += .1f;
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
				if (num416 > 6)
				{
					Main.projectile[num417].netUpdate = true;
					Main.projectile[num417].ai[1] = 36000f;
					return;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 191, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
				
				int num623 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 191, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num623].velocity *= 1f;
				Main.dust[num623].scale = 0.5f;
				Main.dust[num623].noGravity = true;
            
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.damage = 0;
			if (Main.rand.Next (2) == 0)
			{
				int d = 191;
				for (int k = 0; k < 6; k++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 1.5f * Main.rand.Next(-2, 2), -2.5f, 0, Color.Green, 0.7f);
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 1.5f * Main.rand.Next(-2, 2),-2.5f, 0, Color.Green, 0.7f);
				}

				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 1.5f * Main.rand.Next(-2, 2), -2.5f, 0, Color.Green, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 1.5f * Main.rand.Next(-2, 2), -2.5f, 0, Color.Green, 0.7f);
				projectile.velocity *= 0f;
				projectile.width = 40;
				projectile.knockBack = 0;
			}
			return false;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(2) == 0)
				target.AddBuff(BuffID.Slow, 300);
		}
	}
}
