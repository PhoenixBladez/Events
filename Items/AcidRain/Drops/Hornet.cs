using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Events.Items.AcidRain.Drops
{
    public class Hornet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hornet");
			Tooltip.SetDefault("Bullets leave behind pools of Corrosive Acid");
		}


        public override void SetDefaults()
        {
            item.damage = 31;
            item.ranged = true;   
            item.width = 65;     
            item.height = 21;    
            item.useTime = 4;
            item.useAnimation = 12;
            item.useStyle = 5;    
            item.noMelee = true; 
            item.knockBack = 5;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 2, 50, 0);
            item.rare = 8;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HornetBullet"); 
            item.shootSpeed = 15f;
            item.useAmmo = AmmoID.Bullet;
			item.reuseDelay = 30;
        }
		 public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 60f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
            float spread = 25 * 0.0174f;//45 degrees converted to radians
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double baseAngle = Math.Atan2(speedX, speedY);
            double randomAngle = baseAngle + (Main.rand.NextFloat() - 0.5f) * spread;
            speedX = baseSpeed * (float)Math.Sin(randomAngle);
            speedY = baseSpeed * (float)Math.Cos(randomAngle);
			type =  mod.ProjectileType("HornetBullet"); 
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}