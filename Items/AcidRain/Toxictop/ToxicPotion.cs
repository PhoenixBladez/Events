using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Toxictop
{
    public class ToxicPotion : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxictop Brew");
			Tooltip.SetDefault("Reduces life regneration slightly\nBoosts melee speed and critical strike chance by 6%");
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

            item.buffType = mod.BuffType("ToxicBuff");
            item.buffTime = 10800;

            item.UseSound = SoundID.Item3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ToxictopItem", 1);
			recipe.AddIngredient(null, "CreepCluster", 1);
			recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
