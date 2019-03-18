using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.MeteorShower.Drops
{
    public class AstralStaff : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Conduit");
			Tooltip.SetDefault("Opens up an astral rift that shoots beams at enemies\n Up to one portal can exist at once");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/MeteorShower/Drops/AstralStaff_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			
			Lighting.AddLight(item.position, 0.35f, 0.2f, 0.5f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/MeteorShower/Drops/AstralStaff_Glow"),
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
        public override void SetDefaults()
        {
            item.damage = 25;
            item.magic = true;
            item.mana = 18;
            item.width = 44;
            item.height = 42;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Terraria.Item.sellPrice(0, 1, 80, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("AstralRift");
            item.shootSpeed = 16f;
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
            Terraria.Projectile.NewProjectile(mouse.X, mouse.Y, 0f, 0f, type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}