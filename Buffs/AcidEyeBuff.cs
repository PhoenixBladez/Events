using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class AcidEyeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Warding Eye");
			Description.SetDefault("Provides immunity to the 'Acid' debuff");

			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{

			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.hazmatHelm = true;
	
		}
	}
}
