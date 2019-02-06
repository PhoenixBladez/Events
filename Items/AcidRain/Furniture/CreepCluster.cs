using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Furniture
{
	public class CreepCluster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Creep Cluster");
			Tooltip.SetDefault("A mass of writhing tentacles and blinking eyeballs");
		}


		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 26;

			item.maxStack = 999;

            item.useStyle = 1;
			item.useTime = 10;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

            item.createTile = mod.TileType("CreepClusterTile");
		}
	}
}