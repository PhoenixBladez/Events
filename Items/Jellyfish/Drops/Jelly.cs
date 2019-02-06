using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.Jellyfish.Drops
{
    public class ThermalJelly : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thermal Jellyfish");
		}


        public override void SetDefaults()
        {
            item.width = 12;
			item.height = 20;
            item.rare = 1;
            item.maxStack = 99;
            item.noUseGraphic = true;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.bait = 25;

        }
		public override void Update (ref float gravity, ref float maxFallSpeed)
        {

			if(item.wet)
			{
			gravity *= 0f;
			maxFallSpeed *= -.09f;
			}
			else
			{
			maxFallSpeed *= 1f;
			gravity *= 1f;
			}
		}
	}
	public class VoltJelly : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Jellyfish");
		}


        public override void SetDefaults()
        {
            item.width = 12;
			item.height = 20;
            item.rare = 1;
            item.maxStack = 99;
            item.noUseGraphic = true;
			item.value = Item.sellPrice(0, 4, 0, 0);
			item.bait = 25;

        }
		public virtual void CaughtFishStack(ref int stack) 
		{
			stack = Main.rand.Next(10, 24);
		}
		public override void Update (ref float gravity, ref float maxFallSpeed)
        {

			if(item.wet)
			{
			gravity *= 0f;
			maxFallSpeed *= -.09f;
			}
			else
			{
			maxFallSpeed *= 1f;
			gravity *= 1f;
			}
		}
			
	}
	public class EtherJelly : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Etheric Jellyfish");
		}


        public override void SetDefaults()
        {
            item.width = 12;
			item.height = 20;
            item.rare = 1;
            item.maxStack = 99;
            item.noUseGraphic = true;
			item.value = Item.sellPrice(0, 4, 50, 0);
			item.bait = 25;

        }
		public override void Update (ref float gravity, ref float maxFallSpeed)
        {

			if(item.wet)
			{
			gravity *= 0f;
			maxFallSpeed *= -.09f;
			}
			else
			{
			maxFallSpeed *= 1f;
			gravity *= 1f;
			}
		}
			
	}
}
