using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.AshStorm.Drops
{
    public class BloodskaalBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodskaal Blade");
			Tooltip.SetDefault("Releases powerful energy blasts");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/AshStorm/Drops/Bloodskaal_Glow");
		}


        public override void SetDefaults()
        {
            item.damage = 48;            
            item.melee = true;            
            item.width = 52;              
            item.height = 54;             
            item.useTime = 32;           
            item.useAnimation = 32;     
            item.useStyle = 1;        
            item.knockBack = 6;
            item.rare = 6;
            item.shoot = mod.ProjectileType("BloodskaalProj");
            item.shootSpeed = 9f;
            item.autoReuse = true;
            item.UseSound = SoundID.Item66;     
            item.autoReuse = true;
            item.useTurn = true;
			item.value = Item.sellPrice(0, 2, 50, 0);
        }
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{		
			Lighting.AddLight(item.position, 0.5f, 0.12f, 0.18f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/AshStorm/Drops/Bloodskaal_Glow"),
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
		}////////
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			 recipe.AddIngredient(mod.ItemType("CrismiteCrystal"), 1);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
             recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
		}
		

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
				/*for (int k = 0; k < 2; k++)
				{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
				Main.dust[dust].velocity *= -1f;
				Main.dust[dust].noGravity = true;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
				Main.dust[dust].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * 34f;
				Main.dust[dust].position = player.Center - vector2_3;
				}*/
				for (int k = 0; k < 2; k++)
				{
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
				Main.dust[dust].velocity *= -1f;
				Main.dust[dust].noGravity = true;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, -90), (float) 42);
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(-100, -10) * 0.04f);
				Main.dust[dust].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * -6f;
				Main.dust[dust].velocity = player.direction * vector2_3;
				
				}
            }
        }
    }
}