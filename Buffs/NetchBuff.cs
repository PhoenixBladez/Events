using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class NetchBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Voltaic Netchbile");
			Description.SetDefault("Attacks may stun enemies");

			Main.debuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.netchPotion = true;
			

		}
	}
}
