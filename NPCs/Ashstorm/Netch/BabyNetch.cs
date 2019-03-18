using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.NPCs.Ashstorm.Netch
{
	public class BabyNetch : ModNPC
	{
		int timer = 0;
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 150f;
		bool hat = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Netch Calf");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 56;
			npc.height = 52;
			npc.damage = 55;
			npc.lifeMax = 380;
			npc.defense = 25;
			npc.knockBackResist = 0.22f;
			npc.scale *= .8f;
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
		public override void NPCLoot()
		{
			if (Main.rand.Next(6) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NetchJelly"));
			}
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
				
					for (int num623 = 0; num623 < 40; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					Main.dust[num624].scale *= .6f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
					Main.dust[num624].scale *= .6f;
				}
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NetchGore"), 1f);
			
			}
		}
	
		public override bool PreAI()
		{
			bool expertMode = Main.expertMode;
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.3f, 0f, 0.8f);

			Player player = Main.player[npc.target];
			if (npc.Center.X >= player.Center.X && moveSpeed >= -30) // flies to players x position
				moveSpeed--;

			if (npc.Center.X <= player.Center.X && moveSpeed <= 30)
				moveSpeed++;

			npc.velocity.X = moveSpeed * 0.1f;

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
		public override void AI()
		{
			npc.spriteDirection = npc.direction;
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(3) == 0)
				target.AddBuff(BuffID.Slow, 600);
		}
	}
}
