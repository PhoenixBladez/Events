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
    [AutoloadEquip(EquipType.Head)]
    public class AuroraHelm : ModItem
	{
		public static int _type;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boreal Mask");
			Tooltip.SetDefault("Increases maximum minion slots by 1\nProvides a special bonus when worn with Boreal Armor");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/Aurora/Drops/AuroraHelm_Glow");
		
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.08f, .4f, .28f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Aurora/Drops/AuroraHelm_Glow"),
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
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
			glowMaskColor = Color.White;
		}

        int timer = 0;
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
			item.defense = 1;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
            item.accessory = true;
        }
		  public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == 2510 && legs.type == 2511;
        }
		 public override void UpdateArmorSet(Player player)
        {
  
            player.setBonus = "Increases minion damage by 4%";
			player.minionDamage += .05f;
        }
        public override void UpdateEquip(Player player)
        {
			Lighting.AddLight(player.position, 0.08f, .4f, .28f);
			player.maxMinions += 1;
		}	
    }
}
