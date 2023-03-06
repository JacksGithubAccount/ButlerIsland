using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ButlerIsland
{
    //classthat holds lighting of the screen(gamma)
    class ScreenLighting
    {
        int brightness;
        int contrast;

        BlendState brightnessBlend;
        BlendState contrastBlend;
        Texture2D whiteTexture;

        public ScreenLighting(Texture2D whiteTexture)
        {
            this.whiteTexture = whiteTexture;

            brightness = 255;
            contrast = 128;

            brightnessBlend = new BlendState();
            brightnessBlend.ColorSourceBlend = brightnessBlend.AlphaSourceBlend = Blend.Zero;
            brightnessBlend.ColorDestinationBlend = brightnessBlend.AlphaDestinationBlend = Blend.SourceColor;

            contrastBlend = new BlendState();
            contrastBlend.ColorSourceBlend = contrastBlend.AlphaSourceBlend = Blend.DestinationColor;
            contrastBlend.ColorDestinationBlend = contrastBlend.AlphaDestinationBlend = Blend.SourceColor;

        }
        public void changeLighting(int brightness, int contrast)
        {
            this.brightness = brightness;
            this.contrast = contrast;
        }
        public void draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            //spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, 48 * 21, 48 * 21), new Color(brightness, brightness, brightness, 255));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, contrastBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, 48 * 21, 48 * 21), new Color(contrast, contrast, contrast, 255));
            spriteBatch.End();
            
            graphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}
