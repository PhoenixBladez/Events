using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
namespace Events
{
	public class MyPlayer : ModPlayer
	{
		public bool voltCell = false;
		public bool thermalCore = false;
		public bool etherVortex = false;
		public bool acidRainEffect = false;
		public bool acidImbue = false;
		public bool hazmatHelm = false;
		public bool heatEffect = false;
		public bool windSky = false;
		public int hailChance = 0;
		public override void ResetEffects()
		{
			voltCell = false;
			thermalCore = false;
			windSky = false;
			acidImbue = false;
			etherVortex = false;
			acidRainEffect = false;
			hazmatHelm = false;
			heatEffect = false;
		}

		public override void PreUpdate()
		{
			Player player = Main.LocalPlayer;
			if (MyWorld.acidRain && player.ZoneOverworldHeight)
			{
				player.AddBuff(mod.BuffType("Acid"), 3605);
			}
			if (MyWorld.heatWave && player.ZoneOverworldHeight)
			{
				{
					player.AddBuff(mod.BuffType("Heatstroke"), 61);
				}
				Main.windSpeed = 0f;
			}
			if (heatEffect)
			{
				player.velocity.X *= 0.93f;
			}
			if (MyWorld.acidRain && player.wet && player.ZoneOverworldHeight)
			{
				player.AddBuff(mod.BuffType("Acid"), 3605);
			}
			for (int index3 = 0; index3 < 100; ++index3)
			{
				NPC npc = Main.npc[index3];
				if (npc.life >= 1 && MyWorld.acidRain && player.ZoneOverworldHeight)
				{
				npc.AddBuff(mod.BuffType("Acid"), 3605);
				}
			}
			
			
			if (MyWorld.heavyRain && Main.raining && !player.ZoneDesert)
			{
				Main.maxRaining = 1f;
			}
			if (MyWorld.heavyWinds && player.ZoneOverworldHeight)
			{
				if (Main.windSpeed <= -.01)
				{
					Main.windSpeed = -.5f;
				}
				if (Main.windSpeed >= .01)
				{
					Main.windSpeed = .5f;
				}
				player.AddBuff(BuffID.WindPushed, 100);
			}
			if (MyWorld.windy)
			{
				if (Main.windSpeed <= -.01)
				{
					Main.windSpeed = -.2f;
				}
				if (Main.windSpeed >= .01)
				{
					Main.windSpeed = .2f;		
				}
			}
			if (MyWorld.lightRain && Main.raining)
			{
				Main.maxRaining = .05f;
			}
			if (MyWorld.Meteor && Main.rand.Next(380) == 0 && player.ZoneOverworldHeight)
			{
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
                int proj = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y + Main.rand.Next(-1200, -900), SpeedX, SpeedY, mod.ProjectileType("Meteor"), 30, 3, Main.myPlayer, 0.0f, 1);
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].hostile = true;
			}
			if (MyWorld.windy || MyWorld.heavyWinds)
			{
				if (player.ZoneOverworldHeight)
				{
					windSky = true;
				}
				else 
				{
					windSky = false;
				}
			}
			else
			{
				windSky = false;
			}
			if (MyWorld.Hail)
			{
				hailChance = 300;
				if (player.ZoneDesert)
				{
					hailChance = 0;
				}
				if (!player.ZoneOverworldHeight)
				{
					hailChance = 0;
				}
				if (player.ZoneBeach)
				{
					hailChance = 0;
				}
				if (player.ZoneSnow)
				{
					hailChance = 125;
				}
				else
				{
					hailChance = 250;
				}
			}
			if (MyWorld.Hail && Main.rand.Next(hailChance) == 0 && player.ZoneOverworldHeight && !player.ZoneDesert)
			{

				{
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
                int proj = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y + Main.rand.Next(-1200, -900), SpeedX, SpeedY, mod.ProjectileType("Hailstone1"), 1, 3, Main.myPlayer, 0.0f, 1);
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].hostile = true;
				}

			}
			if (MyWorld.Lightning && Main.rand.Next(480) == 0 && player.ZoneOverworldHeight)
			{
				{
					if (Main.rand.Next(2) == 0)
					{
						Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder2"));
					}
					else
					{
						Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/Thunder"));
					}
				}
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
                int proj = Projectile.NewProjectile(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y + Main.rand.Next(-1200, -900), SpeedX, SpeedY, mod.ProjectileType("Lightning"), 20, 3, Main.myPlayer, 0.0f, 1);
				Main.projectile[proj].friendly = true;
				Main.projectile[proj].hostile = true;
			}
		}
		public override void UpdateBiomeVisuals()
		{
			player.ManageSpecialBiomeVisuals("Events:AcidRain", acidRainEffect, player.Center);
			player.ManageSpecialBiomeVisuals("Events:HeatWave", heatEffect, player.Center);
			if (windSky == true)
			{
				player.ManageSpecialBiomeVisuals("Events:WindySky", true);
			}
			else
			{
				player.ManageSpecialBiomeVisuals("Events:WindySky", false);
			}
		}
		public override void PostUpdate()
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (MyWorld.Jellyfish && player.ZoneOverworldHeight && player.ZoneBeach) //This needs to be the name of your ModWaterStyle class.
			{
			int off = 8; //Change this value depending on the strength of your light. Too big and it might cause lag, though. Never go above ~20 or so.
			int x = (int)(Main.screenPosition.X / 16f) - off;
			int y = (int)(Main.screenPosition.Y / 16f) - off;
			int x2 = x + (int)(Main.screenWidth / 16f) + off * 2;
			int y2 = y + (int)(Main.screenHeight / 16f) + off * 2;

			for (int i = x; i <= x2; i++)
			{
				for (int j = y; j <= y2; j++)
				{
					Tile t = Main.tile[i, j];
					if (t == null) return;

					if (!t.active() && t.liquid > 0 && t.liquidType() == 0)
					{
						//Set your lighting colour here. Try and keep the values quite small, too strong a light will require you to increase the "off" value up there
						Lighting.AddLight(i, j, 0.12f, 0.2f, 0.4f);
					}
				}
			}
		    
			}
		}
		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
				return;

			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(60) == 0)
			{
				caughtType = ItemID.PinkJellyfish;
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(60) == 0)
			{
				caughtType = ItemID.BlueJellyfish;
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(60) == 0 && Main.hardMode)
			{
				caughtType = ItemID.GreenJellyfish;
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(80) == 0)
			{
				caughtType = mod.ItemType("ThermalJelly");
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && Main.rand.Next(16) == 0)
			{
				caughtType = mod.ItemType("StickyJelly");
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(90) == 0)
			{
				caughtType = mod.ItemType("VoltJelly");
			}
			if (player.ZoneBeach && MyWorld.Jellyfish && power >= 50 && Main.rand.Next(100) == 0)
			{
				caughtType = mod.ItemType("EtherJelly");
			}
		}
		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (item.melee && this.thermalCore && Main.rand.Next (12) == 0)
			{
				int num54 = Projectile.NewProjectile(target.Center.X, target.Center.Y, Main.rand.Next(-2, 4), -5, mod.ProjectileType("ThermalJellyfish_Proj"), item.damage * 2 / 3, 1, player.whoAmI, 0f, 0f);
				Main.projectile[num54].friendly = true;
				Main.projectile[num54].hostile = false;
			}
			
			if (item.melee && this.acidImbue)
			{
				target.AddBuff(mod.BuffType("Acid"), 240);
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (proj.thrown && this.voltCell && Main.rand.Next(18)==0 && !target.boss)
			{
				target.AddBuff(mod.BuffType("Stun"), 240);
			}
		}
	}
}