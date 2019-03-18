using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Events;

namespace Events.Items.Aurora.Drops
{

	public class AuroraBowGProj : GlobalProjectile
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public bool AuroraBow = false;

		public override bool PreAI(Projectile projectile)
		{
			if (AuroraBow = true)
			{
				if (projectile.ranged)
				{
					if (projectile.owner == Main.myPlayer)
					{
						Lighting.AddLight(projectile.position, 0.2f, 0.4f, 0.5f);
						projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
						int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68);
						Main.dust[dust].noGravity = true;
						Main.dust[dust].velocity *= 0f;
						Main.dust[dust].scale = .78f;
						return true;
					}
				}
			}
			return base.PreAI(projectile);
		}
	}
}