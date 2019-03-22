using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.Aurora.Drops
{
    public class AuroraOrb : ModItem
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismatic Orb");
			Tooltip.SetDefault("Getting hurt often refracts a homing wisp to attack enemies");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/Aurora/Drops/AuroraOrb_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.08f, .4f, .28f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Aurora/Drops/AuroraOrb_Glow"),
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

        int timer = 0;
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.value = Item.sellPrice(0, 0, 15, 0);
            item.rare = 4;
            item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<MyPlayer>(mod).auroraOrb = true;
		}	
    }
}
