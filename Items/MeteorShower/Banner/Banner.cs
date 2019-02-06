using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.MeteorShower.Banner
{
	public class BlobBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Panspermic Blob Banner");
		}
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
			item.createTile = mod.TileType("BlobBanner_Tile");
			item.placeStyle = 0;
		}
	}
	public class MeteorSlimeBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Slime Banner");
		}
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
			item.createTile = mod.TileType("MeteorSlimeBanner_Tile");
			item.placeStyle = 0;
		}
	}
	public class ProbeBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Celestial Probe Banner");
		}
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
			item.createTile = mod.TileType("ProbeBanner_Tile");
			item.placeStyle = 0;
		}
	}
	public class MeteorWormBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Ion Courser Banner");
		}
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
			item.createTile = mod.TileType("MeteorWormBanner_Tile");
			item.placeStyle = 0;
		}
	}
}