using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.AshStorm
{
	public class CrystalBolt : ModProjectile
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
			projectile.penetrate = 4;      //how many npc will penetrate
			projectile.timeLeft = 240;   //how many time projectile projectile has before disepire // projectile light
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
				Main.dust[num2].scale *= .4f;	
				Main.dust[num2].noGravity = true;
			}
		}
		public override void Kill(int timeLeft)
		{
            for (int num621 = 0; num621 < 20; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
            
            }

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
				projectile.Kill();
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
					projectile.velocity.X = -oldVelocity.X;

				if (projectile.velocity.Y != oldVelocity.Y)
					projectile.velocity.Y = -oldVelocity.Y;
				 for (int num621 = 0; num621 < 20; num621++)
				{
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 1f;
				Main.dust[num622].noGravity = true;
				Main.dust[num622].scale = 0.5f;
            
				}
				projectile.velocity *= 0.75f;
			}
			return false;
		}
	}
}
