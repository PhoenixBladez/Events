using System;
using System.Runtime.Serialization.Formatters.Binary;
using Events;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Reflection;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace Events.Items.UI
{
	public class BrokenBeacon_Tile : ModTile
	{
		public override void SetDefaults()
		{
			 Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16, 16, 16, 16, 16};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table| AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Broken Beacon");
			AddMapEntry(new Color(162, 182, 214), name);
			disableSmartCursor = true;
			dustType = 226;
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
			Item.NewItem(i * 16, j * 16, 64, 48, mod.ItemType("BrokenBeacon"));
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(3, 4));
		}
		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				if (Main.rand.Next(20) ==0)
				{
				int d  = Dust.NewDust(new Vector2(i*16 , j * 16 -10), 0, 16, 226, 0.0f, -1, 0, new Color(), 0.5f);//Leave this line how it is, it uses int division
				
				Main.dust[d].velocity *= .8f;
				Main.dust[d].noGravity = true;
				}
				if (Main.rand.Next(700) == 0)
				{
					Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 93));
				}
			}
		}
	}
}