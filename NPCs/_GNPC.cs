using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Events;

using Terraria;
using Terraria.ID;
using Events;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;


namespace Events.NPCs
{
	public class GNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public override void GetChat(NPC npc, ref string chat)
		{
					if (npc.type == NPCID.Angler && Main.rand.Next(4) == 0)
			{
				if (MyWorld.activeEvents.Contains(EventID.Jellyfish))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "Woah! Have you checked out the ocean lately?\nI've heard tell of a Jellyfish Bloom going on!\nYou may be able to find and fish up some rare stuff!";
						break;
					}
				}
				if (MyWorld.activeEvents.Contains(EventID.hurricane))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "Can you believe those winds outside? The fish are practically flying out of the water! Argh, I can't even step outside to catch them or I'll be blown away!";
						break;
					}
				}
			}
			if (npc.type == NPCID.Dryad && Main.rand.Next(4) == 0)
			{
				if (MyWorld.activeEvents.Contains(EventID.acidRain))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "The spirits of nature must be angry... it is raining acid! However, I believe a rare plant thrives in these conditions. Perhaps it can ward off the corrosion of this acid rain.";
						break;
					}
				}
				if (MyWorld.activeEvents.Contains(EventID.butterflies))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "Days like these where the butterflies come out to play are truly beautiful...";
						break;
					}
				}
			}
			if (npc.type == NPCID.Guide && Main.rand.Next(4) == 0)
			{
				if (MyWorld.activeEvents.Contains(EventID.heatWave))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "The heat is killing me! I suggest you cool off by heading underground or to the ice biome! Maybe I'll jump into a pool of water with you to cool off.";
						break;
					}
				}
				if (MyWorld.activeEvents.Contains(EventID.aurora))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "I was walking around in the evening and I saw the Northern Lights flare up around the icy tundra! It was beautiful.";
						break;
					}
				}
			}
			if (npc.type == NPCID.Merchant && Main.rand.Next(4) == 0)
			{
				if (MyWorld.activeEvents.Contains(EventID.Meteor))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "Those meteors falling from the sky are bound to be filled with rare goodies! They may even boast some otherworldly life. Sell me anything you find!";
						break;
					}
				}
				if (MyWorld.activeEvents.Contains(EventID.tremors))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "I've heard tell that these Tremors are kicking up rare gems and items underground. I'm sure there's no risk in heading down there.";
						break;
					}
				}
				if (MyWorld.activeEvents.Contains(EventID.fireflies))
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "It seems as though a firefly swarm is upon us. I can sell you a trusty bug net for a good price!";
						break;
					}
				}
			}
			if (npc.type == NPCID.GoblinTinkerer && Main.rand.Next(4) == 0 && Main.hardMode)
			{
				if (MyWorld.activeEvents.Any())
				{
					switch (Main.rand.Next(1))
					{
						case 0:
						chat = "Ah, another one of those odd climate events that have been cropping up recently. You know, I've developed something that should allow you to track them.";
						break;
					}
				}

			}
		}
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.GoblinTinkerer && Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("WeatherTech"));
				nextSlot++;
			}
		}
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish)&& spawnInfo.water) 
                {
                    pool.Clear(); //remove ALL spawns here
                    pool.Add(NPCID.PinkJellyfish, 5f);
                    pool.Add(NPCID.BlueJellyfish, 2f);
					pool.Add(mod.NPCType("ThermalJellyfish"), 1f);
					pool.Add(mod.NPCType("VoltaicJellyfish"), .8f);
					pool.Add(mod.NPCType("EthericJellyfish"), .6f);
                }
				if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish)&& Main.hardMode && spawnInfo.water)
				{
					 pool.Add(NPCID.GreenJellyfish, .8f);
				}		
            }
			for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.ZoneOverworldHeight && MyWorld.activeEvents.Contains(EventID.Hail) || player.ZoneOverworldHeight && MyWorld.activeEvents.Contains(EventID.coldFront)) 
                {
					if (!player.ZoneDesert)
					{
						if (Main.dayTime)
						{
							pool.Add(NPCID.IceSlime, .7f);
						}
						else	
						{
							pool.Add(NPCID.ZombieEskimo, .5f);					
							if (Main.hardMode)
							{
								pool.Add(NPCID.IceElemental, .25f);	
							}
						}
					}
                }
            }
			 for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (MyWorld.activeEvents.Contains(EventID.butterflies) && player.ZoneOverworldHeight && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneJungle && !player.ZoneHoly && Main.dayTime) //This needs to be the name of your ModWaterStyle class.
				{
					pool.Add(356, 5f);	
				}
			}
			 for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (MyWorld.activeEvents.Contains(EventID.tranquil) && player.ZoneOverworldHeight && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneJungle && !player.ZoneHoly && Main.dayTime) //This needs to be the name of your ModWaterStyle class.
				{
                    pool.Clear(); //remove ALL spawns here
                    pool.Add(NPCID.Bird, .35f);
					pool.Add(NPCID.Bunny, .35f);
					pool.Add(538, .15f);
					pool.Add(539, .0015f);
					pool.Add(443, .0015f);
					pool.Add(299, .15f);
					pool.Add(NPCID.GoldBird, .001f);
					pool.Add(298, .1f);
					pool.Add(297, .1f);
					pool.Add(362, .1f);
					pool.Add(363, .1f);
					pool.Add(356, .1f);
					pool.Add(364, .1f);
					pool.Add(365, .1f);
                }
				 if (MyWorld.activeEvents.Contains(EventID.tranquil) && player.ZoneOverworldHeight && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneJungle && !player.ZoneHoly && !Main.dayTime) //This needs to be the name of your ModWaterStyle class.
				{
					pool.Clear();
					pool.Add(NPCID.Bunny, .35f);
					pool.Add(538, .15f);
					pool.Add(539, .0015f);
					pool.Add(443, .0015f);
					pool.Add(299, .15f);
					pool.Add(355, .25f);
				}		
            }
		return;
        }
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish)) 
            {
				spawnRate = (int)(spawnRate * .2f);
				maxSpawns = (int)(maxSpawns * 2.5f);
			}
		}
		public override void NPCLoot(NPC npc)
		{
			Player player = Main.LocalPlayer;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (modPlayer.etherVortex && Main.rand.Next (30) == 0)
			{
			float Speed = 0f; 
		    Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
			int damage = 15;  
			int time = 0;
			int type = mod.ProjectileType("LiftField");
			float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
			int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
			
			}
		}
	}
}
