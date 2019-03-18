using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.UI
{
	public class BrokenBeacon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Beacon");
			Tooltip.SetDefault("'Random images and data seem to flicker across cracked screens'");
		}


		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 16;
            item.value = 150;

            item.maxStack = 99;

            item.useStyle = 1;
			item.useTime = 10;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("BrokenBeacon_Tile");
		}
	}
}