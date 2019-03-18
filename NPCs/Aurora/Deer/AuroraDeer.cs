using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.NPCs.Aurora.Deer
{
	public class AuroraDeer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boreal Stag");
			Main.npcFrameCount[npc.type] = 14;
		}

		public override void SetDefaults()
		{
			npc.width = 62;
			npc.height = 106;
			npc.damage = 0;
			npc.defense = 10;
			npc.lifeMax = 500;
			npc.HitSound = SoundID.NPCHit6;
			npc.DeathSound = SoundID.NPCDeath5;
			npc.value = 329f;
			npc.knockBackResist = .10f;
			npc.aiStyle = 7;
			aiType = NPCID.Bunny;

		}

		public override void HitEffect(int hitDirection, double damage)
		{
				int d = 110;
				int d1 = 186;
				for (int k = 0; k < 5; k++)
				{
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.4f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.4f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.4f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}
				if (npc.life <= 0)
				{
				for (int k = 0; k < 20; k++)

				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.4f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}
								Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DeerHead"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DeerBody"), 1f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.4f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.4f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
                             drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AuroraHelm"));
			}
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Aurora/Deer/Deer_Glow"));
        }

		int frame = 2;
		int timer = 0;
		int timer2 = 0;
		int shootcounter = 0;
		int directionfacing = 0;
		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .128f, .4884f, .382f);
			Player target = Main.player[npc.target];		
			if (npc.life == npc.lifeMax)
			{
				npc.spriteDirection = npc.direction;
				npc.aiStyle = 7;
				aiType = NPCID.Bunny;
			}
			else
			{
				npc.aiStyle = 3;
				shootcounter++;
				aiType = NPCID.CorruptPenguin;
				npc.damage = 16;
			}
			timer++;
            if(timer == 5)
            {
                 frame++;
                 timer = 0;
			}
            if(frame == 5) //if you only have 6 frames for animation
            {
                frame = 2;
            }
			if (npc.velocity.Y != 0 && shootcounter <= 721)
			{
				frame = 3;
			}
			if (npc.velocity.X == 0 && shootcounter <= 721)
			{
				frame = 0;
			}
			if (shootcounter < 721)
			{
				npc.spriteDirection = npc.direction;
			}
			if (shootcounter == 720 || shootcounter == 721)
			{
				frame = 7;
			}
			if (shootcounter > 721)
			{
				timer2++;
                if(timer2 == 6)
                {
                    frame++;
                    timer2 = 0;
                }
                if(frame >= 13) //if you only have 6 frames for animation
                {
                    frame = 13;
                }
				npc.velocity = Vector2.Zero;
				if (npc.velocity == Vector2.Zero)
				{
					npc.velocity.X = .01f * npc.spriteDirection;
				}
				if (shootcounter == 811 || shootcounter == 870)
				{
					Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 35));
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
						direction.Normalize();
						direction.X *= 2f;
						direction.Y *= 2f;

						int amountOfProjectiles = 1;
						for (int i = 0; i < amountOfProjectiles; ++i)
						{
							float A = (float)Main.rand.Next(-50, 50) * 0.02f;
							float B = (float)Main.rand.Next(-50, 50) * 0.02f;
							int p = Projectile.NewProjectile(npc.Center.X + (npc.spriteDirection * 58), npc.Center.Y - 18, direction.X + A, direction.Y + B, mod.ProjectileType("AuroraVeil"), npc.damage / 3 * 2, 1, Main.myPlayer, 0, 0);
							Main.projectile[p].hostile = true;
						}
				}
			}
			if (shootcounter >= 920)
			{
				shootcounter = 0;
				frame = 2;
			}			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
						Player player = spawnInfo.player;
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.aurora) && player.ZoneOverworldHeight && player.ZoneSnow && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !NPC.AnyNPCs(mod.NPCType("AuroraDeer"))? 0.08f : 0f;
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frame.Y = frameHeight * frame;
		}
	}
}

