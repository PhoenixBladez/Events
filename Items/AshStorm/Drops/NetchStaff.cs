using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
namespace Events.Items.AshStorm.Drops
{
	public class NetchStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Netchrift Staff");
			Tooltip.SetDefault("Summons a rift of tentacles at the cursor position");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/AshStorm/Drops/NetchStaff_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.3f, 0.15f, 0.5f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/AshStorm/Drops/NetchStaff_Glow"),
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
			item.damage = 44;
			item.magic = true;
			item.mana = 14;
			item.width = 40;
			item.height = 40;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true; 
			item.knockBack = 1;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("NetchTentacle");
			item.shootSpeed = 0f;
		}
		  public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			 
      
                Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
			for (int i = 0; i < 6; ++i)
			{
				Vector2 targetDir = ((((float)Math.PI * 2) / 6) * i).ToRotationVector2();
				targetDir.Normalize();
				targetDir *= 7;
				Projectile.NewProjectile(mouse.X, mouse.Y, targetDir.X, targetDir.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
        
           
            return false;
        }
		public override void AddRecipes()
		{			
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(mod.ItemType("HeartStone"), 2);
			recipe2.AddIngredient(mod.ItemType("NetchJelly"), 5);
			recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
		}
	}
}