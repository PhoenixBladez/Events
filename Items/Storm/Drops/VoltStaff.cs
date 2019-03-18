using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Events.Items.Storm.Drops
{
    public class VoltStaff : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fluxvolt Staff");
			Tooltip.SetDefault("Rains down meteors from the sky to the cursor's location innacurately\nAlso shoots a meteor in the direction of the cursor");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/Storm/Drops/VoltStaff_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.15f, 0.48f, 0.5f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Storm/Drops/VoltStaff_Glow"),
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
            item.damage = 27;
            item.magic = true;
            item.mana = 11;
            item.width = 50;
            item.height = 50;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Terraria.Item.sellPrice(0, 3, 10, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item122;
            item.shoot = mod.ProjectileType("Volt");
            item.autoReuse = true;
            item.shootSpeed = 12f;
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
            int proj = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer, 0.0f, 1);
			Main.projectile[proj].friendly = true;
			Main.projectile[proj].hostile = false;
			Main.projectile[proj].magic = true;
            return false;
        }
    }
}