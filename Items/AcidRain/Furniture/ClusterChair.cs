using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Furniture
{
	public class ClusterChair : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eyeball Chair");
		}


		public override void SetDefaults()
		{
            item.width = 16;
			item.height = 30;
            item.value = 150;

            item.maxStack = 99;

            item.useStyle = 1;
			item.useTime = 10;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("ClusterChairTile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"CreepCluster", 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
}