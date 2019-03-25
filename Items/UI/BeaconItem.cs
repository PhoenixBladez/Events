using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.UI
{
	public class BeaconItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteorological Scanner");
			Tooltip.SetDefault("Displays information about ongoing weather events");
		}


		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 16;
            item.value = 150;

            item.maxStack = 99;

            item.useStyle = 1;
			item.rare = 3;
			item.useTime = 10;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("Beacon_Tile");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"BrokenBeacon", 1);
            recipe.AddRecipeGroup("LeadBar", 8);			
			recipe.AddIngredient(ItemID.Lens, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
}