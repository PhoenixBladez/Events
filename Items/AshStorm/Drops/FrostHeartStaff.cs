using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.AshStorm.Drops
{
    public class FrostHeartStaff : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fiery Frostfall");
			Tooltip.SetDefault("Shoots a bolt of ice and fire");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/AshStorm/Drops/FrostHeart_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.07f, 0.15f, 0.23f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/AshStorm/Drops/FrostHeart_Glow"),
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
            item.damage = 39;
            item.magic = true;
            item.mana = 9;
            item.width = 50;
            item.height = 48;
            item.useTime = 29;
            item.useAnimation = 29;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Terraria.Item.sellPrice(0, 2, 10, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item69;
            item.shoot = mod.ProjectileType("FirefrostProj");
            item.autoReuse = true;
            item.shootSpeed = 4f;
        }
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}
		public override void AddRecipes()
		{			
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(mod.ItemType("HeartStone"), 3);
			recipe2.AddIngredient(ItemID.FrostCore, 1);
			recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
		}
    }
}