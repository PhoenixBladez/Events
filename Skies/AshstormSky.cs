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

namespace Events.Skies
{
    public class AshstormSky : CustomSky
    {
        private bool skyActive;
        private float opacity;

        public AshstormSky()
        {
        }

        public override void Deactivate(params object[] args)
        {
            skyActive = false;
        }

        public override void Reset()
        {
            skyActive = false;
        }

        public override bool IsActive()
        {
            return skyActive || opacity > 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            skyActive = true;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            //sky
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(0.3f, 0.3f, 0.3f) * opacity);
            }
            //front of bg
            if (maxDepth >= 0 && minDepth < 0)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(0.3f, 0.3f, 0.3f) * 0.8f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (skyActive && opacity < 1f)
            {
                opacity += 0.02f;
            }
            else if (!skyActive && opacity > 0f)
            {
                opacity -= 0.02f;
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            float amt = opacity * .01f;
            return inColor.MultiplyRGB(new Color(1f - amt, 1f - amt, 1f - amt));
        }

        public override float GetCloudAlpha()
        {
            return (1f - opacity) * 0.97f + 0.03f;
        }
    }
}