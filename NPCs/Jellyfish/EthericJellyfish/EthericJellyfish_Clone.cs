using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.NPCs.Jellyfish.EthericJellyfish
{
	public class EthericJellyfish_Clone : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Etheric Shadow");
		}

		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 34;
			npc.damage = 26;
			npc.defense = 0;
			npc.lifeMax = 40;
			npc.HitSound = SoundID.NPCHit25;
			npc.DeathSound = SoundID.NPCDeath28;
			npc.value = 0f;
			npc.noGravity = true;
			npc.alpha = 100;
			npc.knockBackResist = 0f;
			npc.aiStyle = 18;
			aiType = NPCID.BlueJellyfish;
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BlueJellyfish];
			aiType = NPCID.BlueJellyfish;
			animationType = NPCID.BlueJellyfish;
		}
		int counter;	
		public override void AI()
		{
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), .26f, .12f, 0.5f);

			npc.spriteDirection = npc.direction;
		}
		public override void HitEffect(int hitDirection, double damage)
        {
			{
				int d = 173;
				for (int k = 0; k < 5; k++)
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
				int d = 173;
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
		}
	}
}
