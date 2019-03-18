using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Events.Backgrounds
{
	public class ReachSurfaceBgStyle : ModSurfaceBgStyle
	{
		public override bool ChooseBgStyle()
		{
			Player player = Main.LocalPlayer;
			return !Main.gameMenu && MyWorld.activeEvents.Contains(EventID.coldFront) && player.ZoneOverworldHeight && !player.ZoneDesert && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneSnow && !player.ZoneJungle;
		}

		// Use this to keep far Backgrounds like the mountains.
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}

		public override int ChooseFarTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/ColdSnapFar");
		}

		/*public override int ChooseMiddleTexture()
		{
			return mod.GetBackgroundSlot("Backgrounds/ReachBiomeSurfaceMid");
		}*/

		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
			b += 220;
			scale *= .9f;
			return mod.GetBackgroundSlot("Backgrounds/ColdSnapClose");
		}
	}
}