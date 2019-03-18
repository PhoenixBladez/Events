using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.NPCs.Ashstorm.AshHopper
{
	public class AshHopper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ash Strider");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 42;
			npc.damage = 55;
			npc.defense = 13;
			npc.lifeMax = 350;
			banner = npc.type;
			bannerItem = mod.ItemType("AshHopperBanner");
			npc.HitSound = SoundID.NPCHit31;
			npc.DeathSound = SoundID.NPCDeath16;
			npc.value = 1329f;
			npc.knockBackResist = .25f;
			npc.aiStyle = 26;
			aiType = NPCID.Wolf;

		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HopperLeg"));
			}
			if (Main.rand.Next(150) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BloodskaalBlade"));
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(8) == 1)
			{
				target.AddBuff(BuffID.BrokenArmor, 3600);
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
				int d = 0;
				int d1 = 0;
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
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, Color.White, 0.7f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/AshLeg"), 1f);				
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.ashStorm) && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.8f : 0f;
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.20f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{
			npc.spriteDirection = npc.direction;
		}
	}
}

