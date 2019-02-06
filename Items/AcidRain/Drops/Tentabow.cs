using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Drops
{
    public class Tentabow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tentabow");
			Tooltip.SetDefault("Occasionally shoots out spurts of eyeballs");
		}



        public override void SetDefaults()
        {
            item.damage = 34;
            item.noMelee = true;
            item.ranged = true;
            item.width = 32;
            item.height = 44;
            item.useTime = 27;
            item.useAnimation = 27;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 2;
            item.rare = 4;
            item.UseSound = SoundID.Item5;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.value = Item.sellPrice(0, 2, 50, 0);
            item.autoReuse = true;
            item.shootSpeed = 8f;

        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			if (Main.rand.Next (10) == 0)
			{
			Projectile.NewProjectile(position.X, position.Y - Main.rand.Next (-20, 20), speedX + Main.rand.Next(-5, 5), speedY, mod.ProjectileType("Eyeball"), damage, knockBack, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(position.X, position.Y - Main.rand.Next (-20, 20), speedX + Main.rand.Next(-5, 5), speedY, mod.ProjectileType("Eyeball"), damage, knockBack, player.whoAmI, 0f, 0f);
            Projectile.NewProjectile(position.X, position.Y - Main.rand.Next (-20, 20), speedX + Main.rand.Next(-5, 5), speedY, mod.ProjectileType("Eyeball"), damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return true; 
        }
       public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
        }
    }
}