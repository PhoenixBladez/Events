using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Events;

namespace Events.Water
{
	public class IceWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			Player player = Main.LocalPlayer;
			return MyWorld.activeEvents.Contains(EventID.coldFront) && player.ZoneOverworldHeight && !player.ZoneDesert && !player.ZoneBeach;
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("IceWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("IceWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Water/IceDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.Purple;
		}
	}
}
