using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Events.Items.AcidRain.Furniture
{
	public class CreepClusterTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			Main.tileMerge[Type][TileID.Dirt] = true;
			Main.tileMergeDirt[Type] = true;
			AddMapEntry(new Color(193, 27, 96));
			drop = mod.ItemType("CreepCluster");
			dustType = 5;
			soundType = 3;
		}
		public override bool HasWalkDust()
		{
              return true;
        }
		public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color) 
		{
			dustType = 5;
			makeDust = true;
		}
		public override void FloorVisuals(Player player)
		{
			if (Main.rand.Next (200) == 0)
			{
				Main.PlaySound(3, (int)player.position.X, (int)player.position.Y, 19);
			}
        }

		public override bool CanExplode(int i, int j)
		{
			return true;
		}
	}
}

