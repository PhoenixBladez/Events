using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class Heatstroke : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Heatstroke");
			Description.SetDefault("It's... so... hot...");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.heatEffect = true;
			
			if (!MyWorld.heatWave)
			{
			player.buffTime[buffIndex] = 0;
			modPlayer.heatEffect = false;
			}
			else if (player.ZoneSnow || player.wet)
			{
			player.buffTime[buffIndex] = 0;	
			modPlayer.heatEffect = false;		
			}
		}
	}
}
