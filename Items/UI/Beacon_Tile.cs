using System;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Reflection;
using Events;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace Events.Items.UI
{
	public class Beacon_Tile : ModTile
	{
		public override void SetDefaults()
		{
			 Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = true;
			animationFrameHeight = 54;	
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16, 16, 16, 16, 16};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table| AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Meteorological Scanner");
			AddMapEntry(new Color(162, 182, 214), name);
			disableSmartCursor = true;
			dustType = 226;
		}
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (MyWorld.activeEvents.Count > 0)
			{
			frameCounter++;
			if(frameCounter >= 8) //replace 10 with duration of frame in ticks
			{
				frameCounter = 0;
				frame++;
				frame %= 7;
			}
			}
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (MyWorld.activeEvents.Count > 0)
			{
			r = .07f;
			g = .3f;
			b = 0.5f;
			}
		}
	
		public override void SetDrawPositions (int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1: 3;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 64, 48, mod.ItemType("BeaconItem"));
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(3, 4));
		}
		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			if (closer)
			{
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
				player.AddBuff(mod.BuffType("BeaconBuff"), 20);
				if (MyWorld.dayTimeSwitched)
				{
					if (MyWorld.activeEvents.Count > 0)
					{
					Main.PlaySound(SoundLoader.customSoundType, player.position, mod.GetSoundSlot(SoundType.Custom, "Sounds/BeaconActive"));
					}		
				}
			}
		}
	}
}