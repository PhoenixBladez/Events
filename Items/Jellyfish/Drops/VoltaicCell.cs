using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.Jellyfish.Drops
{
    public class VoltaicCell: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Voltaic Cell");
            Tooltip.SetDefault("Thrown projectiles have a small chance to paralyze enemies\nCompletely electrifies the 'Viscous Jelly' weapon");

        }


        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 16;
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 3;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>(mod).voltCell = true;
        }
			public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.15f, 0.48f, 0.5f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Jellyfish/Drops/VoltCell_Glow"),
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
