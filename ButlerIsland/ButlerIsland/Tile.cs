using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ButlerIsland
{
    //class for an individual tile
    class Tile
    {
        static public Texture2D TileSetTexture;
        static public int TileWidth = 48;
        static public int TileHeight = 48;

        static public Vector2 originPoint = new Vector2(19, 39);

        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }

    }
}
