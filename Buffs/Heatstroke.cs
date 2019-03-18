using System;
using Events;
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
			Description.SetDefault("The sweltering heat is reduces your movement speed");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.heatEffect = true;
			
			if (!MyWorld.activeEvents.Contains(EventID.heatWave))
			{
			player.buffTime[buffIndex] = 0;
			modPlayer.heatEffect = false;
			}
			else if (player.ZoneSnow || player.wet || player.HasBuff(mod.BuffType("WaterBuff")))
			{
			player.buffTime[buffIndex] = 0;	
			modPlayer.heatEffect = false;		
			}
			if (player.ZoneDesert  || player.HasBuff(mod.BuffType("WaterBuff")))
			{
				player.lifeRegen -= 4;
			}
		}
	}
}
