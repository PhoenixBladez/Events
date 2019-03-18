using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Events.Items.Aurora.Drops
{
    public class AuroraBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Borealis");
			Tooltip.SetDefault("All arrows shot emit a trail of boreal light");
			EventsGlowmask.AddGlowMask(item.type, "Events/Items/Aurora/Drops/AuroraBow_Glow");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Lighting.AddLight(item.position, 0.08f, .4f, .28f);
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Aurora/Drops/AuroraBow_Glow"),
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
            item.damage = 9;
            item.noMelee = true;
            item.ranged = true;
            item.width = 18;
            item.height = 50;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 1;
            item.rare = 1;
            item.UseSound = SoundID.Item5;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.value = Item.sellPrice(0, 2, 50, 0);
            item.autoReuse = false;
            item.shootSpeed = 1f;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			int p = Projectile.NewProjectile(position.X, position.Y, speedX * (Main.rand.Next(400, 800) / 90), speedY * (Main.rand.Next(400, 800) / 90), type, damage, knockBack, player.whoAmI);
				
			Main.projectile[p].GetGlobalProjectile<AuroraBowGProj>(mod).AuroraBow = true;
			return false; 
        }
		  public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
    }
}