using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Events.Projectiles.Jellyfish
{
    public class ThermalJellyfish_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thermal Explosion");
        } 
        public override void SetDefaults()
        {
            projectile.width = 128;
            projectile.height = 128;
            projectile.aiStyle = 1;
			projectile.damage = 30;
			projectile.timeLeft = 1;
        }
    public override void AI()
    {
        projectile.velocity.X = 0f;
        projectile.velocity.Y = 0f;
	}
	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		if(Main.rand.Next (4)==0)
		{
		target.AddBuff(BuffID.OnFire, 180);
		}
	}
	public override void OnHitPlayer(Player target, int damage, bool crit)
	{
		if (Main.rand.Next (4)==0)
		{
				target.AddBuff(BuffID.OnFire, 180);
		}
	}		
	public override void Kill(int timeLeft)
	{
          Main.PlaySound(SoundID.Item14, projectile.position);
          for (int index = 0; index < 10; ++index)
            Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
          for (int index1 = 0; index1 < 10; ++index1)
          {
            int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity *= 3f;
            int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Main.dust[index3].velocity *= 2f;
          }
          int index4 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index4].velocity *= 0.4f;
          Main.gore[index4].velocity.X += (float) Main.rand.Next(-10, 11) * 0.1f;
          Main.gore[index4].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.1f;
          int index5 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index5].velocity *= 0.4f;
          Main.gore[index5].velocity.X += (float) Main.rand.Next(-10, 11) * 0.1f;
          Main.gore[index5].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.1f;
          if (projectile.owner == Main.myPlayer)
          {
            projectile.penetrate = -1;
            projectile.position.X += (float) (projectile.width / 2);
            projectile.position.Y += (float) (projectile.height / 2);
            projectile.width = 128;
            projectile.height = 128;
            projectile.position.X -= (float) (projectile.width / 2);
            projectile.position.Y -= (float) (projectile.height / 2);
            projectile.Damage();
          }
    }
}
}