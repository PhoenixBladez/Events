using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.AshStorm.Drops
{
    public class HopperLeg: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash Strider Leg");
            Tooltip.SetDefault("'A delicacy for Ashlanders'");

        }


        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 16;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = 4;
			item.maxStack = 999;
			item.useStyle = 2;
            item.useTime = item.useAnimation = 30;
			
			item.buffType = BuffID.Regeneration;
			item.buffTime = 1800;
            item.noMelee = true;
            item.consumable = true;
			item.UseSound = SoundID.Item2;
            item.autoReuse = false;

        }
    }
}
