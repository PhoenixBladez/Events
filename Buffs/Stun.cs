using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Buffs
{
	public class Stun : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Electric Stun");
			Main.pvpBuff[Type] = false;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			if(!npc.boss)
			{
			npc.velocity.X = .0f;
			if(Main.rand.Next(4) == 0)
			{
			int d = Dust.NewDust(npc.position, npc.width, npc.height, 226);
			Main.dust[d].velocity *= .0f;
			Main.dust[d].scale *= .5f;
			}
			}
		}
	}
}