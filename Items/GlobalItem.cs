using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Events.Items
{
	public class GItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;
		
		public override void MeleeEffects (Item item, Player player, Rectangle hitbox)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (modPlayer.acidImbue == true && item.melee)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), (int)hitbox.Width/2, hitbox.Height, 107);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0f;
			}
		}
		
	}
}