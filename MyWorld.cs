using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace Events
{
	public class MyWorld : ModWorld
	{
		private static bool dayTimeLast;
		public static bool dayTimeSwitched;

		public static bool Meteor = false;
		public static bool Lightning = false;
		public static bool Jellyfish = false;
		public static bool acidRain = false;
		
		public static bool heavyRain = false;
		public static bool windy = false;
		public static bool heavyWinds = false;
		public static bool Hail = false;
		public static bool lightRain = false;
		public static bool heatWave = false;

		public override void Initialize()
		{
			Meteor = false;
			Lightning = false;
			Jellyfish = false;
			acidRain = false;
			heavyRain = false;
			Hail = false;
			windy = false;
			heavyWinds = false;
			lightRain = false;
			heatWave = false;
		}
		public override void PostUpdate()
		{
			if (MyWorld.heatWave)
			{
				Main.raining = false;
			}
			if (Main.dayTime != dayTimeLast)
				dayTimeSwitched = true;
			else
				dayTimeSwitched = false;
			dayTimeLast = Main.dayTime;

			if (dayTimeSwitched)
			{
				if (!Main.dayTime)
				{
					if (Main.rand.Next(20) == 0 && NPC.downedBoss2)
					{
						Main.NewText("Meteor shower inbound! It's going to be a beautiful night...", 255, 28, 99);
						Meteor = true;
					}
				}
				else
				{
					Meteor = false;
				}
			}
			
			if (dayTimeSwitched)
			{
				if (Main.raining && Main.rand.Next(12) == 0 && Main.hardMode)
				{
					Main.NewText("A putrid acid rain is setting in...", 111, 178, 44);
					acidRain = true;					
				}
				else
				{
					acidRain = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.raining && Main.rand.Next(7) == 0 && !MyWorld.acidRain)
				{
						Main.NewText("Watch out! A thunderstorm is brewing!", 66, 244, 223);
						Lightning = true;					
				}
				else if (!Main.raining)
				{
					Lightning = false;
				}
				else
				{
					Lightning = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.raining && Main.rand.Next(10) == 0 && !MyWorld.acidRain && !MyWorld.lightRain)
				{
						Main.NewText("The storm grows torrential...", 66, 134, 244);
						heavyRain = true;					
				}
				else
				{
					heavyRain = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.raining && Main.rand.Next(10) == 0 && !MyWorld.acidRain && !MyWorld.heavyRain)
				{
						Main.NewText("The rain is slowing to a drizzle!", 66, 134, 244);
						lightRain = true;					
				}
				else
				{
					lightRain = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.raining && Main.rand.Next(8) == 0 && !MyWorld.acidRain && !MyWorld.Lightning)
				{
						Main.NewText("It's hailing!", 66, 215, 244);
						Hail = true;					
				}
				else
				{
					Hail = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.rand.Next(12) == 0 && !MyWorld.heatWave)
				{
						Main.NewText("It's a windy outside!", 133, 171, 175);
						windy = true;					
				}
				else
				{
					windy = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (Main.rand.Next(26) == 0 && !MyWorld.heatWave && !MyWorld.windy)
				{
						Main.NewText("The wind is roaring!", 133, 171, 175);
						heavyWinds = true;					
				}
				else
				{
					heavyWinds = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (!Main.raining && Main.rand.Next(30) == 0 && Main.dayTime)
				{
						Main.NewText("It's getting really hot...", 244, 209, 66);
						heatWave = true;	
				}
				else
				{
					heatWave = false;
				}
			}
			if (dayTimeSwitched)
			{
				if (!Main.dayTime)
				{
					if (Main.rand.Next(50) == 0)
					{
						Main.NewText("The ocean is glowing on the horizon!", 255, 109, 230);
						Jellyfish = true;
					}
				}
				else
				{
					Jellyfish = false;
				}
			}
		}
	}
}