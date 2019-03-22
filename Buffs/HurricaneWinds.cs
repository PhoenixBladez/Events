using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class HurricaneWinds : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Hurricane Winds");
			Main.pvpBuff[Type] = false;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			if(!npc.boss)
			{
				if (npc.type <= 547 && npc.type >= 578)
				{
				npc.velocity.X += .08f * (float)Main.windSpeed;
				}
			}
		}
	}
}