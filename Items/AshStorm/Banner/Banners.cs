using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AshStorm.Banner
{
	public class NetchBanner : ModItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 24;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.createTile = mod.TileType("NetchBanner_Tile");
			item.placeStyle = 0;
		}
	}
	public class AshHopperBanner : ModItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 24;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.createTile = mod.TileType("AshHopperBanner_Tile");
			item.placeStyle = 0;
		}
	}
	public class AshSpawnBanner : ModItem
	{
		// The tooltip for this item is automatically assigned from .lang files
		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 24;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.rare = 1;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.createTile = mod.TileType("AshSpawnBanner_Tile");
			item.placeStyle = 0;
		}
	}
}