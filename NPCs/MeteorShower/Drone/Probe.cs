using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Events;

namespace Events.NPCs.MeteorShower.Drone
{
	public class Probe : ModNPC
	{
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 120f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Probe");
			Main.npcFrameCount[npc.type] = 1;
		}

		public override void SetDefaults()
		{
			npc.width = 30;
			npc.height = 42;
			npc.damage = 22;
			npc.defense = 12;
			npc.noTileCollide = false;
			npc.lifeMax = 65;
			npc.HitSound = SoundID.NPCHit4;
			banner = npc.type;
			bannerItem = mod.ItemType("ProbeBanner");
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 160f;
			npc.knockBackResist = .16f;
			npc.noGravity = true;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.15f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
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
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/MeteorShower/Drone/Probe_Glow"));
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 30;
				npc.height = 30;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 226, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 1f;
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
				if(shoottimer >= 180)
				{
				 Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, 12);
				Vector2 direction = Main.player[npc.target].Center - npc.Center;
						direction.Normalize();
						direction.X *= 14f;
						direction.Y *= 14f;

						int amountOfProjectiles = 1;
						for (int i = 0; i < amountOfProjectiles; ++i)
						{
							float A = (float)Main.rand.Next(-50, 50) * 0.02f;
							float B = (float)Main.rand.Next(-50, 50) * 0.02f;
							int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, mod.ProjectileType("ProbeBeam"), npc.damage / 3 * 2, 1, Main.myPlayer, 0, 0);
							Main.projectile[p].hostile = true;
						}
						shoottimer = 0;
				}
			}
			npc.TargetClosest(true);
			if (npc.direction == -1)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					direction *= 8f;
					npc.rotation = direction.ToRotation();
				}
				if (npc.direction == 1)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					direction *= -8f;
					npc.rotation = direction.ToRotation();
				}
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
			for (int index1 = 0; index1 < 6; ++index1)
            {
				float x = (npc.Center.X);
				float xnum2 = (npc.Center.X);
				float y = (npc.Center.Y);
				if (npc.direction == -1)
				{
					int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 68, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position.X = x;
					Main.dust[index2].position.Y = y;
					Main.dust[index2].scale = .5f;
					Main.dust[index2].velocity *= .1f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
				else if (npc.direction == 1)
				{
					int index2 = Dust.NewDust(new Vector2(xnum2, y), 1, 1, 68, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position.X = xnum2;
					Main.dust[index2].position.Y = y;
					Main.dust[index2].scale = .5f;
					Main.dust[index2].velocity *= .1f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
            }		
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .25f, .57f, .85f);
			npc.spriteDirection = npc.direction;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(6) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Meteorite, Main.rand.Next(1, 4));
			}
			if (Main.rand.Next(17) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AstralStaff"), 1);
			}
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MeteorStaff"), 1);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.Meteor) && !Main.dayTime && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.09f : 0f;
		}
	}
}
