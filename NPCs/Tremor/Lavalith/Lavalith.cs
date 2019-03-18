using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Events;

namespace Events.NPCs.Tremor.Lavalith
{
	public class Lavalith : ModNPC
	{
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 120f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lavalith");
			Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
		{
			npc.width = 38;
			npc.height = 44;
			npc.damage = 22;
			npc.defense = 7;
			npc.noTileCollide = true;
			npc.lifeMax = 80;
			npc.behindTiles = true;
			npc.HitSound = SoundID.NPCHit7;
			banner = npc.type;
			bannerItem = mod.ItemType("ProbeBanner");
			npc.DeathSound = SoundID.Item14;
			npc.value = 160f;
			npc.knockBackResist = .26f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.buffImmune[24] = true;
		}
		public int counter = 0;
		public override bool PreAI()
		{
			counter++;
			if (counter <= 90)
			{
				npc.dontTakeDamage = true;
			}
			else
			{
				npc.dontTakeDamage = false;
			}
			int dust = Dust.NewDust(npc.position, npc.width, npc.height, 6);
            Main.dust[dust].noGravity = true;	
			npc.spriteDirection = npc.direction;
			return true;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Tremor/Lavalith/Lavalith_Glow"));
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			
				int d = 0;
				int d1 = 6;
				for (int k = 0; k < 5; k++)
				{
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}
			if (npc.life <= 0)
			{				
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
			
			}
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 30;
				npc.height = 30;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
			}
		}
		int shoottimer;
		public override void AI()
		{
			shoottimer++;
			{
				if(shoottimer >= 240)
				{
				Vector2 direction = Main.player[npc.target].Center - npc.Center;
						direction.Normalize();
						direction.X *= 6f;
						direction.Y *= 6f;

						int amountOfProjectiles = 1;
						for (int i = 0; i < amountOfProjectiles; ++i)
						{
							int num184 = 20;
							if (Main.expertMode)
							{
								num184 = 11;
							}
							int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X, direction.Y, ProjectileID.GeyserTrap, num184, 1, Main.myPlayer, 0, 0);
							Main.projectile[p].hostile = true;
							Main.projectile[p].friendly = false;
							Main.projectile[p].height = 20;
							Main.projectile[p].timeLeft = 60;
						}
						shoottimer = 0;
				}
				/*Player player = Main.player[npc.target];
			shoottimer++;
			{
				if(shoottimer >= 240 && shoottimer < 321)
				{
					npc.SimpleFlyMovement(npc.DirectionTo(player.Center + new Vector2((float)((double)300.0), -250f)) * 6.5f, 1.8f);
					npc.direction = npc.spriteDirection = (double)npc.Center.X < (double)player.Center.X ? 1 : -1;
				}
				if (shoottimer >= 321)
				{
					npc.direction = npc.spriteDirection = (double)npc.Center.X < (double)player.Center.X ? 1 : -1;
					npc.SimpleFlyMovement(npc.DirectionTo(player.Center + new Vector2((float)((double)-300.0), -250f)) * 6.5f, 1.8f);
				}
				if (shoottimer == 392)
				{
					shoottimer = 0;
					npc.velocity = Vector2.Zero;					
				}
			}*/
			}
			npc.TargetClosest(true);

			Player player = Main.player[npc.target];
			if (npc.Center.X >= player.Center.X && moveSpeed >= -30) // flies to players x position
			{
				moveSpeed--;
			}

			if (npc.Center.X <= player.Center.X && moveSpeed <= 30)
			{
				moveSpeed++;
			}

			npc.velocity.X = moveSpeed * 0.1f;

			if (npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -27) //Flies to players Y position
			{
				moveSpeedY--;
				HomeY = 120f;
			}

			if (npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 27)
			{
				moveSpeedY++;
			}

			npc.velocity.Y = moveSpeedY * 0.12f;
			if (Main.rand.Next(220) == 22)
			{
				HomeY = -35f;
			}
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .85f, .3f, .25f);
			npc.spriteDirection = npc.direction;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Obsidian, Main.rand.Next(0, 2));
			}
			if (Main.rand.Next(150) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThermalCore"));
			}
		}

				public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.25f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}
