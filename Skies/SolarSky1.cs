
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Events.Skies
{
  public class SolarSky1 : CustomSky
  {
    private UnifiedRandom random = new UnifiedRandom();
    private Texture2D meteorTexture;
    private bool isActive;
    private SolarSky1.Meteor[] meteors;
    private float fadeOpacity;

    public override void OnLoad()
    {
      meteorTexture = TextureManager.Load("Images/Misc/SolarSky/Meteor");
    }

    public override void Update(GameTime gameTime)
    {
      fadeOpacity = !isActive ? Math.Max(0.0f, fadeOpacity - 0.01f) : Math.Min(1f, 0.01f + fadeOpacity);
      float num = 1200f;
      for (int index = 0; index < meteors.Length; ++index)
      {
        meteors[index].Position.X -= num * (float) gameTime.ElapsedGameTime.TotalSeconds;
        meteors[index].Position.Y += num * (float) gameTime.ElapsedGameTime.TotalSeconds;
        if ((double) meteors[index].Position.Y > Main.worldSurface * 16.0)
        {
          meteors[index].Position.X = meteors[index].StartX;
          meteors[index].Position.Y = -10000f;
        }
      }
    }

    public override Color OnTileColor(Color inColor)
    {
      return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, fadeOpacity * 0.5f));
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
	  Mod mod = ModLoader.GetMod("Events");
		if (Main.rand.Next (5) == 0)
		{
			meteorTexture =  mod.GetTexture("Images/Misc/Meteor");
		}
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < meteors.Length; ++index)
      {
        float depth = meteors[index].Depth;
        if (num1 == -1 && (double) depth < (double) maxDepth)
          num1 = index;
        if ((double) depth > (double) minDepth)
          num2 = index;
        else
          break;
      }
      if (num1 == -1)
        return;
      float num3 = Math.Min(1f, (float) (((double) Main.screenPosition.Y - 1000.0) / 1000.0));
      Vector2 vector2_3 = Main.screenPosition + new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
      Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
      for (int index = num1; index < num2; ++index)
      {
        Vector2 vector2_1 = new Vector2(1f / meteors[index].Depth, 0.9f / meteors[index].Depth);
        Vector2 position = (meteors[index].Position - vector2_3) * vector2_1 + vector2_3 - Main.screenPosition;
        int num4 = meteors[index].FrameCounter / 3;
        meteors[index].FrameCounter = (meteors[index].FrameCounter + 1) % 12;
        if (rectangle.Contains((int) position.X, (int) position.Y))
          spriteBatch.Draw(meteorTexture, position, new Rectangle?(new Rectangle(0, num4 * (meteorTexture.Height / 4), meteorTexture.Width, meteorTexture.Height / 4)), Color.White * num3 * fadeOpacity, 0.0f, Vector2.Zero, vector2_1.X * 5f * meteors[index].Scale, SpriteEffects.None, 0.0f);
      }
    }

    public override float GetCloudAlpha()
    {
      return (float) ((1.0 - (double) fadeOpacity) * 0.300000011920929 + 0.699999988079071);
    }

    public override void Activate(Vector2 position, params object[] args)
    {

      fadeOpacity = 1f / 500f;
      isActive = true;
      meteors = new SolarSky1.Meteor[50];
      for (int index = 0; index < meteors.Length; ++index)
      {
        float num = (float) index / (float) meteors.Length;
        meteors[index].Position.X = (float) ((double) num * ((double) Main.maxTilesX * 16.0) + (double) random.NextFloat() * 40.0 - 20.0);
        meteors[index].Position.Y = (float) ((double) random.NextFloat() * -(Main.worldSurface * 16.0 + 10000.0) - 10000.0);
        meteors[index].Depth = random.Next(3) == 0 ? (float) ((double) random.NextFloat() * 5.0 + 4.80000019073486) : (float) ((double) random.NextFloat() * 3.0 + 1.79999995231628);
        meteors[index].FrameCounter = random.Next(12);
        meteors[index].Scale = (float) ((double) random.NextFloat() * 0.5 + 1.0);
        meteors[index].StartX = meteors[index].Position.X;
      }
      Array.Sort<SolarSky1.Meteor>(meteors, new Comparison<SolarSky1.Meteor>(this.SortMethod));
    }

    private int SortMethod(SolarSky1.Meteor meteor1, SolarSky1.Meteor meteor2)
    {
      return meteor2.Depth.CompareTo(meteor1.Depth);
    }

    public override void Deactivate(params object[] args)
    {
      isActive = false;
    }

    public override void Reset()
    {
      isActive = false;
    }

    public override bool IsActive()
    {
      if (!isActive)
        return (double) fadeOpacity > 1.0 / 1000.0;
      return true;
    }

    private struct Meteor
    {
      public Vector2 Position;
      public float Depth;
      public int FrameCounter;
      public float Scale;
      public float StartX;
    }
  }
}
