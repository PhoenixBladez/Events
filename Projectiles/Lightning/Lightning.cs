using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Events.Projectiles.Lightning
{
    public class Lightning : ModProjectile
    {
				private bool init = false;
		Vector2 initialVel = Vector2.Zero;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        } 
        public override void SetDefaults()
        {
             projectile.width = 12;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many projectile will penetrate
            projectile.timeLeft = 240;   //how many time projectile projectile has before disepire
            projectile.light = 0.45f;    // projectile light
            projectile.extraUpdates = 6;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {             
			if (!init)
			{
				initialVel = projectile.velocity;
				init = true;
			}
			projectile.velocity = Vector2.Zero;
			projectile.Center = projectile.Center + initialVel.RotatedByRandom(MathHelper.ToRadians(120));
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 1; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 0), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;

					Main.dust[index2].velocity*=0.1f;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = .82f;
				}
            }
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 1; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, -2), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;

					Main.dust[index2].velocity*=0.1f;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = .82f;
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 1; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.500000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 2), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;

					Main.dust[index2].alpha = 255;
					Main.dust[index2].velocity*=0.1f;
					Main.dust[index2].scale = .82f;
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 1; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;

					Main.dust[index2].alpha = 255;
					Main.dust[index2].velocity*=0.1f;
					Main.dust[index2].scale = .82f;
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 1; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, -4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;

					Main.dust[index2].alpha = 255;
					Main.dust[index2].velocity*=0.1f;					
					Main.dust[index2].scale = .82f;
				}
			}
        }
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			{
				target.AddBuff(BuffID.Electrified, 180, true);
			}
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 20; i++)
			{
			int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num].noGravity = true;
			Main.dust[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
			Main.dust[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
			if (Main.dust[num].position != projectile.Center)
			Main.dust[num].velocity = projectile.DirectionTo(Main.dust[num].position) * 6f;
			}
		}
    }
}