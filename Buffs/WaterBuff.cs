using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class WaterBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Quenched");
			Description.SetDefault("Drinking water negates the effect of a Heatstroke");

			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			
		}
	}
}
