using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.Skies;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Initializers;
using Terraria.IO;
using Terraria.GameContent;
using Terraria.ModLoader;
using System.Linq;
using Terraria.UI;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.GameContent.Events;
using Events;
using Events.Skies;
using Events.Overlays;


namespace Events
{
    enum PacketType
    {
        StartEvent,
        EndEvent,
        AuroraData,
        FireflyData
    }
	class Events : Mod
	{
        public static Effect auroraEffect;
        public static Texture2D noise;		
		public override void Load()
		{
			if (!Main.dedServ) 
			{
				
			Player player = Main.LocalPlayer;
			Filters.Scene["Events:Meteor"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.5f, 0.5f, 0.5f).UseOpacity(0f), EffectPriority.VeryHigh);
			SkyManager.Instance["Events:Meteor"] = new SolarSky1();
			
			Filters.Scene["Events:Stardust"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0.5f, 0.5f, 0.5f).UseOpacity(0f), EffectPriority.VeryHigh);
			SkyManager.Instance["Events:Stardust"] = new LightningSky();
			
			Filters.Scene["Events:AcidRain"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(0.5f, 1f, .25f).UseOpacity(1.25f), EffectPriority.Medium);
			Filters.Scene["Events:HeatWave"] = new Filter(new ScreenShaderData("FilterHeatDistortion").UseImage("Images/Misc/noise", 0, (SamplerState) null).UseIntensity(4f), EffectPriority.Medium);
			Filters.Scene["Events:WindySky"] = new Filter((new BlizzardShaderData("FilterBlizzardForeground")).UseColor(0.4f, 0.4f, 0.4f).UseSecondaryColor(0.2f, 0.2f, 0.2f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.099f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
			SkyManager.Instance["Events:Ashstorm"] = new AshstormSky();

            Filters.Scene["Events:Ashstorm"] = new Filter((new BlizzardShaderData("FilterBlizzardForeground")).UseColor(0.4f, 0.4f, 0.4f).UseSecondaryColor(0.2f, 0.2f, 0.2f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.07f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
            //Literally only here so the game doesnt crash
            Filters.Scene["Events:AshstormParticles"] = new Filter((new ScreenShaderData("FilterMiniTower")).UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryLow);

            Terraria.Graphics.Effects.Overlays.Scene["Events:Ashstorm"] = new SimpleOverlay("Images/Misc/noise", (new BlizzardShaderData("FilterBlizzardBackground")).UseColor(0.4f, 0.4f, 0.4f).UseSecondaryColor(0.2f, 0.2f, 0.2f).UseImage("Images/Misc/noise", 0, null).UseOpacity(0.05f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High, RenderLayers.Landscape);
            Terraria.Graphics.Effects.Overlays.Scene["Events:AshstormParticles"] = new AshstormOverlay(EffectPriority.VeryHigh);
			
			auroraEffect = GetEffect("Effects/aurora");
            noise = GetTexture("Textures/noise");

            //filler stuff
            SkyManager.Instance["Events:AuroraSky"] = new AuroraSky();
            Filters.Scene["Events:AuroraSky"] = new Filter((new ScreenShaderData("FilterMiniTower")).UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryLow);

            //the actually important thing
            Terraria.Graphics.Effects.Overlays.Scene["Events:AuroraSky"] = new AuroraOverlay(EffectPriority.VeryHigh);
			}
			base.Load();
		}
		 public override void Unload()
        {
            auroraEffect = null;
            noise = null;
        }
		
		public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.gameMenu)
				return;
			if (priority > MusicPriority.Event)
				return;
			Player player = Main.LocalPlayer;
			if (!player.active)
				return;
			MyPlayer myplayer = player.GetModPlayer<MyPlayer>();
			if (MyWorld.activeEvents.Contains(EventID.aurora) && player.ZoneSnow && player.ZoneOverworldHeight)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Aurora");
				priority = MusicPriority.Environment;
			}
			if (MyWorld.activeEvents.Contains(EventID.Jellyfish) && player.ZoneBeach && player.ZoneOverworldHeight)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Jellyfish");
				priority = MusicPriority.Environment;
			}
			if (MyWorld.activeEvents.Contains(EventID.acidRain) && player.ZoneOverworldHeight)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/AcidRain");
				priority = MusicPriority.Event;
			}			
			if (MyWorld.activeEvents.Contains(EventID.ashStorm) && player.ZoneOverworldHeight)
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/AshStorm");
				priority = MusicPriority.Event;
			}
			if (MyWorld.activeEvents.Contains(EventID.tranquil) && player.ZoneOverworldHeight && !player.ZoneBeach && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneJungle && !player.ZoneHoly && !player.ZoneSnow) 
			{
				music = GetSoundSlot(SoundType.Music, "Sounds/Music/Tranquil");
				priority = MusicPriority.Environment;				
			}
			
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
						Mod mod = ModLoader.GetMod("Events");
									Player player = Main.LocalPlayer;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
            if (MyWorld.activeEvents.Count > 0 && modPlayer.weatherTech == true)
            {
                int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                LegacyGameInterfaceLayer eventThing = new LegacyGameInterfaceLayer("event:thing",
                    delegate
                    {
                        DrawEventAcc(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index, eventThing);
            }
        }
        public void DrawEventAcc(SpriteBatch spriteBatch)
        {
            float offsetY = 0;
            for (int i = 0; i < MyWorld.activeEvents.Count; i++)
            {
                int eventType = MyWorld.activeEvents.ElementAt(i);
                String icon = eventType.ToString();
                Texture2D EventIcon = GetTexture("Icons/" + icon);
                String eventName = EventID.nameString[eventType] + " detected";
                Rectangle textPlace = Utils.CenteredRectangle(new Vector2(Main.screenWidth - 365, offsetY + 112f), new Vector2(100, 50));
                spriteBatch.Draw(EventIcon, new Vector2(Main.screenWidth  - 476, offsetY + 88f), Color.White);
                Utils.DrawBorderString(spriteBatch, eventName, new Vector2(textPlace.X + textPlace.Width / 2.4f, textPlace.Y + 5), Color.White, 0.8f, 0.5f, -0.1f);
                offsetY += 30f;
            }
        }
		public int screenshakeTimer = 0;
		public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
		{
			 if (!Main.gameMenu && MyWorld.activeEvents.Contains(EventID.tremors))
             {
                   screenshakeTimer++;
                   if (MyWorld.screenshakeAmount >= 0 && screenshakeTimer >= 5) // so it doesnt immediately decrease
                   {
                       MyWorld.screenshakeAmount -= 0.1f;
                   }
                   if (MyWorld.screenshakeAmount < 0)
                   {
                      MyWorld.screenshakeAmount = 0;
                   }
                   Main.screenPosition += new Vector2(MyWorld.screenshakeAmount * Main.rand.NextFloat(), MyWorld.screenshakeAmount * Main.rand.NextFloat()); //NextFloat creates a random value between 0 and 1, multiply screenshake amount for a bit of variety
              }
              else // dont shake on the menu
              {
                   MyWorld.screenshakeAmount = 0;
                   screenshakeTimer = 0;
              }
        }
		public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar" + Lang.GetItemNameValue(ItemType("Adamantite Bar")), new int[]
			{
				391,
				1198
			});
			RecipeGroup.RegisterGroup("AdamantiteBars", group);
			group = new RecipeGroup(() => Lang.misc[37] + " Iron or Lead Bars" + Lang.GetItemNameValue(ItemType("Lead Bar")), new int[]
			{
				22,
				704
			});
			RecipeGroup.RegisterGroup("LeadBar", group);
		}
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            //todo: switch to enum system for this instead of just bytes.
            PacketType packetType = (PacketType)reader.ReadByte();
            switch(packetType)
            {
                case PacketType.StartEvent:
                    int eventID = reader.ReadInt32();
                    MyWorld.activeEvents.Add(eventID);
                    switch(eventID)
                    {
                        case EventID.tremors:
                            MyWorld.screenshakeAmount = 5f;
                            break;
                        case EventID.butterflies:
                            NPC.butterflyChance = 2;
                            break;
                    }
                    break;
                case PacketType.EndEvent:
                    MyWorld.activeEvents.Remove(reader.ReadInt32());
                    break;
                case PacketType.AuroraData:
                    MyWorld.auroraType = reader.ReadInt32();
                    break;
                case PacketType.FireflyData:
                    NPC.fireFlyChance = reader.ReadInt32();
                    NPC.fireFlyMultiple = reader.ReadInt32();
                    break;
            }
        }
    }
}
