using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Toxictop
{
    public class AcidFlask : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flask of Acid");
			Tooltip.SetDefault("Melee attacks coat enemies in damaging acid");
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

            item.buffType = mod.BuffType("AcidImbue");
            item.buffTime = 72010;

            item.UseSound = SoundID.Item3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ToxictopItem", 1);
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
