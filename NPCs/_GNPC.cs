using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
		public override bool InstancePerEntity => true;
		public override void GetChat(NPC npc, ref string chat)
		{
			if (npc.type == NPCID.Angler && MyWorld.Jellyfish && Main.rand.Next(4) == 0)
			{
				switch (Main.rand.Next(1))
				{
					case 0:
					chat = "Woah! Have you checked out the ocean lately?\nI've heard tell of a Jellyfish Bloom going on!\nYou may be able to find and fish up some rare stuff!";
					break;
				}
			}
			if (npc.type == NPCID.Dryad && MyWorld.acidRain && Main.rand.Next(4) == 0)
			{
				switch (Main.rand.Next(1))
				{
					case 0:
					chat = "The spirits of nature must be angry... it is raining acid! However, I believe a rare plant thrives in these conditions. Perhaps it can ward off the corrosion of this acid rain.";
					break;
				}
			}
			if (npc.type == NPCID.Guide && MyWorld.heatWave && Main.rand.Next(4) == 0)
			{
				switch (Main.rand.Next(1))
				{
					case 0:
					chat = "The heat is killing me! I suggest you cool off by heading underground or to the ice biome! Maybe I'll jump into a pool of water with you to cool off.";
					break;
				}
			}
			if (npc.type == NPCID.Merchant && MyWorld.Meteor && Main.rand.Next(4) == 0)
			{
				switch (Main.rand.Next(1))
				{
					case 0:
					chat = "Those meteors falling from the sky are bound to be filled with rare goodies! They may even boast some otherworldly life. Sell me anything you find!";
					break;
				}
			}
		}
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.ZoneBeach && MyWorld.Jellyfish && spawnInfo.water) 
                {
                    pool.Clear(); //remove ALL spawns here
                    pool.Add(NPCID.PinkJellyfish, 5f);
                    pool.Add(NPCID.BlueJellyfish, 2f);
					pool.Add(mod.NPCType("ThermalJellyfish"), .8f);
					pool.Add(mod.NPCType("VoltaicJellyfish"), .6f);
					pool.Add(mod.NPCType("EthericJellyfish"), .4f);
                }
				if (player.ZoneBeach && MyWorld.Jellyfish && Main.hardMode && spawnInfo.water)
				{
					 pool.Add(NPCID.GreenJellyfish, .8f);
				}		
            }
			for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.ZoneOverworldHeight && MyWorld.Hail && !player.ZoneDesert) 
                {
                    pool.Add(NPCID.IceSlime, .7f);
                }
            }
		return;
        }
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (player.ZoneBeach && MyWorld.Jellyfish) 
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
