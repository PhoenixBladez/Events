using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Drops
{
    [AutoloadEquip(EquipType.Head)]
    public class HazmatHelm : ModItem
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hazmat Headgear");
			Tooltip.SetDefault("Provides immunity to the Acid Debuff\nWorks in the vanity slot");
			//EventsGlowmask.AddGlowMask(item.type, "Events/Items/AcidRain/Drops/HazmatGlow", Color.White);
		
		}


        int timer = 0;
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
			item.defense = 3;
            item.value = Item.sellPrice(0, 0, 15, 0);
            item.rare = 4;
            item.accessory = true;
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.buffImmune[mod.BuffType("Acid")] = true;
            player.GetModPlayer<MyPlayer>(mod).hazmatHelm = true;
		}	
		public override void UpdateVanity(Player player, EquipType type)
		{
			player.buffImmune[mod.BuffType("Acid")] = true;
            player.GetModPlayer<MyPlayer>(mod).hazmatHelm = true;
		}
    }
}
