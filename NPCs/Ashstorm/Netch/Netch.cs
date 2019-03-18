using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.NPCs.Ashstorm.Netch
{
	public class Netch : ModNPC
	{
		int timer = 0;
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 150f;
		bool hat = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Netch");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 56;
			npc.height = 52;
			npc.damage = 70;
			npc.lifeMax = 640;
			npc.defense = 38;
			npc.knockBackResist = 0.1f;
			banner = npc.type;
			bannerItem = mod.ItemType("NetchBanner");
			npc.noGravity = true;
			npc.aiStyle = 44;
			aiType = NPCID.FlyingFish;
			npc.HitSound = SoundID.NPCHit41;
			npc.DeathSound = SoundID.NPCDeath22;
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.15f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			{
				int d = 0;
				int d1 = 173;
				for (int k = 0; k < 5; k++)
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
			{				int d = 0;
				int d1 = 173;
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				for (int num623 = 0; num623 < 40; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NetchJelly"));
			}
			if (Main.rand.Next(150) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodskaalBlade"));
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
            EventsUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Ashstorm/Netch/Netch_Glow"));
        }
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.ashStorm) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.5f : 0f;
		}
		bool calfSpawn = false;
		public override bool PreAI()
		{
			if (!calfSpawn)
			{
									Vector2 direction = Vector2.One.RotatedByRandom(MathHelper.ToRadians(100));
					int newNPC = NPC.NewNPC((int)npc.position.X + Main.rand.Next (-30, 30), (int)npc.position.Y + Main.rand.Next (-30, 30), mod.NPCType("BabyNetch"));
					int newNPC1 = NPC.NewNPC((int)npc.position.X+ Main.rand.Next (-30, 30), (int)npc.position.Y + Main.rand.Next (-30, 30), mod.NPCType("BabyNetch"));
					int newNPC2 = NPC.NewNPC((int)npc.position.X + Main.rand.Next (-30, 30), (int)npc.position.Y + Main.rand.Next (-30, 30), mod.NPCType("BabyNetch"));
					Main.npc[newNPC].velocity = direction * (Main.rand.Next(-15, 15));
					Main.npc[newNPC1].velocity = direction * (Main.rand.Next(-15, 15));
					Main.npc[newNPC2].velocity = direction * (Main.rand.Next(-15, 15));
					calfSpawn = true;
			}
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.3f, 0f, 0.8f);

			Player player = Main.player[npc.target];

			if (npc.Center.X >= player.Center.X && moveSpeed >= -20) // flies to players x position
				moveSpeed--;

			if (npc.Center.X <= player.Center.X && moveSpeed <= 20)
				moveSpeed++;

			npc.velocity.X = moveSpeed * 0.08f;

			if (npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30) //Flies to players Y position
			{
				moveSpeedY--;
				HomeY = 185f;
			}

			if (npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
				moveSpeedY++;

			npc.velocity.Y = moveSpeedY * 0.1f;
			if (Main.rand.Next(210) == 3)
				HomeY = -25f;

			return true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(5) == 0)
				target.AddBuff(BuffID.Electrified, 200);
		}
	}
}
