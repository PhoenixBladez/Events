using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class AcidPure : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Acid Purification");
			Description.SetDefault("The air here is cleansed of acid");

			Main.debuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.hazmatHelm = true;
		}
	}
}
