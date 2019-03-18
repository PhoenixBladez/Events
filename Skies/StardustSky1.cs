// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.StardustSky1
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: AA3606A2-F3DB-4481-937B-7295FB97CD3E
// Assembly location: E:\TSHOCK\TerrariaServer.exe

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
  public class StardustSky1 : CustomSky
  {
    private UnifiedRandom random = new UnifiedRandom();
    private Texture2D[] starTextures;
    private bool isActive;
    private StardustSky1.Star[] stars;
    private float fadeOpacity;

    public override void OnLoad()
    {
      starTextures = new Texture2D[2];
      for (int index = 0; index < starTextures.Length; ++index)
        starTextures[index] = TextureManager.Load("Images/Misc/StardustSky/Star " + (object) index);
    }

    public override void Update(GameTime gameTime)
    {
      if (isActive)
        fadeOpacity = Math.Min(1f, 0.01f + fadeOpacity);
      else
        fadeOpacity = Math.Max(0.0f, fadeOpacity - 0.01f);
    }

    public override Color OnTileColor(Color inColor)
    {
      return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, fadeOpacity * 0.5f));
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < stars.Length; ++index)
      {
        float depth = stars[index].Depth;
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
        Vector2 vector2_1 = new Vector2(1f / stars[index].Depth, 1.1f / stars[index].Depth);
        Vector2 position = (stars[index].Position - vector2_3) * vector2_1 + vector2_3 - Main.screenPosition;
        if (rectangle.Contains((int) position.X, (int) position.Y))
        {
          float num4 = (float) Math.Sin((double) stars[index].AlphaFrequency * (double) Main.GlobalTime + (double) stars[index].SinOffset) * stars[index].AlphaAmplitude + stars[index].AlphaAmplitude;
          float num5 = (float) (Math.Sin((double) stars[index].AlphaFrequency * (double) Main.GlobalTime * 5.0 + (double) stars[index].SinOffset) * 0.100000001490116 - 0.100000001490116);
          float num6 = MathHelper.Clamp(num4, 0.0f, 1f);
          Texture2D starTexture = starTextures[stars[index].TextureIndex];
          spriteBatch.Draw(starTexture, position, new Rectangle?(), Color.White * num3 * num6 * 0.8f * (1f - num5) * fadeOpacity, 0.0f, new Vector2((float) (starTexture.Width >> 1), (float) (starTexture.Height >> 1)), (float) (((double) vector2_1.X * 0.5 + 0.5) * ((double) num6 * 0.300000011920929 + 0.699999988079071)), SpriteEffects.None, 0.0f);
        }
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
      int num1 = 200;
      int num2 = 2;
      stars = new StardustSky1.Star[num1 * num2];
      int index1 = 0;
      for (int index2 = 0; index2 < num1; ++index2)
      {
        float num3 = (float) index2 / (float) num1;
        for (int index3 = 0; index3 < num2; ++index3)
        {
          float num4 = (float) index3 / (float) num2;
          stars[index1].Position.X = (float) ((double) num3 * (double) Main.maxTilesX * 16.0);
          stars[index1].Position.Y = (float) ((double) num4 * (Main.worldSurface * 16.0 + 2000.0) - 1000.0);
          stars[index1].Depth = (float) ((double) random.NextFloat() * 8.0 + 1.5);
          stars[index1].TextureIndex = random.Next(starTextures.Length);
          stars[index1].SinOffset = random.NextFloat() * 6.28f;
          stars[index1].AlphaAmplitude = random.NextFloat() * 5f;
          stars[index1].AlphaFrequency = random.NextFloat() + 1f;
          ++index1;
        }
      }
      Array.Sort<StardustSky1.Star>(stars, new Comparison<StardustSky1.Star>(this.SortMethod));
    }

    private int SortMethod(StardustSky1.Star meteor1, StardustSky1.Star meteor2)
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

    private struct Star
    {
      public Vector2 Position;
      public float Depth;
      public int TextureIndex;
      public float SinOffset;
      public float AlphaFrequency;
      public float AlphaAmplitude;
    }
  }
}
