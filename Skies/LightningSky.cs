// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.LightningSky
// Assembly: TerrariaServer, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: AA3606A2-F3DB-4481-937B-7295FB97CD3E
// Assembly location: E:\TSHOCK\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Events.Skies
{
  public class LightningSky : CustomSky
  {
    private UnifiedRandom random = new UnifiedRandom();
    private Texture2D boltTexture;
    private Texture2D flashTexture;
    private bool isActive;
    private int ticksUntilNextBolt;
    private float fadeOpacity;
    private LightningSky.Bolt[] bolts;

    public override void OnLoad()
    {
      boltTexture = TextureManager.Load("Images/Misc/VortexSky/Bolt");
      flashTexture = TextureManager.Load("Images/Misc/VortexSky/Flash");
    }

    public override void Update(GameTime gameTime)
    {
      fadeOpacity = !isActive ? Math.Max(0.0f, fadeOpacity - 0.01f) : Math.Min(1f, 0.01f + fadeOpacity);
      if (ticksUntilNextBolt <= 0)
      {
        ticksUntilNextBolt = random.Next(20, 30);
        int index = 0;
        while (bolts[index].IsAlive && index != bolts.Length - 1)
          ++index;
        bolts[index].IsAlive = true;
        bolts[index].Position.X = (float) ((double) random.NextFloat() * ((double) Main.maxTilesX * 16.0 + 4000.0) - 2000.0);
        bolts[index].Position.Y = random.NextFloat() * 500f;
        bolts[index].Depth = (float) ((double) random.NextFloat() * 8.0 + 2.0);
        bolts[index].Life = 30;
      }
      ticksUntilNextBolt = ticksUntilNextBolt - 1;
      for (int index = 0; index < bolts.Length; ++index)
      {
        if (bolts[index].IsAlive)
        {
          --bolts[index].Life;
          if (bolts[index].Life <= 0)
            bolts[index].IsAlive = false;
        }
      }
    }

    public override Color OnTileColor(Color inColor)
    {
      return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, fadeOpacity * 0.5f));
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      float num1 = Math.Min(1f, (float) (((double) Main.screenPosition.Y - 1000.0) / 1000.0));
      Vector2 vector2_3 = Main.screenPosition + new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
      Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
      for (int index = 0; index < bolts.Length; ++index)
      {
        if (bolts[index].IsAlive && (double) bolts[index].Depth > (double) minDepth && (double) bolts[index].Depth < (double) maxDepth)
        {
          Vector2 vector2_1 = new Vector2(1f / bolts[index].Depth, 0.9f / bolts[index].Depth);
          Vector2 position = (bolts[index].Position - vector2_3) * vector2_1 + vector2_3 - Main.screenPosition;
          if (rectangle.Contains((int) position.X, (int) position.Y))
          {
            Texture2D texture = boltTexture;
            int life = bolts[index].Life;
            if (life > 26 && life % 2 == 0)
              texture = flashTexture;
            float num2 = (float) life / 30f;
            spriteBatch.Draw(texture, position, new Rectangle?(), Color.White * num1 * num2 * fadeOpacity, 0.0f, Vector2.Zero, vector2_1.X * 5f, SpriteEffects.None, 0.0f);
          }
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
      bolts = new LightningSky.Bolt[500];
      for (int index = 0; index < bolts.Length; ++index)
        bolts[index].IsAlive = false;
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

    private struct Bolt
    {
      public Vector2 Position;
      public float Depth;
      public int Life;
      public bool IsAlive;
    }
  }
}
