using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.Jellyfish.Furniture
{
	public class ThermalCage : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thermal Jellyfish Jar");
		}


		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 22;
			item.value = Item.buyPrice(0, 0, 30, 0);;

            item.maxStack = 999;

            item.useStyle = 1;
			item.useTime = 15;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("ThermalCage_Tile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"ThermalJelly", 1);
			recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
		public class VoltaicCage : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Jellyfish Jar");
		}


		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 22;
			item.value = Item.buyPrice(0, 0, 30, 0);;

            item.maxStack = 999;

            item.useStyle = 1;
			item.useTime = 15;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("VoltaicCage_Tile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"VoltJelly", 1);
			recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
		public class EthericCage : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Etheric Jellyfish Jar");
		}


		public override void SetDefaults()
		{
            item.width = 22;
			item.height = 22;
			item.value = Item.buyPrice(0, 0, 30, 0);;

            item.maxStack = 999;

            item.useStyle = 1;
			item.useTime = 15;
            item.useAnimation = 15;

            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;

			item.createTile = mod.TileType("EthericCage_Tile");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null,"EtherJelly", 1);
			recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.SetResult(this);
			recipe.AddRecipe();            
        }
	}
}