using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using Events;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace Events
{
	public class MyWorld : ModWorld
	{
		private static bool dayTimeLast;
		public static bool dayTimeSwitched;
		public static int auroraType = 0;
		public static int auroraChance = 3;
		public static int jellyChance = 16;

		public static float screenshakeAmount = 0f;
        public static List<int> activeEvents = new List<int>();
		public bool windy = false;
		public bool heavyWinds = false;
        public override void Initialize()
		{
            activeEvents = new List<int>();
        }
		public bool txt = false;
		public bool txt1 = false;

		public int rainCheck = 0;
		public bool rainEvent = false;
		public override void PostUpdate()
		{
			if (Main.dayTime != dayTimeLast)
				dayTimeSwitched = true;
			else
				dayTimeSwitched = false;
			dayTimeLast = Main.dayTime;
		    if (dayTimeSwitched)
		    {
			     activeEvents = new List<int>();
		    }

		    if (dayTimeSwitched)
		    {
			    if (!activeEvents.Any())
                {
				    if (Main.raining)
				    {
					    if (Main.rand.Next(5) == 0)
					    {
						    StartEvent(EventID.Lightning);
					    }
					    if (Main.rand.Next(11) == 0 && Main.hardMode)
					    {
						    StartEvent(EventID.acidRain);
					    }
					    if (Main.rand.Next(5) == 0)
					    {
						    StartEvent(EventID.Hail);
					    }
					    if (Main.rand.Next(4) == 0)					
					    {
					    StartEvent(EventID.heavyRain);						
					    }
					    else if (Main.rand.Next(5) == 0)
					    {
				         StartEvent(EventID.lightRain);
					    }					
					    if (Main.rand.Next(23) == 0)
					    {
						    if (!MyWorld.activeEvents.Contains(EventID.lightRain) || !MyWorld.activeEvents.Contains(EventID.heavyRain) || !MyWorld.activeEvents.Contains(EventID.acidRain) || !MyWorld.activeEvents.Contains(EventID.Hail))
						    {
						        StartEvent(EventID.hurricane);
							    EndEvent(EventID.lightRain);							 
						    }
						    if (!MyWorld.activeEvents.Contains(EventID.heavyWinds))
						    {
						         StartEvent(EventID.heavyWinds);							 
						    }
					    }
					    else
					    {
						    EndEvent(EventID.hurricane);		
					    }
				    }
				    if (NPC.downedBoss3)
				    {
					    auroraChance = 5;
				    }
				    if (Main.hardMode)
				    {
					    auroraChance = 10;
				    }
				    if (!Main.dayTime && Main.rand.Next(auroraChance) == 0)
				    {
					    auroraType = Main.rand.Next(new int[]{1, 2, 3, 4, 5});
					    StartEvent(EventID.aurora);
				    }
				    else
				    {
					    EndEvent(EventID.aurora);
					    auroraType = 0;
				    }
				    if (Main.rand.Next(13) == 0 && NPC.downedBoss2 && !Main.dayTime)
				    {
					    StartEvent(EventID.Meteor);
				    }
				    else
				    {
					    EndEvent(EventID.Meteor);
				    }
					 if (!Main.raining && Main.rand.Next(12) == 0 && Main.dayTime && Main.hardMode)
				    {
					    StartEvent(EventID.ashfall);
				    }
				    else
				    {
					    EndEvent(EventID.ashfall);
				    }
				    if (!Main.raining && Main.rand.Next(20) == 0 && Main.hardMode && Main.dayTime)
				    {
					    StartEvent(EventID.ashStorm);
					    EndEvent(EventID.ashfall);
				    }
				    else
				    {
					    EndEvent(EventID.ashStorm);
				    }
					if (!Main.raining && Main.rand.Next(16) == 0 && Main.dayTime)
					{
						StartEvent(EventID.heatWave);
					}
					else if (!Main.raining && Main.rand.Next(13) == 0 && Main.dayTime)
					{
						StartEvent(EventID.coldFront);
					}
					else
					{
					EndEvent(EventID.heatWave);
					EndEvent(EventID.coldFront);
					}
					if (Main.rand.Next(23) == 0)
				    {
					    StartEvent(EventID.tremors);
					    screenshakeAmount = 5f;
				    }
				    else
				    {
					    EndEvent(EventID.tremors);
				    }
				    if (Main.hardMode)
				    {
					    jellyChance = 25;
				    }
				    if (Main.rand.Next(jellyChance) == 0 && !Main.dayTime)
				    {
					    StartEvent(EventID.Jellyfish);
				    }
				    else
				    {
					    EndEvent(EventID.Jellyfish);
				    }
				    if (Main.rand.Next(16) == 0 && !Main.hardMode || Main.rand.Next(37) == 0 && Main.hardMode)
				    {
					    StartEvent(EventID.tranquil);
					    EndEvent(EventID.ashfall);
					    EndEvent(EventID.ashStorm);
					    EndEvent(EventID.heatWave);
					    EndEvent(EventID.Lightning);
					    EndEvent(EventID.Hail);
					    EndEvent(EventID.acidRain);
					    EndEvent(EventID.hurricane);
					    EndEvent(EventID.heavyWinds);
				    }
				    else
				    {
					    EndEvent(EventID.tranquil);
				    }
				    if (Main.rand.Next(10) == 0 && !Main.raining && Main.dayTime || Main.rand.Next(20) == 0 && !Main.raining  && Main.dayTime)
				    {
					    StartEvent(EventID.butterflies);
					    NPC.butterflyChance = 2;
				    }
				    else
				    {
					    EndEvent(EventID.butterflies);
				    }
				    if (Main.rand.Next(13) == 0 && !Main.raining && !Main.dayTime || Main.rand.Next(25) == 0 && !Main.raining  && !Main.dayTime)
                    {
                        NPC.fireFlyChance = Main.rand.Next(0, 2);
                        NPC.fireFlyMultiple = Main.rand.Next(0, 6);
                        StartEvent(EventID.fireflies);
				    }
				    else
				    {
					    EndEvent(EventID.fireflies);
				    }
					if (Main.rand.Next(12) == 0 && !Main.raining && !Main.dayTime || Main.rand.Next(25) == 0 && !Main.raining  && !Main.dayTime)
					{
						StartEvent(EventID.stardust);
					}
					else
					{
						EndEvent(EventID.stardust);
					}		
                }
		    }
		    if (Main.windSpeed <= -.2f || Main.windSpeed >= .2f)
			{
				if (Main.windSpeed >= -.3f || Main.windSpeed <= .3f)
				{
					{
						if (!txt)
						{
						 StartEvent(EventID.windy);	
						 txt = true;
						 }											 
					}
				}						
			}
			else
			{
				EndEvent(EventID.windy);
				txt = false;
			}
			if (Main.windSpeed <= -.3f|| Main.windSpeed >= .3f)
			{
				{
					if (!txt1)
					{
                        StartEvent(EventID.heavyWinds);
					    txt1 = true;
					}
					
				}
			}
			else
			{
				txt1 = false;
				EndEvent(EventID.heavyWinds);
			}
			if (!Main.raining)
			{
					EndEvent(EventID.lightRain);
					EndEvent(EventID.heavyRain);
					EndEvent(EventID.Lightning);
					EndEvent(EventID.hurricane);
					EndEvent(EventID.Hail);
					EndEvent(EventID.acidRain);
			}
		}
			public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int num2 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
			if (num2 == -1)
			{
				return;
			}
			tasks.Insert(num2 + 1, new PassLegacy("Placing Weather Beacons", delegate(GenerationProgress progress)
			{
				for (int i = 0; i < Main.maxTilesX * 1.3f; i++)
				{
					int num3 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num4 = WorldGen.genRand.Next((int)WorldGen.rockLayer + 100, Main.maxTilesY);
					WorldGen.PlaceObject(num3, num4, mod.TileType("BrokenBeacon_Tile"), false, 0, 0, -1, -1);
					NetMessage.SendObjectPlacment(-1, num3, num4, mod.TileType("BrokenBeacon_Tile"), 0, 0, -1, -1);
				}
			}));
		}
		public override void PreUpdate()
		{
			Player player = Main.LocalPlayer;
			if (player.ZoneHoly)
			{
				Main.rainTexture = mod.GetTexture("Images/Misc/HallowRain");
			}
			else if (player.ZoneCorrupt)
			{
				Main.rainTexture = mod.GetTexture("Images/Misc/CorruptRain");
			}
			else if (player.ZoneCrimson)
			{
				Main.rainTexture = mod.GetTexture("Images/Misc/CrimsonRain");
			}
			else if (activeEvents.Contains(EventID.acidRain))
			{
				Main.rainTexture = mod.GetTexture("Images/Misc/AcidRain");
			}
			else
			{
				char directorySeparatorChar2 = Path.DirectorySeparatorChar;
				string str166 = "Images";
				directorySeparatorChar2 = Path.DirectorySeparatorChar;
				string str167 = directorySeparatorChar2.ToString();
				string str168 = "Rain";
   
				Main.rainTexture =  TextureManager.Load(str166 + str167 + str168);
			}
		}

        private void StartEvent(int id)
        {
            if (Main.netMode != 0)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)PacketType.StartEvent);
                packet.Write(id);
                packet.Send();

                switch(id)
                {
                    case EventID.aurora:
                        ModPacket data = mod.GetPacket();
                        data.Write((byte)PacketType.AuroraData);
                        data.Write(auroraType);
                        data.Send();
                        break;
                    case EventID.fireflies:
                        ModPacket data2 = mod.GetPacket();
                        data2.Write((byte)PacketType.FireflyData);
                        data2.Write(NPC.fireFlyChance);
                        data2.Write(NPC.fireFlyMultiple);
                        data2.Send();
                        break;
                }
            }
            activeEvents.Add(id);
        }

        private void EndEvent(int id)
        {
            if (Main.netMode != 0)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)PacketType.EndEvent);
                packet.Write(id);
                packet.Send();
            }
            activeEvents.Remove(id);
        }
	}
}