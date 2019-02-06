using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.NPCs.Jellyfish.EthericJellyfish
{
	public class EthericJellyfish : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Etheric Jellyfish");
		}

		public override void SetDefaults()
		{
			npc.width = 28;
			npc.height = 34;
			npc.damage = 32;
			npc.defense = 0;
			npc.lifeMax = 120;
			npc.HitSound = SoundID.NPCHit25;
			npc.DeathSound = SoundID.NPCDeath28;
			banner = npc.type;
			bannerItem = mod.ItemType("EthericJellyfishBanner");
			npc.value = 300f;
			npc.noGravity = true;
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
			counter++;
			{
				if (counter >= 600)
				{
					Vector2 direction = Vector2.One.RotatedByRandom(MathHelper.ToRadians(100));
					int newNPC = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("EthericJellyfish_Clone"));
					Main.npc[newNPC].velocity = direction * (Main.rand.Next(-4, 8));
					counter = 0;
				}
			}
		}

		public override void NPCLoot ()
		{
			Player player = Main.player[npc.target];
			float Speed = 0f; 
		    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
			int damage = 15;  
			int time = 0;
			int type = mod.ProjectileType("EthericJellyfish_Proj");
			float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
			int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			time = 0;
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Glowstick, Main.rand.Next(0, 5));
			}
			if (Main.rand.Next(100) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.JellyfishNecklace, 1);
			}
			if (Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EthericCharm"), 1);
			}
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
