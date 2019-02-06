using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Events.Projectiles.MeteorShower
{
    public class AstralRift : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Rift");
        } 
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 1200;
        }
	int shoottimer;
    public override void AI()
    {
        projectile.velocity.X = 0f;
        projectile.velocity.Y = 0f;
            projectile.frameCounter++;
		Player player = Main.player[Main.myPlayer];
		if (projectile.frameCounter >= 45)
            {
                projectile.frameCounter = 0;
                float num = 8000f;
                int num2 = -1;
                for (int i = 0; i < 200; i++)
                {
                    float num3 = Vector2.Distance(projectile.Center, Main.npc[i].Center);
                    if (num3 < num && num3 < 640f && Main.npc[i].CanBeChasedBy(projectile, false))
                    {
                        num2 = i;
                        num = num3;
                    }
                }
                if (num2 != -1)
                {
                    bool flag = Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num2].position, Main.npc[num2].width, Main.npc[num2].height);
                    if (flag)
                    {
						Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 12);
                        Vector2 value = Main.npc[num2].Center - projectile.Center;
                        float num4 = 25f;
                        float num5 = (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
                        if (num5 > num4)
                        {
                            num5 = num4 / num5;
                        }
                        value *= num5;
                        int p = Terraria.Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value.X, value.Y, mod.ProjectileType("ProbeBeam"), projectile.damage, projectile.knockBack / 2f, projectile.owner, 0f, 0f);
                        Main.projectile[p].friendly = true;
                        Main.projectile[p].hostile = false;
                    }
                }
            }
		{
          int index = Dust.NewDust(projectile.position, 128, 128, 68, 0.0f, 0.0f, 200, new Color(), 0.5f);
          Main.dust[index].noGravity = true;
          Main.dust[index].velocity *= 0.85f;
		  Main.dust[index].scale *= .5f;
          Main.dust[index].fadeIn = 1.3f;
          Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
          vector2_1.Normalize();
          Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
          Main.dust[index].velocity = vector2_2;
          vector2_2.Normalize();
          Vector2 vector2_3 = vector2_2 * 29f;
          Main.dust[index].position = projectile.Center - vector2_3;
        }
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
				if (num416 > 1)
				{
					Main.projectile[num417].netUpdate = true;
					Main.projectile[num417].ai[1] = 36000f;
					return;
				}
			}
		}
	}
}