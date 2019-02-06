using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.NPCs.AcidRain.Tentagnat
{
	public class Tentablob : ModNPC
	{
		int timer = 0;
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float HomeY = 150f;
		bool hat = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tentablob");
			Main.npcFrameCount[npc.type] = 5;
		}

		public override void SetDefaults()
		{
			npc.width = 56;
			npc.height = 46;
			npc.buffImmune[mod.BuffType("Acid")] = true;
			npc.damage = 50;
			banner = npc.type;
			bannerItem = mod.ItemType("TentablobBanner");
			npc.lifeMax = 400;
			npc.defense = 12;
			npc.knockBackResist = 0.4f;
			npc.value = 600f;

			npc.noGravity = true;
			npc.HitSound = SoundID.NPCHit19;
			npc.DeathSound = SoundID.NPCDeath22;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.acidRain && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.4f : 0f;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
				int d = 107;
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				
				if (npc.life <= 0)
				{
					NPC.NewNPC((int)npc.Center.X + Main.rand.Next (-10, 10), (int)npc.Center.Y + Main.rand.Next (-10, 10), mod.NPCType("Tentagnat"), 0, 0, 0, 0, -1);
					
					NPC.NewNPC((int)npc.Center.X + Main.rand.Next (-10, 10), (int)npc.Center.Y + Main.rand.Next (-10, 10), mod.NPCType("Tentagnat"), 0, 0, 0, 0, -1);
					
					NPC.NewNPC((int)npc.Center.X + Main.rand.Next (-10, 10), (int)npc.Center.Y + Main.rand.Next (-10, 10), mod.NPCType("Tentagnat"), 0, 0, 0, 0, -1);
				}
				
				Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 19);
				Vector2 dir = Main.player[npc.target].Center - npc.Center;
				dir.Normalize();
				dir.X *= 12f;
				dir.Y *= 12f;
				bool expertMode = Main.expertMode;
				int amountOfProjectiles = 1;
				for (int i = 0; i < amountOfProjectiles; ++i)
				{
					float A = (float)Main.rand.Next(-200, 200) * 0.01f;
					float B = (float)Main.rand.Next(-200, 200) * 0.01f;
					int damagenumber = expertMode ? 19 : 27;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X + A, dir.Y + B, mod.ProjectileType("AcidBlob"), damagenumber, 1, Main.myPlayer, 0, 0);
				}
		}
		
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.1f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override bool PreAI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.1f, 0.3f, 0.09f);

			Player player = Main.player[npc.target];

			if (npc.Center.X >= player.Center.X && moveSpeed >= -25) // flies to players x position
				moveSpeed--;

			if (npc.Center.X <= player.Center.X && moveSpeed <= 25)
				moveSpeed++;

			npc.velocity.X = moveSpeed * 0.1f;

			if (npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -20) //Flies to players Y position
			{
				moveSpeedY--;
				HomeY = 185f;
			}

			if (npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 20)
				moveSpeedY++;

			npc.velocity.Y = moveSpeedY * 0.1f;
			return true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(5) == 0)
				target.AddBuff(BuffID.Poisoned, 600);
		}
		public override void NPCLoot()
		{
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CreepCluster"), Main.rand.Next(6, 24));
			}
			if (Main.rand.Next (100) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Hornet"), 1);
			}
			if (Main.rand.Next (45) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Tentabow"), 1);
			}
		}
	}
}
