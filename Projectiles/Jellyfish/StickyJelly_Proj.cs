using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Projectiles.Jellyfish
{
	public class StickyJelly_Proj : ModProjectile
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viscous Jelly");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.ranged = true;
			projectile.timeLeft = 600;
			projectile.friendly = true;

			projectile.penetrate = -1;
		}

		public override bool PreAI()
		{
			if (projectile.ai[0] == 0)
				projectile.rotation = projectile.velocity.ToRotation() + 1.57F;
			else
			{
				projectile.ignoreWater = true;

				projectile.tileCollide = false;
				int num996 = 15;
				bool flag52 = false;
				bool flag53 = false;
				projectile.localAI[0] += 1f;
				if (projectile.localAI[0] % 30f == 0f)
				{
					flag53 = true;
				}
				int num997 = (int)projectile.ai[1];
				if (projectile.localAI[0] >= (float)(60 * num996))
				{
					flag52 = true;
				}
				else if (num997 < 0 || num997 >= 200)
				{
					flag52 = true;
				}
				else if (Main.npc[num997].active && !Main.npc[num997].dontTakeDamage)
				{
					projectile.Center = Main.npc[num997].Center - projectile.velocity * 2f;
					projectile.gfxOffY = Main.npc[num997].gfxOffY;
					if (flag53)
						Main.npc[num997].HitEffect(0, 1.0);
				}
				else
					flag52 = true;

				if (flag52)
					projectile.Kill();
			}
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (modPlayer.voltCell && Main.rand.Next (10) == 0)
			{
				int d= Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
				Main.dust[d].velocity *= .1f;
				
			}
			return false;
		}
		int counter;
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (modPlayer.voltCell)
			{
				if (Main.rand.Next (2) == 0)
				target.AddBuff(mod.BuffType("Stun"), 240);
			}
			else
			{
			target.AddBuff(mod.BuffType("Slow"), 720);
			target.AddBuff(BuffID.Slimed, 720);
			}
			int d;
			if (modPlayer.voltCell)
			{
				d = 226;
			}
			else
			{
				d = 176;
			}
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, d, 2.5f * 1, -2.5f, 0, Color.Purple, 0.7f);
			projectile.ai[0] = 1f;
			projectile.ai[1] = (float)target.whoAmI;
			target.velocity.X -= 1f;
			target.velocity.Y -= .5f;
			projectile.velocity = (target.Center - projectile.Center) * 0.75f;
			projectile.netUpdate = true;
			projectile.damage = 0;
			projectile.knockBack = 0f;
			
			int num31 = 6;
			Point[] array2 = new Point[num31];
			int num32 = 0;

			for (int n = 0; n < 1000; n++)
			{
				if (n != projectile.whoAmI && Main.projectile[n].active && Main.projectile[n].owner == Main.myPlayer && Main.projectile[n].type == projectile.type && Main.projectile[n].ai[0] == 1f && Main.projectile[n].ai[1] == target.whoAmI)
				{
					array2[num32++] = new Point(n, Main.projectile[n].timeLeft);
					if (num32 >= array2.Length)
						break;
				}
			}

			if (num32 >= array2.Length)
			{
				int num33 = 0;
				for (int num34 = 1; num34 < array2.Length; num34++)
				{
					if (array2[num34].Y < array2[num33].Y)
						num33 = num34;
				}
				Main.projectile[array2[num33].X].Kill();
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		int f;
		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			
			if (modPlayer.voltCell)
			{
				f = 226;	
			}
			else 
			{
				f = 176;
			}
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, f);
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
			if(Main.rand.Next(4)==0)
			Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("StickyJelly"), 1, false, 0, false, false);

		}

	}
}
