using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Events.Items.AcidRain.Toxictop
{
    public class ToxictopTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
			Main.tileFrameImportant[Type] = true;
            //TileObjectData.addTile(Type);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Toxictop");
			drop = mod.ItemType("ToxictopItem");
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(196, 33, 193), name);
			dustType = 69;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16
            };

            TileObjectData.addTile(Type);
        }
		
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
      public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			 Main.PlaySound(3, i * 16, j * 16, 19);
             
        }
		public override void SetDrawPositions (int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
	}
}
