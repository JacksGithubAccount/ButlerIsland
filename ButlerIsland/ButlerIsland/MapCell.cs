using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButlerIsland
{
    //class for an individual tile by cell
    class MapCell
    {
        public List<int> BaseTiles = new List<int>();
        public List<int> HeightTiles = new List<int>();

        public bool Walkable { get; set; }
        public bool Pushable { get; set; }
        public bool Chair { get; set; }
        public bool Table { get; set; }
        public bool sitChair { get; set; }

        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        public void AddBaseTile(int tileID)
        {
            BaseTiles.Add(tileID);
        }
        public void AddHeightTile(int tileID)
        {
            HeightTiles.Add(tileID);
        }
        public MapCell(int tileID)
        {
            TileID = tileID;
            Walkable = true;
        }
    }
}
