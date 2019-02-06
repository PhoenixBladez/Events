using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class ToxicBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Toxictop Brew");
			Description.SetDefault("Increases melee speed and crit chance at the expense of draining life regenration");

			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen -= 1;
			player.meleeSpeed *= 1.06f;
			player.meleeCrit += 6;
			if (Main.rand.Next (4) == 0)
			{
				int d = Dust.NewDust(player.position, player.width, player.height, 71);
				Main.dust[d].scale *= .4f;
			}
		}
	}
}
