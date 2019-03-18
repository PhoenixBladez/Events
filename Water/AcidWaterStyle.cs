using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Events;

namespace Events.Water
{
	public class AcidWaterStyle : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			Player player = Main.LocalPlayer;;
			return MyWorld.activeEvents.Contains(EventID.acidRain) && player.ZoneOverworldHeight && !player.HasBuff(mod.BuffType("AcidPure"));
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("AcidWaterfallStyle");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("AcidWaterSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Water/AcidDroplet");
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
