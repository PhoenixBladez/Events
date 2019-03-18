using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.AshStorm
{
	public class CrystalProj : ModProjectile
	{
		int target;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shardstorm");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;       //projectile width
			projectile.height = 8;  //projectile height
			projectile.friendly = true;      //make that the projectile will not damage you
			projectile.magic = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 1;      //how many npc will penetrate
			projectile.timeLeft = 460;   //how many time projectile projectile has before disepire // projectile light
			projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.aiStyle = -1;
		}

		public override void AI()
		{
			for (int j = 0; j < 10; j++)
			{
				int num2 = Dust.NewDust(projectile.Center, 4, 4, 27, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num2].alpha = projectile.alpha;
				Main.dust[num2].velocity *= 0f;
				Main.dust[num2].scale *= .8f;	
				Main.dust[num2].noGravity = true;
			}
				for (int i = 0; i < 8; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 9f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 9f * (float)i;
				int num = Dust.NewDust(projectile.Center, 8, 8, 242, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].alpha = projectile.alpha;
				Main.dust[num].velocity *= .6f;
				Main.dust[num].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
            
            }

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int n = Main.rand.Next(2, 5);
			int deviation = Main.rand.Next(0, 300);
			for (int i = 0; i < n; i++)
			{
				float rotation = MathHelper.ToRadians(270 / n * i + deviation);
				Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(rotation);
				perturbedSpeed.Normalize();
				perturbedSpeed.X *= 5.5f;
				perturbedSpeed.Y *= 5.5f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CrystalBolt"), projectile.damage / 2, projectile.knockBack, projectile.owner);
			}

			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			int n = Main.rand.Next(2, 5);
			int deviation = Main.rand.Next(0, 300);
			for (int i = 0; i < n; i++)
			{
				float rotation = MathHelper.ToRadians(270 / n * i + deviation);
				Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(rotation);
				perturbedSpeed.Normalize();
				perturbedSpeed.X *= 5.5f;
				perturbedSpeed.Y *= 5.5f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CrystalBolt"), projectile.damage / 3 * 2, projectile.knockBack, projectile.owner);
			}

			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
			return true;
		}
	}
}
