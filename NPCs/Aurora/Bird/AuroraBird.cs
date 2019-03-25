using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.NPCs.Aurora.Bird
{
	public class AuroraBird : ModNPC
	{
		int timer = 0;
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 150f;
		int aitimer = 0;
		int frame = 0;
		int frametimer = 0;
		int leftDive = 1;
		bool hat = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Taigal Tern");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 38;
			npc.damage = 12;
			npc.lifeMax = 38;
			npc.defense = 2;
			npc.knockBackResist = 0.31f;
			npc.noGravity = true;
			npc.aiStyle = 44;
			npc.value = 60f;
			aiType = NPCID.FlyingAntlion;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath4;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			{
				int d = 186;
				int d1 = 172;
				for (int k = 0; k < 3; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
			}
			if (npc.life <= 0)
			{				int d = 172;
				int d1 = 186;
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.27f);
				}
					Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BirdWing"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/BirdHead"), 1f);
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AuroraBow"));
			}
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
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Aurora/Bird/Bird_Glow"));
        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
						Player player = spawnInfo.player;
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.aurora) && player.ZoneOverworldHeight && player.ZoneSnow && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse && !Main.bloodMoon && !NPC.AnyNPCs(mod.NPCType("AuroraBird"))? 0.12f : 0f;
		}
		public override void AI()
		{	
			Player player = Main.player[npc.target];
			aitimer++;
			frametimer++;
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.05f, 0.2f, 0.8f);
				if(frametimer == 8)
				{
					frame++;
					frametimer = 0;
				}
				if(frame == 4) //if you only have 6 frames for animation
				{
					frame = 0;
				}
				if(aitimer >= 540 && aitimer < 560)
				{
					frame = 1;
					npc.SimpleFlyMovement(npc.DirectionTo(player.Center + new Vector2((float)((double) npc.direction * 1000 ), npc.Center.Y + .001f)) * 25.5f, 1.8f);
					npc.direction = npc.spriteDirection = (double)npc.Center.X < (double)player.Center.X ? 1 : -1;
				}
				if (aitimer >= 561)
				{
					aitimer = 0;	

				}
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frame.Y = frameHeight * frame;
		}
	}
}
