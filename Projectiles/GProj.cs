using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Events;

namespace Events.Projectiles
{

	public class GProj : GlobalProjectile
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override bool PreAI(Projectile projectile)
		{
						Player player = Main.player[projectile.owner];
			if (MyWorld.activeEvents.Contains(EventID.hurricane) && player.ZoneOverworldHeight)
			{
				projectile.velocity.X += .2f * (float)Main.windSpeed;
			}
			return true;
		}
	}
}