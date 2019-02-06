using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.Jellyfish.Drops
{
	public class StickyJelly : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viscous Jelly");
            Tooltip.SetDefault("Sticks to enemies and slows them down");

        }


        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Shuriken);
            item.width = 18;
            item.height = 18;
            item.shoot = mod.ProjectileType("StickyJelly_Proj");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 10f;
            item.damage = 19;
            item.knockBack = 0f;
			item.value = Terraria.Item.sellPrice(0, 0, 0, 10);
            item.rare = 2;
            item.autoReuse = true;
        }
    }
}
