using System;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace Events.Items.AcidRain.Furniture
{
	public class AcidPurifier_Tile : ModTile
	{
		public override void SetDefaults()
		{
			 Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = true;
			animationFrameHeight = 36;	
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16, 16, 16, 16, 16};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table| AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Atmospheric Purifier");
			AddMapEntry(new Color(100, 110, 100), name);
			disableSmartCursor = true;
			dustType = 107;
		}
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (MyWorld.activeEvents.Contains(EventID.acidRain))
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
			if (MyWorld.activeEvents.Contains(EventID.acidRain))
			{
			r = .37f;
			g = .6f;
			b = 0.16f;
			}
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			if (MyWorld.activeEvents.Contains(EventID.acidRain))
			{
               int d  = Dust.NewDust(new Vector2(i*16 + 8, j * 16 ), 0, 0, 107);//Leave this line how it is, it uses int division
				Main.dust[d].velocity *= -1f;
				Main.dust[d].noGravity = true;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-16, 16), (float) Main.rand.Next(16));
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(40, 100) * 0.04f);
				Main.dust[d].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * 22f;
				Main.dust[d].position = new Vector2(i*16 + 8, j * 16 ) - vector2_3;
            }
		}
		public override void SetDrawPositions (int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 3;
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 64, 48, mod.ItemType("AtmosphericPurifierInventory"));
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(3, 4));
		}
		
		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (closer)
			{
				Player player = Main.LocalPlayer;
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
				player.AddBuff(mod.BuffType("AcidPure"), 20);
				if (modPlayer.hazmatHelm == true)
				{
				player.ClearBuff(mod.BuffType("Acid"));
				}
			}
		}
	}
}