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
    public class NetchJelly: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Netch Jelly");
            Tooltip.SetDefault("'Slow and viscous'");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/AshStorm/Drops/NetchJelly_Glow");

        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 0, 4, 0);
            item.rare = 4;
			item.maxStack = 999;
        }
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.42f, 0.18f, 0.7f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/AshStorm/Drops/NetchJelly_Glow"),
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
		}
    }
}
