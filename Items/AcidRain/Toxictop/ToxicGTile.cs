using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Events;

namespace Events.Items.AcidRain.Toxictop
{
	internal sealed class ToxicGTiles : GlobalTile
	{
		int[] TileArray2 = {0, 3, 185, 186, 187, 71, 28};
		public override void RandomUpdate(int i, int j, int type)
        {
			if (type == 2 || type == 60 || type == 23)
			{
				if(TileArray2.Contains(Framing.GetTileSafely(i,j-1).type) &&TileArray2.Contains(Framing.GetTileSafely(i,j-2).type) && MyWorld.acidRain == true)
				{
                        if(Main.rand.Next(200)==0)
                        {
                            WorldGen.PlaceObject(i-1,j-1,mod.TileType("ToxictopTile"));
                            NetMessage.SendObjectPlacment(-1,i-1,j-1,mod.TileType("ToxictopTile"),0,0,-1,-1);
                        }            
                
				}
			}
		}
	}
}