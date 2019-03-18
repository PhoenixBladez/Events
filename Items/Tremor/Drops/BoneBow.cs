using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Events.Items.Tremor.Drops
{
	public class BoneBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fossil Bow");
			Tooltip.SetDefault("Converts arrows into powerful Bone Arrows");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 40;
			item.value = Item.sellPrice(0, 1, 50, 0);
			item.rare = 3;
			item.damage = 16;
			item.knockBack = 1f;
			item.useStyle = 5;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useAmmo = AmmoID.Arrow;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("BoneArrow");
			item.shootSpeed = 11f;
			item.UseSound = SoundID.Item5;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BoneArrow"), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
