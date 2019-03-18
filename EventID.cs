using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.GameContent;
using Terraria.ModLoader;
using System.Linq;
using Terraria.UI;
using Terraria.GameContent.UI;

namespace Events
{
	public class EventID
	{
        public static String[] nameString = {"Meteor Shower", "Thunderstorm", "Jellyfish Swarm", "Acid Rain", "Ashfall", "Heavy Rain", "Winds", "Heavy Winds", "Hailstorm", "Heat Wave", "Light Rain", "Ash Storm", "Boreal Aurora", "Hurricane", "Tremors", "Tranquil Winds", "Butterfly Swarm", "Firefly Swarm", "Cold Front", "Stardust" };

        public const short Meteor = 0;
        public const short Lightning = 1;
        public const short Jellyfish = 2;
        public const short acidRain = 3;
        public const short ashfall = 4;
        public const short heavyRain = 5;
        public const short windy = 6;
        public const short heavyWinds = 7;
        public const short Hail = 8;
        public const short heatWave = 9;
        public const short lightRain = 10;
		public const short ashStorm = 11;
		public const short aurora = 12;
		public const short hurricane = 13;
		public const short tremors = 14;
		public const short tranquil = 15;
		public const short butterflies = 16;
		public const short fireflies = 17;
		public const short coldFront = 18;
		public const short stardust = 19;
    }
}
