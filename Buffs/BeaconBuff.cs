using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class BeaconBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Weather Beacon");
			Description.SetDefault("The strange beacon displays information about ongoing weather events");

			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.weatherTech = true;
		}
	}
}
