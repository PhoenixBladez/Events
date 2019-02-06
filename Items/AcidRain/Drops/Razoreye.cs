using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Drops
{
	public class Razoreye : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Razoreye");
		}


		public override void SetDefaults()
		{
            item.damage = 36;            
            item.melee = true;
            item.width = 26;
            item.height = 42;
			item.useTime = 19;
			item.useAnimation = 19;
            item.noUseGraphic = true;
            item.useStyle = 1;
			item.knockBack = 4;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 4;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType ("RazoreyeProj");
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }
}