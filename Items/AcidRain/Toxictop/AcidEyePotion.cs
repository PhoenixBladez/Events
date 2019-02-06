using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Toxictop
{
    public class AcidEyePotion : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eyeward Potion");
			Tooltip.SetDefault("Provides immunity to the 'Acid' debuff");
		}


        public override void SetDefaults()
        {
            item.width = 18; 
            item.height = 30;
            item.rare = 4;
            item.maxStack = 30;

            item.useStyle = 2;
            item.useTime = item.useAnimation = 20;

            item.consumable = true;
            item.autoReuse = false;

            item.buffType = mod.BuffType("AcidEyeBuff");
            item.buffTime = 18000;

            item.UseSound = SoundID.Item3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CreepCluster", 1);
            recipe.AddIngredient(ItemID.Lens, 1);
            recipe.AddIngredient(ItemID.Waterleaf, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
