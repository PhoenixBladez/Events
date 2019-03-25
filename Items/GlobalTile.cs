using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items
{
	public class GTile : GlobalTile
	{
		int[] TileArray2 = {0, 3, 185, 186, 187, 71, 28};
		public int tremorItem = 0;
		public override void KillTile (int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			Player player = Main.LocalPlayer;
			if (type == 1 || type == 25 || type == 117 || type == 203 || type == 57)
			{
				if (Main.rand.Next(90) == 1 && MyWorld.activeEvents.Contains(EventID.tremors) && player.ZoneRockLayerHeight && !NPC.AnyNPCs(mod.NPCType("Lavalith")))
				{
					NPC.NewNPC((int)i * 16 - 20, (int)j * 16, mod.NPCType("Lavalith"), 0, 2, 1, 0, 0, Main.myPlayer);			
				}
				if (Main.rand.Next(45) == 1 && MyWorld.activeEvents.Contains(EventID.tremors) && player.ZoneRockLayerHeight)
				{
					tremorItem = Main.rand.Next(new int[]{11, 12, 13, 14, 699, 700, 701, 702, 999, 182, 178, 179, 177, 180, 181});
					if (Main.hardMode)
					{
						tremorItem = Main.rand.Next(new int[]{11, 12, 13, 14, 699, 700, 701, 702, 999, 182, 178, 179, 177, 180, 181, 364, 365, 366, 1104, 1105, 1106});
					}					
					Item.NewItem(i * 16, j * 16, 64, 48, tremorItem, Main.rand.Next(0, 2));
				}
			}
			if (type == 57)
			{
				if (Main.rand.Next(70) == 1 && MyWorld.activeEvents.Contains(EventID.tremors) && player.ZoneUnderworldHeight && !NPC.AnyNPCs(mod.NPCType("Lavalith")))
				{
					NPC.NewNPC((int)i * 16, (int)j * 16, mod.NPCType("Lavalith"), 0, 2, 1, 0, 0, Main.myPlayer);			
				}
			}
			if (type == 1 || type == 25 || type == 117 || type == 203)
			{
				if (player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
				{
					if (Main.rand.Next(70) == 1 && MyWorld.activeEvents.Contains(EventID.tremors) && !NPC.AnyNPCs(mod.NPCType("TarSap")))
					{
						NPC.NewNPC((int)i * 16 - 20, (int)j * 16, mod.NPCType("TarSap"), 0, 2, 1, 0, 0, Main.myPlayer);			
					}
				}
			}
			/*if (type == mod.TileType("GhostReed") && player.ZoneBeach)
			{
			{
			Item.NewItem(i * 16, j * 16, 32, 48, mod.ItemType("Kelp"), Main.rand.Next(2) + 1);			
			}
			}*/
		}		
	}	
}
