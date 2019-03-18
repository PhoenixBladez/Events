using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.UI
{
    public class WeatherTech: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Climate Surveyor");
            Tooltip.SetDefault("Identifies special weather");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/UI/WeatherTech_Glow");

        }


        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 34;
            item.value = Item.buyPrice(0, 30, 0, 0);
			item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>(mod).weatherTech = true;
        }
		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<MyPlayer>(mod).weatherTech = true;
		}			 
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.2f, 0.66f, 0.83f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/UI/WeatherTech_Glow"),
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
		}////
    }
}
