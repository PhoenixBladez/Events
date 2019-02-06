using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.MeteorShower.Drops
{
    public class MeteorStaff : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Shower");
			Tooltip.SetDefault("Rains down meteors from the sky to the cursor's location innacurately\nAlso shoots a meteor in the direction of the cursor");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/MeteorShower/Drops/MeteorStaff_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.23f, 0.15f, 0.07f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/MeteorShower/Drops/MeteorStaff_Glow"),
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
            item.mana = 12;
            item.width = 58;
            item.height = 60;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 7;
            item.value = Terraria.Item.sellPrice(0, 2, 10, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item20;
            item.shoot = mod.ProjectileType("MeteorFriendly");
            item.autoReuse = false;
            item.shootSpeed = 16f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;
			Vector2 vector2_1 = new Vector2((float)((double)player.position.X + (double)player.width * 0.5 + (double)(Main.rand.Next(201) * -player.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)player.position.X)), (float)((double)player.position.Y + (double)player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
            vector2_1.X = (float)(((double)vector2_1.X + (double)player.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
            vector2_1.Y -= (float)(100);
            float num12 = Main.rand.Next(-30, 30);
            float num13 = 100;
            if ((double)num13 < 0.0) num13 *= -1f;
            if ((double)num13 < 20.0) num13 = 20f;
            float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
            float num15 = 10 / num14;
            float num16 = num12 * num15;
            float num17 = num13 * num15;
            float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile X position speed and randomnes
            float SpeedY = num17 + (float)Main.rand.Next(-40, 41) * 0.02f;  //this defines the projectile Y position speed and randomnes
            int proj = Projectile.NewProjectile(mouse.X, mouse.Y + Main.rand.Next(-800, -600), SpeedX, SpeedY, type, damage, knockBack, Main.myPlayer, 0.0f, 1);
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].magic = true;
            return true;
        }
		public override void AddRecipes()
		{			
			ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(this, 1);
			recipe2.AddIngredient(ItemID.PixieDust, 15);
			recipe2.AddIngredient(ItemID.SoulofLight, 10);
			recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(ItemID.MeteorStaff, 1);
            recipe2.AddRecipe();
		}
    }
}