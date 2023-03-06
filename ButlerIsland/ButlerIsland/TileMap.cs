using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ButlerIsland
{
    //class for the columns of the map
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }
    //class for the actual map itself
    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 17;
        public int MapHeight = 14;
        private string levelStage = "Tutorial";
        Random random = new Random();
        public string getLevelStage()
        {
            return levelStage;
        }
        public TileMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(4));
                }
                Rows.Add(thisRow);
            }
            updateMap();
            wallsDoorsWindows();
        }
        public TileMap(int width, int height)
        {
            MapWidth = width;
            MapHeight = height;
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(4));
                }
                Rows.Add(thisRow);
            }
            updateMap();
        wallsDoorsWindows();
        }
        public void initialize()
        {
            if (levelStage == "Tutorial")
            {
                tutorialStage();
            }
            if (levelStage == "StageOne")
            {
                stageOne();
            }
            if (levelStage == "StageTwo")
            {
                stageTwo();
            }
            updateMap();
        }
        public void changeMap(string name)
        {
            this.levelStage = name;
            initialize();
        }
        private void tutorialStage()
        {
            //chairs everywhere
            Rows[2].Columns[3].AddBaseTile(96);
            Rows[3].Columns[3].AddBaseTile(96);
            Rows[4].Columns[3].AddBaseTile(96);
            Rows[2].Columns[5].AddBaseTile(97);
            Rows[3].Columns[5].AddBaseTile(97);
            Rows[4].Columns[5].AddBaseTile(97);


            //a table
            Rows[2].Columns[3].AddHeightTile(60);
            Rows[2].Columns[4].AddHeightTile(61);
            Rows[2].Columns[5].AddHeightTile(62);
            Rows[3].Columns[3].AddHeightTile(72);
            Rows[3].Columns[4].AddHeightTile(73);
            Rows[3].Columns[5].AddHeightTile(74);
            Rows[4].Columns[3].AddHeightTile(84);
            Rows[4].Columns[4].AddHeightTile(85);
            Rows[4].Columns[5].AddHeightTile(86);

            //more chairs
            Rows[6].Columns[3].AddBaseTile(96);
            Rows[7].Columns[3].AddBaseTile(96);
            Rows[8].Columns[3].AddBaseTile(96);
            Rows[6].Columns[5].AddBaseTile(97);
            Rows[7].Columns[5].AddBaseTile(97);
            Rows[8].Columns[5].AddBaseTile(97);

            //another table
            Rows[6].Columns[3].AddHeightTile(60);
            Rows[6].Columns[4].AddHeightTile(61);
            Rows[6].Columns[5].AddHeightTile(62);
            Rows[7].Columns[3].AddHeightTile(72);
            Rows[7].Columns[4].AddHeightTile(73);
            Rows[7].Columns[5].AddHeightTile(74);
            Rows[8].Columns[3].AddHeightTile(84);
            Rows[8].Columns[4].AddHeightTile(85);
            Rows[8].Columns[5].AddHeightTile(86);

            //third table
            Rows[4].Columns[9].AddHeightTile(60);
            Rows[4].Columns[10].AddHeightTile(61);
            Rows[4].Columns[11].AddHeightTile(62);
            Rows[5].Columns[9].AddHeightTile(72);
            Rows[5].Columns[10].AddHeightTile(73);
            Rows[5].Columns[11].AddHeightTile(74);
            Rows[6].Columns[9].AddHeightTile(84);
            Rows[6].Columns[10].AddHeightTile(85);
            Rows[6].Columns[11].AddHeightTile(86);

            //more chairs
            Rows[4].Columns[9].AddBaseTile(96);
            Rows[5].Columns[9].AddBaseTile(96);
            Rows[6].Columns[9].AddBaseTile(96);
            Rows[4].Columns[11].AddBaseTile(97);
            Rows[5].Columns[11].AddBaseTile(97);
            Rows[6].Columns[11].AddBaseTile(97);
            
        }
        private void stageOne()
        {
            //MapWidth = 20;
            //MapHeight = 17;

            Rows[3].Columns[3].AddHeightTile(60);
            Rows[3].Columns[4].AddHeightTile(61);
            Rows[3].Columns[5].AddHeightTile(62);
            Rows[4].Columns[3].AddHeightTile(72);
            Rows[4].Columns[4].AddHeightTile(73);
            Rows[4].Columns[5].AddHeightTile(74);
            Rows[5].Columns[3].AddHeightTile(72);
            Rows[5].Columns[4].AddHeightTile(73);
            Rows[5].Columns[5].AddHeightTile(74);
            Rows[6].Columns[3].AddHeightTile(84);
            Rows[6].Columns[4].AddHeightTile(85);
            Rows[6].Columns[5].AddHeightTile(86);

            Rows[3].Columns[3].AddBaseTile(96);
            Rows[4].Columns[2].AddBaseTile(96);
            Rows[5].Columns[3].AddBaseTile(96);
            Rows[6].Columns[3].AddBaseTile(96);
            Rows[3].Columns[5].AddBaseTile(97);
            Rows[4].Columns[5].AddBaseTile(97);
            Rows[5].Columns[6].AddBaseTile(97);
            Rows[6].Columns[5].AddBaseTile(97);

            //third set of tables/chairs
            Rows[6].Columns[9].AddHeightTile(60);
            Rows[6].Columns[10].AddHeightTile(62);
            Rows[7].Columns[9].AddHeightTile(84);
            Rows[7].Columns[10].AddHeightTile(86);

            Rows[6].Columns[9].AddBaseTile(96);
            Rows[6].Columns[11].AddBaseTile(97);
            Rows[7].Columns[9].AddBaseTile(96);
            Rows[7].Columns[10].AddBaseTile(97);

            //more chairs
            Rows[9].Columns[3].AddBaseTile(96);
            Rows[10].Columns[4].AddBaseTile(96);
            Rows[11].Columns[4].AddBaseTile(96);
            Rows[9].Columns[6].AddBaseTile(97);
            Rows[10].Columns[6].AddBaseTile(97);
            Rows[11].Columns[6].AddBaseTile(97);

            //another table
            Rows[9].Columns[4].AddHeightTile(60);
            Rows[9].Columns[5].AddHeightTile(61);
            Rows[9].Columns[6].AddHeightTile(62);
            Rows[10].Columns[4].AddHeightTile(72);
            Rows[10].Columns[5].AddHeightTile(73);
            Rows[10].Columns[6].AddHeightTile(74);
            Rows[11].Columns[4].AddHeightTile(84);
            Rows[11].Columns[5].AddHeightTile(85);
            Rows[11].Columns[6].AddHeightTile(86);
        }
        public void stageTwo()
        {
            //fat tabnle
            Rows[3].Columns[3].AddHeightTile(60);
            Rows[3].Columns[4].AddHeightTile(61);
            Rows[3].Columns[5].AddHeightTile(61);
            Rows[3].Columns[6].AddHeightTile(62);
            Rows[4].Columns[3].AddHeightTile(72);
            Rows[4].Columns[4].AddHeightTile(73);
            Rows[4].Columns[5].AddHeightTile(73);
            Rows[4].Columns[6].AddHeightTile(74);
            Rows[5].Columns[3].AddHeightTile(72);
            Rows[5].Columns[4].AddHeightTile(73);
            Rows[5].Columns[5].AddHeightTile(73);
            Rows[5].Columns[6].AddHeightTile(74);
            Rows[6].Columns[3].AddHeightTile(84);
            Rows[6].Columns[4].AddHeightTile(85);
            Rows[6].Columns[5].AddHeightTile(85);
            Rows[6].Columns[6].AddHeightTile(86);

            Rows[3].Columns[2].AddBaseTile(96);
            Rows[4].Columns[3].AddBaseTile(96);
            Rows[5].Columns[2].AddBaseTile(96);
            Rows[6].Columns[3].AddBaseTile(96);
            Rows[3].Columns[7].AddBaseTile(97);
            Rows[4].Columns[6].AddBaseTile(97);
            Rows[5].Columns[6].AddBaseTile(97);
            Rows[6].Columns[6].AddBaseTile(97);

            //skinny table
            Rows[2].Columns[11].AddHeightTile(60);
            Rows[2].Columns[12].AddHeightTile(62);
            Rows[3].Columns[11].AddHeightTile(72);
            Rows[3].Columns[12].AddHeightTile(74);
            Rows[4].Columns[11].AddHeightTile(72);
            Rows[4].Columns[12].AddHeightTile(74);
            Rows[5].Columns[11].AddHeightTile(72);
            Rows[5].Columns[12].AddHeightTile(74);
            Rows[6].Columns[11].AddHeightTile(72);
            Rows[6].Columns[12].AddHeightTile(74);
            Rows[7].Columns[11].AddHeightTile(72);
            Rows[7].Columns[12].AddHeightTile(74);
            Rows[8].Columns[11].AddHeightTile(72);
            Rows[8].Columns[12].AddHeightTile(74);
            Rows[9].Columns[11].AddHeightTile(72);
            Rows[9].Columns[12].AddHeightTile(74);
            Rows[10].Columns[11].AddHeightTile(84);
            Rows[10].Columns[12].AddHeightTile(86);

            Rows[2].Columns[11].AddBaseTile(96);
            Rows[2].Columns[12].AddBaseTile(97);
            Rows[3].Columns[11].AddBaseTile(96);
            Rows[3].Columns[12].AddBaseTile(97);
            Rows[4].Columns[11].AddBaseTile(96);
            Rows[4].Columns[12].AddBaseTile(97);
            Rows[5].Columns[11].AddBaseTile(96);
            Rows[5].Columns[12].AddBaseTile(97);
            Rows[6].Columns[11].AddBaseTile(96);
            Rows[6].Columns[12].AddBaseTile(97);
            Rows[7].Columns[11].AddBaseTile(96);
            Rows[7].Columns[12].AddBaseTile(97);
            Rows[8].Columns[11].AddBaseTile(96);
            Rows[8].Columns[12].AddBaseTile(97);
            Rows[9].Columns[11].AddBaseTile(96);
            Rows[9].Columns[12].AddBaseTile(97);
            Rows[10].Columns[11].AddBaseTile(96);
            Rows[10].Columns[13].AddBaseTile(97);

            //horizontal skinny
            Rows[10].Columns[3].AddHeightTile(60);
            Rows[10].Columns[4].AddHeightTile(61);
            Rows[10].Columns[5].AddHeightTile(61);
            Rows[10].Columns[6].AddHeightTile(61);
            Rows[10].Columns[7].AddHeightTile(61);
            Rows[10].Columns[8].AddHeightTile(62);
            Rows[11].Columns[3].AddHeightTile(84);
            Rows[11].Columns[4].AddHeightTile(85);
            Rows[11].Columns[5].AddHeightTile(85);
            Rows[11].Columns[6].AddHeightTile(85);
            Rows[11].Columns[7].AddHeightTile(85);
            Rows[11].Columns[8].AddHeightTile(86);

            Rows[10].Columns[3].AddBaseTile(96);
            Rows[10].Columns[8].AddBaseTile(97);
            Rows[11].Columns[3].AddBaseTile(96);
            Rows[11].Columns[8].AddBaseTile(97);
        }
        public void wallsDoorsWindows()
        {
            Rows[0].Columns[4].AddHeightTile(101);
            //put walls around the edges
            Rows[0].Columns[0].AddBaseTile(18);
            Rows[0].Columns[MapWidth - 1].AddBaseTile(16);
            Rows[MapHeight - 1].Columns[MapWidth - 1].AddBaseTile(17);
            Rows[MapHeight - 1].Columns[0].AddBaseTile(19);
            for (int i = 1; i < MapWidth; i++)
            {
                Rows[0].Columns[i].AddBaseTile(13);
                Rows[MapHeight - 1].Columns[i].AddBaseTile(12);
                Rows[0].Columns[i].Walkable = false;
                Rows[MapHeight - 1].Columns[i].Walkable = false;
                if (i == random.Next(2, MapWidth - 1) && i != 4)
                {
                    Rows[0].Columns[i].AddHeightTile(98);
                }
            }
            for (int i = 1; i < MapHeight; i++)
            {
                Rows[i].Columns[0].AddBaseTile(15);
                Rows[i].Columns[MapWidth - 1].AddBaseTile(14);
                Rows[i].Columns[0].Walkable = false;
                Rows[i].Columns[MapWidth - 1].Walkable = false;
                if (i == random.Next(2, MapHeight - 1))
                {
                    Rows[i].Columns[0].AddHeightTile(99);
                }
                if (i == random.Next(2, MapHeight - 1))
                {
                    Rows[i].Columns[MapWidth -1].AddHeightTile(100);
                }
            }
        }
        public bool updateMap()
        {
            int chairsOut = 0;
            //Sets the map straight
            for (int i = 1; i < MapWidth; i++)
            {
                for (int j = 1; j < MapHeight; j++)
                {
                    Rows[j].Columns[i].sitChair = false;
                    Rows[j].Columns[i].Chair = false;
                    if (Rows[j].Columns[i].BaseTiles.Contains(96) ||
                        Rows[j].Columns[i].BaseTiles.Contains(96) ||
                        Rows[j].Columns[i].BaseTiles.Contains(96))
                    {
                        Rows[j].Columns[i].Chair = true;
                    }
                    if (Rows[j].Columns[i].BaseTiles.Contains(97) ||
                        Rows[j].Columns[i].BaseTiles.Contains(97) ||
                        Rows[j].Columns[i].BaseTiles.Contains(97))
                    {
                        Rows[j].Columns[i].Chair = true;
                    }
                    
                    if (Rows[j].Columns[i].BaseTiles.Contains(96) || Rows[j].Columns[i].BaseTiles.Contains(97))
                    {
                        Rows[j].Columns[i].Pushable = true;
                    }
                    if(Rows[j].Columns[i].HeightTiles.Contains(60) || Rows[j].Columns[i].HeightTiles.Contains(72) || Rows[j].Columns[i].HeightTiles.Contains(84) ||
                       Rows[j].Columns[i].HeightTiles.Contains(62) || Rows[j].Columns[i].HeightTiles.Contains(74) || Rows[j].Columns[i].HeightTiles.Contains(86))
                    {
                        Rows[j].Columns[i].Pushable = false;
                    }
                    
                    Rows[j].Columns[i].Walkable = true;
                    if (Rows[j].Columns[i].BaseTiles.Contains(96) || Rows[j].Columns[i].BaseTiles.Contains(97) ||
                        Rows[j].Columns[i].HeightTiles.Contains(60) || Rows[j].Columns[i].HeightTiles.Contains(61) ||
                        Rows[j].Columns[i].HeightTiles.Contains(62) || Rows[j].Columns[i].HeightTiles.Contains(72) ||
                        Rows[j].Columns[i].HeightTiles.Contains(73) || Rows[j].Columns[i].HeightTiles.Contains(74) ||
                        Rows[j].Columns[i].HeightTiles.Contains(84) || Rows[j].Columns[i].HeightTiles.Contains(85) ||
                        Rows[j].Columns[i].HeightTiles.Contains(86))
                    {
                        Rows[j].Columns[i].Walkable = false;
                    }
                    Rows[j].Columns[i].Table = false;
                    if (Rows[j].Columns[i].HeightTiles.Contains(60) || Rows[j].Columns[i].HeightTiles.Contains(61) ||
                        Rows[j].Columns[i].HeightTiles.Contains(62) || Rows[j].Columns[i].HeightTiles.Contains(72) ||
                        Rows[j].Columns[i].HeightTiles.Contains(73) || Rows[j].Columns[i].HeightTiles.Contains(74) ||
                        Rows[j].Columns[i].HeightTiles.Contains(84) || Rows[j].Columns[i].HeightTiles.Contains(85) ||
                        Rows[j].Columns[i].HeightTiles.Contains(86))
                    {
                        Rows[j].Columns[i].Table = true;
                    }
                    if (Rows[j].Columns[i].Chair == true &&  Rows[j].Columns[i].Table == false)
                    {
                            chairsOut += 1;                        
                    }
                }
            }
            if (chairsOut >= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //allows player to push in chair
        public void pushChair(Vector2 worldPoint)
        {
            if (GetCellAtWorldPoint(worldPoint).BaseTiles.Contains(96))
            {
                GetCellAtWorldPoint(worldPoint).BaseTiles.Remove(96);
                GetCellAtWorldPoint(worldPoint - new Vector2(-48, 0)).AddBaseTile(96);
            }
            if (GetCellAtWorldPoint(worldPoint).BaseTiles.Contains(97))
            {
                GetCellAtWorldPoint(worldPoint).BaseTiles.Remove(97);
                GetCellAtWorldPoint(worldPoint - new Vector2(48, 0)).AddBaseTile(97);
            }
            GetCellAtWorldPoint(worldPoint).Pushable = false;
        }
        //allows ai to pull out chairs
        public int pullChair(Vector2 worldPoint)
        {
            if (GetCellAtWorldPoint(worldPoint).BaseTiles.Contains(96))
            {
                GetCellAtWorldPoint(worldPoint).BaseTiles.Remove(96);
                GetCellAtWorldPoint(worldPoint - new Vector2(48, 0)).AddBaseTile(96);
                return 1;
            }
            if (GetCellAtWorldPoint(worldPoint).BaseTiles.Contains(97))
            {
                GetCellAtWorldPoint(worldPoint).BaseTiles.Remove(97);
                GetCellAtWorldPoint(worldPoint - new Vector2(-48, 0)).AddBaseTile(97);
                return 2;
            }
            else
            {
                return 0;
            }
        }
        //returns position from pixels to tiles
        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / Tile.TileHeight) - 1,
               ((int)(worldPoint.Y / Tile.TileWidth) - 1)
               );

            int localPointX = worldPoint.X % 48;
            int localPointY = worldPoint.Y % 48;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }
        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }
        public Point WorldToMapCell(Vector2 worldPoint)
        {
            return WorldToMapCell(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }
        public MapCell GetCellAtWorldPoint(Point worldPoint)
        {
            Point mapPoint = WorldToMapCell(worldPoint);
            return Rows[mapPoint.Y].Columns[mapPoint.X];
        }        
        public MapCell GetCellAtWorldPoint(Vector2 worldPoint)
        {
            return GetCellAtWorldPoint(new Point((int)worldPoint.X, (int)worldPoint.Y));
        }
    }
}
