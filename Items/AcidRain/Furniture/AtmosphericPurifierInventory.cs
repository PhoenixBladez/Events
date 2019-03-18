using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Furniture
{
	public class AtmosphericPurifierInventory : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Atmospheric Purifier");
			Tooltip.SetDefault("Cleanses nearby acid rain");
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

			item.createTile = mod.TileType("AcidPurifier_Tile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"CreepCluster", 5);
            recipe.AddRecipeGroup("AdamantiteBars", 4);			
			recipe.AddIngredient(ItemID.Glass, 10);;
			recipe.AddIngredient(ItemID.Wire, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
}