using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.Skies;
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
using Terraria.Graphics;
using Terraria.GameContent.Events;
using Events;

namespace Events
{
	class Events : Mod
	{
		public override void Load()
		{
			Player player = Main.LocalPlayer;
				Filters.Scene["Events:AcidRain"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(0.5f, 1f, .25f).UseOpacity(1.25f), EffectPriority.Medium);
				Filters.Scene["Events:HeatWave"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, (SamplerState) null).UseIntensity(4f), EffectPriority.Medium);
				Filters.Scene["Events:WindySky"] = new Filter((new BlizzardShaderData("FilterBlizzardForeground")).UseColor(0.4f, 0.4f, 0.4f).UseSecondaryColor(0.2f, 0.2f, 0.2f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.06f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
		}
	}
}
