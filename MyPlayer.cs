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
using Events;
namespace Events
{
	public class MyPlayer : ModPlayer
	{
		public bool voltCell = false;
		public bool thermalCore = false;
		public bool etherVortex = false;
		public bool acidRainEffect = false;
		public bool ashSky = false;
		public bool ashOver = false;
		public bool acidImbue = false;
		public bool weatherTech = false;
		public int windFallType = 0;
		public bool hazmatHelm = false;
		public bool auroraOrb = false;
		public bool displayShader =false;
		public bool heatEffect = false;
		public bool hurricaneWind = false;
		public bool windSky = false;
		public bool netchPotion = false;
		public int hailChance = 0;
		
		private int SnowMinX;
        private int SnowMaxX;
		public override void ResetEffects()
		{
			voltCell = false;
			netchPotion = false;
			auroraOrb = false;
			thermalCore = false;
			windSky = false;
			acidImbue = false;
			hurricaneWind = false;
			weatherTech = false;
			displayShader =false;
			etherVortex = false;
			acidRainEffect = false;
			ashOver = false;
			ashSky = false;
			hazmatHelm = false;
			heatEffect = false;
		}
		public override void UpdateBiomeVisuals()
		{
            int x = (int)(player.Center.X / 16f);

            bool showAurora = 
                !player.ZoneDesert && 
                !Main.dayTime && 
                (player.ZoneSnow) &&
				MyWorld.activeEvents.Contains(EventID.aurora) || !Main.dayTime && 
                (player.ZoneSkyHeight) &&
				MyWorld.activeEvents.Contains(EventID.aurora);
				
			bool showHeat = MyWorld.activeEvents.Contains(EventID.heatWave) && player.ZoneOverworldHeight && displayShader && !player.ZoneSnow 
				|| MyWorld.activeEvents.Contains(EventID.heatWave) &&  player.ZoneOverworldHeight && displayShader && player.wet || 
				MyWorld.activeEvents.Contains(EventID.heatWave) && player.ZoneOverworldHeight && displayShader && !player.HasBuff(mod.BuffType("WaterBuff")) ;			
			 
			
            player.ManageSpecialBiomeVisuals("Events:AuroraSky", showAurora);
			player.ManageSpecialBiomeVisuals("Events:AcidRain", acidRainEffect, player.Center);
			player.ManageSpecialBiomeVisuals("Events:HeatWave", showHeat);

			if (windSky == true && displayShader)
			{
				player.ManageSpecialBiomeVisuals("Events:WindySky", true);
			}
			else
			{
				player.ManageSpecialBiomeVisuals("Events:WindySky", false);
			}
			if (ashOver == true || ashSky == true)
			{
				player.ManageSpecialBiomeVisuals("Events:AshstormParticles", true);
			}
			else if (ashSky == false)
			{
				player.ManageSpecialBiomeVisuals("Events:AshstormParticles", false);	
			}
			if (ashOver == true)
			{
				player.ManageSpecialBiomeVisuals("Events:Ashstorm", true);
			}
			else
			{
				player.ManageSpecialBiomeVisuals("Events:Ashstorm", false);
			}
			bool meteor = MyWorld.activeEvents.Contains(EventID.Meteor);
			player.ManageSpecialBiomeVisuals("Events:Meteor", meteor);
			
			bool meteorShader = MyWorld.activeEvents.Contains(EventID.Meteor) && displayShader;
			player.ManageSpecialBiomeVisuals("Events:MeteorShader", meteorShader);
			bool lightning = MyWorld.activeEvents.Contains(EventID.Lightning);
			
			player.ManageSpecialBiomeVisuals("Events:Stardust", lightning);
		}       
		
		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			if (auroraOrb)
			{
				if (Main.rand.Next(2) == 0)
				{
					Vector2 vel = new Vector2(0, -1);
					float rand = Main.rand.NextFloat() * MathHelper.TwoPi;
					vel = vel.RotatedBy(rand);
					vel *= 2f;
					Projectile.NewProjectile(Main.player[Main.myPlayer].Center.X, Main.player[Main.myPlayer].Center.Y, vel.X, vel.Y, mod.ProjectileType("AuroraWisp"), 12, 0, Main.myPlayer);
				}
			}
		}

		public int counter = 0;
		public int tremorTime = 0;
		public override void PreUpdate()
		{
			Player player = Main.LocalPlayer;
			if (MyWorld.activeEvents.Contains(EventID.coldFront) && !player.ZoneDesert && player.ZoneOverworldHeight || MyWorld.activeEvents.Contains(EventID.Hail) && !player.ZoneDesert && player.ZoneOverworldHeight )
			{		
			int maxValue = 800;
			int num1 = (int) ((double) (int) (500.0 * (double) ((float) Main.screenWidth / (float) Main.maxScreenW)) * (1.0 + 2.0 * (double) Main.cloudAlpha));
			float num2 = (float) (1.0 + 50.0 * (double) Main.cloudAlpha);
			if (Main.rand.Next(20) == 0)
			{
            int num3 = Main.rand.Next(Main.screenWidth + 1000) - 500;
            int num4 = (int) Main.screenPosition.Y - Main.rand.Next(50);
            if ((double) Main.player[Main.myPlayer].velocity.Y > 0.0)
              num4 -= (int) Main.player[Main.myPlayer].velocity.Y;
            if (Main.rand.Next(5) == 0)
              num3 = Main.rand.Next(500) - 500;
            else if (Main.rand.Next(5) == 0)
              num3 = Main.rand.Next(500) + Main.screenWidth;
            if (num3 < 0 || num3 > Main.screenWidth)
              num4 += Main.rand.Next((int) ((double) Main.screenHeight * 0.8)) + (int) ((double) Main.screenHeight * 0.1);
            int num5 = num3 + (int) Main.screenPosition.X;
            int index2 = num5 / 16;
            int index3 = num4 / 16;
            if (Main.tile[index2, index3] != null)
            {
              if ((int) Main.tile[index2, index3].wall == 0)
              {
                int index4 = Dust.NewDust(new Vector2((float) num5, (float) num4), 10, 10, 76, 0.0f, 0.0f, 0, new Microsoft.Xna.Framework.Color(), 1f);
                Main.dust[index4].scale += Main.cloudAlpha * 0.2f;
                Main.dust[index4].velocity.Y = (float) (3.0 + (double) Main.rand.Next(30) * 0.100000001490116);
                Main.dust[index4].velocity.Y *= Main.dust[index4].scale;
                if (!Main.raining)
                {
                  Main.dust[index4].velocity.X = Main.windSpeed + (float) Main.rand.Next(-10, 10) * 0.1f;
                  Main.dust[index4].velocity.X += (float) ((double) Main.windSpeed * (double) Main.cloudAlpha * 10.0);
                }
                else
                {
                  Main.dust[index4].velocity.X = (float) (Math.Sqrt((double) Math.Abs(Main.windSpeed)) * (double) Math.Sign(Main.windSpeed) * ((double) Main.cloudAlpha + 0.5) * 25.0 + (double) Main.rand.NextFloat() * 0.200000002980232 - 0.100000001490116);
                  Main.dust[index4].velocity.Y *= 0.5f;
                }
                Main.dust[index4].velocity.Y *= (float) (1.0 + 0.300000011920929 * (double) Main.cloudAlpha);
                Main.dust[index4].scale += Main.cloudAlpha * 0.2f;
                Main.dust[index4].velocity *= (float) (1.0 + (double) Main.cloudAlpha * 0.5);
				}
				}
			}
			}	
			int d = Main.rand.Next(new int[]{6, 162, 169, DustID.GoldCoin});
			if (MyWorld.activeEvents.Contains(EventID.stardust) && player.ZoneOverworldHeight)
			{		
			int maxValue = 800;
			int num1 = (int) ((double) (int) (500.0 * (double) ((float) Main.screenWidth / (float) Main.maxScreenW)) * (1.0 + 2.0 * (double) Main.cloudAlpha));
			float num2 = (float) (1.0 + 50.0 * (double) Main.cloudAlpha);
			if (Main.rand.Next(10) == 0)
			{
            int num3 = Main.rand.Next(Main.screenWidth + 1000) - 500;
            int num4 = (int) Main.screenPosition.Y - Main.rand.Next(50);
            if ((double) Main.player[Main.myPlayer].velocity.Y > 0.0)
              num4 -= (int) Main.player[Main.myPlayer].velocity.Y;
            if (Main.rand.Next(5) == 0)
              num3 = Main.rand.Next(500) - 500;
            else if (Main.rand.Next(5) == 0)
              num3 = Main.rand.Next(500) + Main.screenWidth;
            if (num3 < 0 || num3 > Main.screenWidth)
              num4 += Main.rand.Next((int) ((double) Main.screenHeight * 0.8)) + (int) ((double) Main.screenHeight * 0.1);
            int num5 = num3 + (int) Main.screenPosition.X;
            int index2 = num5 / 16;
            int index3 = num4 / 16;
            if (Main.tile[index2, index3] != null)
            {
              if ((int) Main.tile[index2, index3].wall == 0)
              {
                int index4 = Dust.NewDust(new Vector2((float)(num5), (float) num4), 10, 10, d, 0.0f, 0.0f, 0, new Microsoft.Xna.Framework.Color(), 1f);
                Main.dust[index4].scale += Main.cloudAlpha * 0.2f;
                Main.dust[index4].velocity.Y = (float) (3.0 + (double) Main.rand.Next(30) * 0.100000001490116);
                Main.dust[index4].velocity.Y *= Main.dust[index4].scale;
                if (!Main.raining)
                {
                  Main.dust[index4].velocity.X = Main.windSpeed + (float) Main.rand.Next(-10, 10) * 0.1f;
                  Main.dust[index4].velocity.X += (float) ((double) Main.windSpeed * (double) Main.cloudAlpha * 10.0);
                }
                else
                {
                  Main.dust[index4].velocity.X = (float) (Math.Sqrt((double) Math.Abs(Main.windSpeed)) * (double) Math.Sign(Main.windSpeed) * ((double) Main.cloudAlpha + 0.5) * 25.0 + (double) Main.rand.NextFloat() * 0.200000002980232 - 0.100000001490116);
                  Main.dust[index4].velocity.Y *= 0.5f;
                }
                Main.dust[index4].velocity.Y *= (float) (1.0 + 0.300000011920929 * (double) Main.cloudAlpha);
                Main.dust[index4].scale += Main.cloudAlpha * 0.2f;
                Main.dust[index4].velocity *= (float) (1.0 + (double) Main.cloudAlpha * 0.5);
				}
				}
			}
			}
			if (MyWorld.activeEvents.Contains(EventID.coldFront) && player.ZoneOverworldHeight && player.wet ||MyWorld.activeEvents.Contains(EventID.coldFront) && player.ZoneOverworldHeight && player.ZoneSnow)
			{

				{
					player.AddBuff(BuffID.Chilled, 61);
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.tremors) && Main.rand.Next (430) == 0 && !player.ZoneSkyHeight && player.ZoneRockLayerHeight)
			{
				MyWorld.screenshakeAmount = 7f;
				if (Main.rand.Next (6) == 0 && player.velocity.Y == 0)
				{
					player.AddBuff(BuffID.Dazed, 180);				
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.tremors) && Main.rand.Next (350) == 0 && !player.ZoneSkyHeight && player.ZoneUnderworldHeight)
			{
				MyWorld.screenshakeAmount = 10f;
				if (Main.rand.Next (4) == 0 && player.velocity.Y == 0)
				{
					player.AddBuff(BuffID.Dazed, 180);				
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.heatWave) && player.ZoneOverworldHeight)
			{
				int x1 = (int)player.Center.X / 16;
				int y1 = (int)player.Center.Y / 16;
				if (Main.tile[x1 + 1, y1].wall != 0 && Main.tile[x1, y1 + 1].wall != 0 && Main.tile[x1, y1 + 2].wall != 0)
				{
				}
				else
				{
					player.AddBuff(mod.BuffType("Heatstroke"), 61);
				}
			}
			if (heatEffect)
			{
				player.velocity.X *= 0.95f;
			}
			if (MyWorld.activeEvents.Contains(EventID.acidRain) && player.ZoneOverworldHeight)
			{
				if (!hazmatHelm)
				{
					int x1 = (int)player.Center.X / 16;
				int y1 = (int)player.Center.Y / 16;
				if (Main.tile[x1 + 1, y1].wall != 0 && Main.tile[x1, y1 + 1].wall != 0 && Main.tile[x1, y1 + 2].wall != 0)
				{
				}
				else
				{
					player.AddBuff(mod.BuffType("Acid"), 600);
				}
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.hurricane))
			{
				for (int index3 = 0; index3 < 100; ++index3)
				{
					NPC npc = Main.npc[index3];
					if( player.ZoneOverworldHeight)
					npc.AddBuff(BuffID.Wet, 60);
					npc.AddBuff(mod.BuffType("HurricaneWinds"), 60);				
				}
				if (player.ZoneOverworldHeight && displayShader)
				{
				player.AddBuff(BuffID.Wet, 60);
				}				
				if (Main.windSpeed <= -.01f)
				{
					Main.windSpeed = -1f;;
				}
				if (Main.windSpeed >= .01f)
				{
					Main.windSpeed = 1f;
				}
				Main.maxRaining = 1f;
				counter++;
				{
				if (player.ZoneOverworldHeight && Main.rand.Next(3) == 0)
				{
					Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/HurricaneWind"));
					counter = 0;
				}
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.tranquil) && player.wet || MyWorld.activeEvents.Contains(EventID.tranquil) && Main.raining)
			{
				int buff = BuffID.Regeneration; 
				player.AddBuff((buff), 60);
			}
			for (int index3 = 0; index3 < 100; ++index3)
			{
				NPC npc = Main.npc[index3];
				if (npc.life >= 1 && MyWorld.activeEvents.Contains(EventID.acidRain) && player.ZoneOverworldHeight)
				{
				npc.AddBuff(mod.BuffType("Acid"), 10);
				}
			}
			if (player.HeldItem.type == ItemID.Umbrella && player.velocity.Y > 0 && MyWorld.activeEvents.Contains(EventID.hurricane)) 
            {			
				player.velocity.X = 5f * Main.windSpeed; 
			}
			
			if (MyWorld.activeEvents.Contains(EventID.heavyRain) && Main.raining && !player.ZoneDesert)
			{
				Main.maxRaining = 1f;
			}	
			if (MyWorld.activeEvents.Contains(EventID.heavyWinds) && player.ZoneOverworldHeight)
			{
				 {
                bool leftPush = (Main.windSpeed <= 0.01f);
                bool doPush = true;
                if (leftPush)
                {
                    float x1 = (player.Center.X + Main.screenWidth / 4)/ 16f;
                    int playerTile = (int)player.Center.X / 16;
                    int offScTile = (int)x1;
                    int playerTileY = (int)player.Center.Y / 16;
                    for (int i = playerTile; i < offScTile; i++)
                    {
                        Tile t = Main.tile[i, playerTileY];
						Tile t1 = Main.tile[i, playerTileY + 1];
						Tile t2 = Main.tile[i, playerTileY -1];
                        if (Main.tile[i, playerTileY] != null && Main.tile[i, playerTileY + 1] != null && Main.tile[i, playerTileY - 1] != null)
                        {
							if (t.active() ||  t1.active() || t2.active())
							{
                            doPush = false;
							}
                        }
                    }
                }
                else //right push
                {
                    float x1 = (player.Center.X - Main.screenWidth /4)/ 16f;
                    int playerTile = (int)player.Center.X / 16;
                    int offScTile = (int)x1;
                    int playerTileY = (int)player.Center.Y / 16;
                    for (int i = offScTile; i < playerTile; i++)
                    {
						Tile t = Main.tile[i, playerTileY];
						Tile t1 = Main.tile[i, playerTileY + 1];
						Tile t2 = Main.tile[i, playerTileY -1];
                        if (Main.tile[i, playerTileY] != null && Main.tile[i, playerTileY + 1] != null && Main.tile[i, playerTileY - 1] != null)
                        {
							if (t.active() ||  t1.active() || t2.active())
							{
                            doPush = false;
							}
                        }
                    }
                }
                if (doPush && displayShader)
                    player.AddBuff(BuffID.WindPushed, 3);
				}
			}
            int x = (int)player.Center.X / 16;
            int y = (int)player.Center.Y / 16;
            {
                if (Main.tile[x + 1, y].wall != 0 && Main.tile[x, y + 1].wall != 0 && Main.tile[x, y + 2].wall != 0)
                 {
                      displayShader = false;
                 }
          
			else
			{
			 displayShader = true;
			}
			}
			if (MyWorld.activeEvents.Contains(EventID.windy) && player.ZoneOverworldHeight)
			 {
				counter++;
				if (counter >= 900)
				{
				if (player.ZoneOverworldHeight && Main.rand.Next(5) == 0)
				{
					
					Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/HurricaneWind"));
					counter = 0;
				}
				}
				 
			}
			if (MyWorld.activeEvents.Contains(EventID.lightRain) && Main.raining)
			{
				Main.maxRaining = .05f;
			}
			if (MyWorld.activeEvents.Contains(EventID.Meteor) && Main.rand.Next(380) == 0 && player.ZoneOverworldHeight)
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
			if (MyWorld.activeEvents.Contains(EventID.windy) || MyWorld.activeEvents.Contains(EventID.heavyWinds))
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
			if (MyWorld.activeEvents.Contains(EventID.ashfall) || MyWorld.activeEvents.Contains(EventID.ashStorm))
			{
				if (player.ZoneOverworldHeight)
				{
					ashSky = true;
					if (MyWorld.activeEvents.Contains(EventID.ashStorm))
					{
						ashOver = true;
						
					}
					else
					{

						ashOver = false;
					}
				}
			}
			else if (!MyWorld.activeEvents.Contains(EventID.ashfall) || !MyWorld.activeEvents.Contains(EventID.ashStorm))
			{
				ashSky = false;
			}
			if (MyWorld.activeEvents.Contains(EventID.Hail))
			{
				hailChance = 225;
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
					hailChance = 225;
				}
			}
			if (MyWorld.activeEvents.Contains(EventID.Hail) && Main.rand.Next(hailChance) == 0 && player.ZoneOverworldHeight && !player.ZoneDesert)
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
			
			if (Main.raining)
			{
			if (MyWorld.activeEvents.Contains(EventID.Lightning) && Main.rand.Next(480) == 0 && player.ZoneOverworldHeight || MyWorld.activeEvents.Contains(EventID.hurricane) && Main.rand.Next(440) == 0 && player.ZoneOverworldHeight)
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
			
			if (player.ZoneCorrupt)
			{
				windFallType = 110;
			}
			else if (player.ZoneDesert)
			{
				windFallType = 813;
			}
			else if (player.ZoneCrimson)
			{
				windFallType = 897;
			}
			else if (player.ZoneSnow)
			{
				windFallType = 705;
			}
			else if (player.ZoneHoly)
			{
				windFallType = 1030;
			}
			else
			{
				windFallType = 386;
			}
			if (Main.rand.Next(15) == 0 && MyWorld.activeEvents.Contains(EventID.heavyWinds) && player.ZoneOverworldHeight)
			{
				float goreScale = 0.01f * Main.rand.Next(20, 70);
				int a = Gore.NewGore(new Vector2(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y + (Main.rand.Next(-1000,-100))), new Vector2(Main.windSpeed*3f, 0f), windFallType, goreScale);
				Main.gore[a].timeLeft = 15;
				Main.gore[a].rotation = 0f;
				Main.gore[a].velocity = new Vector2(Main.windSpeed * 40f, Main.rand.NextFloat(0.2f, 2f));
			}
			else if  (Main.rand.Next(15) == 0 && MyWorld.activeEvents.Contains(EventID.tranquil) && player.ZoneOverworldHeight)
			{
				float goreScale = 0.01f * Main.rand.Next(20, 70);
				int a = Gore.NewGore(new Vector2(player.Center.X + Main.rand.Next(-1000, 1000), player.Center.Y + (Main.rand.Next(-1000,-100))), new Vector2(Main.windSpeed*3f, 0f), 911, goreScale);
				Main.gore[a].timeLeft = 15;
				Main.gore[a].rotation = 0f;
				Main.gore[a].velocity = new Vector2(Main.windSpeed * 40f, Main.rand.NextFloat(0.2f, 2f));
			}
		}
		public override void PostUpdate()
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
		    if (MyWorld.activeEvents.Contains(EventID.tranquil) && player.ZoneOverworldHeight && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneJungle && !player.ZoneHoly) //This needs to be the name of your ModWaterStyle class.
			{
			int off = 5; //Change this value depending on the strength of your light. Too big and it might cause lag, though. Never go above ~20 or so.
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
						Lighting.AddLight(i, j, 0.135f, 0.3f, 0.34f);
					}
				}
			}
			}
			for (int index3 = 0; index3 < 100; ++index3)
			{
				NPC npc = Main.npc[index3];
				if (npc.boss && MyWorld.activeEvents.Contains(EventID.tranquil))
				{					
					MyWorld.activeEvents.Remove(EventID.tranquil);
				}
			}
		}
		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
				return;

			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			/*if (Main.rand.Next (2) == 0)
			{
			caughtType = NPC.NewNPC((int)player.Center.X + (player.direction * 240), (int)player.Center.Y - 10, mod.NPCType("AshSpawn"), 0, 2, 1, 0, 0, Main.myPlayer);
			for (int index1 = 0; index1 < 1000; ++index1)
			{
				if (Main.projectile[index1].active && Main.projectile[index1].owner == player.whoAmI && Main.projectile[index1].bobber)
				{
					Main.projectile[index1].ai[0] = 2f;
				}
			}
			}*/
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(60) == 0)
			{
				caughtType = ItemID.PinkJellyfish;
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(60) == 0)
			{
				caughtType = ItemID.BlueJellyfish;
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(60) == 0 && Main.hardMode)
			{
				caughtType = ItemID.GreenJellyfish;
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(80) == 0)
			{
				caughtType = mod.ItemType("ThermalJelly");
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && Main.rand.Next(16) == 0)
			{
				caughtType = mod.ItemType("StickyJelly");
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(90) == 0)
			{
				caughtType = mod.ItemType("VoltJelly");
			}
			if (player.ZoneBeach && MyWorld.activeEvents.Contains(EventID.Jellyfish) && power >= 50 && Main.rand.Next(100) == 0)
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
			if (Main.rand.Next(10) == 0 && this.netchPotion)
			{
				target.AddBuff(mod.BuffType("Stun"), 240);
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if (proj.thrown && this.voltCell && Main.rand.Next(18)==0 && !target.boss)
			{
				target.AddBuff(mod.BuffType("Stun"), 240);
			}
			if (Main.rand.Next(12) == 0 && this.netchPotion)
			{
				target.AddBuff(mod.BuffType("Stun"), 240);
			}
		}
	}
}