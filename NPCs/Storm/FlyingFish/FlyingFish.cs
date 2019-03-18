using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Events;

namespace Events.NPCs.Storm.FlyingFish
{
	public class FlyingFish : ModNPC
	{		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gliding Voltfish");
			Main.npcFrameCount[npc.type] = 4;
		}
		public override void SetDefaults()
		{
			npc.width = 42;
			npc.height = 34;
			npc.damage = 19;
			npc.defense = 5;
			npc.lifeMax = 60;
			banner = npc.type;
			bannerItem = mod.ItemType("FlyingFishBanner");
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 90f;
			npc.knockBackResist = .45f;
			npc.aiStyle = 44;
			aiType = NPCID.FlyingFish;
			animationType = NPCID.FlyingFish;
		}
		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .12f, .5f, .5f);
			npc.spriteDirection = npc.direction;
		}
		
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.Next(8) == 0)
			{
				target.AddBuff(BuffID.Electrified, 180, true);
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && MyWorld.activeEvents.Contains(EventID.Lightning) && Main.raining && !spawnInfo.playerSafe && !spawnInfo.invasion && !spawnInfo.sky && !Main.eclipse ? 0.1f : 0f;
		}
		public override void NPCLoot()
		{
			if (Main.rand.Next(6) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Feather, Main.rand.Next(1, 4));
			}
		}
		public override void HitEffect(int hitDirection, double damage)
        {
		    for (int i = 0; i < 10; i++) ;
			if (npc.life <= 0)
			{
				int d = 226;
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				}

				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
				Dust.NewDust(npc.position, npc.width, npc.height, d, 2.5f * hitDirection, -2.5f, 0, Color.Purple, 0.7f);
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FishHead"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FishWing"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/FishWing"), 1f);
			}
		}
	}
}
