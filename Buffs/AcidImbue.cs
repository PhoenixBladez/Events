using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class AcidImbue : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Acid Imbue");
			Description.SetDefault("Melee attacks coat enemies in damaging acid");

			Main.debuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.acidImbue = true;
			

		}
	}
}
