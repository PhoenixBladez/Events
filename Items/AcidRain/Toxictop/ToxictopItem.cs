using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Toxictop
{
    public class ToxictopItem : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxictop");
			Tooltip.SetDefault("It reeks of deadly venom");
		}


        public override void SetDefaults()
        {
            item.width = 22;
			item.height = 32;
            item.rare = 2;
            item.maxStack = 99;
            item.noUseGraphic = true;
			item.useStyle = 2;
            item.useTime = item.useAnimation = 30;
			
			item.buffType = BuffID.Poisoned;
			item.buffTime = 3600;
            item.noMelee = true;
            item.consumable = true;
			item.UseSound = SoundID.Item2;
            item.autoReuse = false;

        }
    }
}
