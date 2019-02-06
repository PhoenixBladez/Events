using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class Acid : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Acid");
			Description.SetDefault("The acid seeps through you armor\nReduces defense and saps life");

			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen -= 5;

			player.statDefense -= 5;
			player.endurance -= .05f;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			modPlayer.acidRainEffect = true;
			if (modPlayer.hazmatHelm == true)
			{
			player.buffTime[buffIndex] = 0;
			modPlayer.acidRainEffect = false;
			}
			if (Main.rand.Next (4) == 0)
			{
				int d = Dust.NewDust(player.position, player.width, player.height, 107);
				Main.dust[d].scale *= .4f;
				Main.dust[d].velocity *= 0f;
			}
		}
		
		public override void Update(NPC npc, ref int buffIndex)
		{
			if (!npc.friendly)
			{
			npc.lifeRegen -= 5;
			}
			if (Main.rand.Next (4) == 0)
			{
				int d = Dust.NewDust(npc.position, npc.width, npc.height, 107);
				Main.dust[d].scale *= .4f;
				Main.dust[d].velocity *= 0f;
			}
		}
	}
}
