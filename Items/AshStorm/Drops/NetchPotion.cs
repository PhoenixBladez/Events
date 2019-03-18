using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AshStorm.Drops
{
    public class NetchPotion : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Netchbile Potion");
			Tooltip.SetDefault("Attacks may stun enemies\nThis effect does not work on bosses");
		}


        public override void SetDefaults()
        {
            item.width = 18; 
            item.height = 30;
            item.rare = 5;
            item.maxStack = 30;

            item.useStyle = 2;
            item.useTime = item.useAnimation = 20;

            item.consumable = true;
            item.autoReuse = false;

            item.buffType = mod.BuffType("NetchBuff");
            item.buffTime = 7500;

            item.UseSound = SoundID.Item3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "NetchJelly", 3);
			recipe.AddIngredient(null, "HopperLeg", 1);
			recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
