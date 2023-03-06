using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace ButlerIsland
{
    //Class that controls the artifical intelligence
    class TrollBrain
    {
        TileMap myMap;

        Random randomChair = new Random();
        int isPulledChair = 0;
        double sitTimer = 3000;
        float xSpeed = 2;
        float ySpeed = 2;
        int tableDir = 0;
        bool goleft = false;
        bool goright = false;
        bool gonorth = false;
        bool gosouth = false;
        bool hasChair = false;
        bool iscorner = false;
        bool sitChair = false;
        bool atTable = false;
        int[] destinationChair = new int[3];
        List<int> Xcoord = new List<int>();
        List<int> Ycoord = new List<int>();
        Random random = new Random();
        SpriteAnimation troll;
        SpriteAnimation vlad;
        string name = "Troll";

        //constructor
        public TrollBrain(TileMap tileMap,SpriteAnimation Troll, string name)
        {
            this.myMap = tileMap;
            this.troll = Troll;
            this.name = name;
            

            if (name == "Troll")
            {
                sitTimer = 3000;
                xSpeed = 2;
                ySpeed = 2;
                troll.Position = new Vector2(470, 345);
            }
            if (name == "Sloth")
            {
                sitTimer = 4500;
                xSpeed = 1;
                ySpeed = 1;
                troll.Position = new Vector2(470, 145);
            }
            troll.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            troll.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            troll.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            troll.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            troll.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            troll.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            troll.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            troll.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            troll.AddAnimation("SitEast", 0, 48 * 8, 48, 48, 1, 0.1f);
            troll.AddAnimation("SitWest", 0, 48 * 9, 48, 48, 1, 0.1f);

            
            
            troll.DrawOffset = new Vector2(-24, -38);
            troll.CurrentAnimation = "WalkWest";
            troll.IsAnimating = true;
        }
        public void initialize(SpriteAnimation vlad)
        {
            this.vlad = vlad;
        }
        //the upate methofor the troll
        public void rollTroll(GameTime gameTime)
        {
            Vector2 moveVectorTroll = Vector2.Zero;
            Vector2 moveDirTroll = Vector2.Zero;
            string animationTroll = "";
            int totalChairs = 0;

            for (int x = 0; x < myMap.MapHeight - 1; x++)
            {
                for (int y = 0; y < myMap.MapWidth - 1; y++)
                {
                    if (myMap.Rows[x].Columns[y].Chair == true && myMap.Rows[x].Columns[y].Pushable == false)
                    {
                        Xcoord.Add(x + 1);
                        Ycoord.Add(y + 2);
                        totalChairs++;
                    }
                }
            }
            if (totalChairs != 0)
            {
                if (hasChair == false)
                {
                    destinationChair[0] = randomChair.Next(0, totalChairs);
                    //destinationChair[0] = 1;
                    hasChair = true;
                }
                if (myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].BaseTiles.Contains(96))
                {
                    destinationChair[1] = (Xcoord.ElementAt(destinationChair[0]) * 48) + 40;
                    destinationChair[2] = (Ycoord.ElementAt(destinationChair[0]) * 48) - 48;
                }
                else
                {
                    destinationChair[1] = (Xcoord.ElementAt(destinationChair[0]) * 48) + 40;
                    destinationChair[2] = (Ycoord.ElementAt(destinationChair[0]) * 48);
                }
                if (troll.Position.X < destinationChair[2])
                {
                    goleft = false;
                    goright = true;
                }
                if (troll.Position.X > destinationChair[2])
                {
                    goright = false;
                    goleft = true;
                }

                if (troll.Position.Y > destinationChair[1])
                {
                    gosouth = false;
                    gonorth = true;
                }

                if (troll.Position.Y < destinationChair[1])
                {
                    gonorth = false;
                    gosouth = true;
                }
                if (troll.Position.X >= destinationChair[2] - 1 && troll.Position.X <= destinationChair[2] + 1)
                {
                    goright = false;
                    goleft = false;
                }
                if (troll.Position.Y >= destinationChair[1] - 1 && troll.Position.Y <= destinationChair[1] + 1)
                {
                    gonorth = false;
                    gosouth = false;
                }
                //code to make troll go around tables
                if (myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(48, 0)).Table == true ||
                    myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(-48, 0)).Table == true)
                {
                    atTable = true;
                    tableDir = 1;
                }
                if (myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(0, 48)).Table == true ||
                    myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(0, -48)).Table == true)
                {
                    if (moveDirTroll.Y != 0)
                    {
                        atTable = true;
                        tableDir = 2;
                    }
                }
                if (atTable == true)
                {
                    gosouth = false;
                    gonorth = false;
                    goleft = false;
                    goright = false;

                    if (myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(48, 0)).Table == true ||
                        myMap.GetCellAtWorldPoint(troll.Position + moveDirTroll + new Vector2(-48, 0)).Table == true)
                    {
                        int tableCheckInt = tableCheck(tableDir);
                        if (tableCheckInt == 1)
                        {
                            gosouth = true;
                        }
                        if (tableCheckInt == 2)
                        {
                            gonorth = true;
                        }
                        if (tableCheckInt == 3)
                        {
                            goleft = true;
                        }
                        if (tableCheckInt == 4)
                        {
                            goright = true;
                        }
                    }
                    else if (troll.Position.X < destinationChair[2])
                    {
                        goright = true;
                    }
                    else if (troll.Position.X > destinationChair[2])
                    {
                        goleft = true;
                    }
                    else if (troll.Position.Y < destinationChair[2])
                    {
                        gosouth = true;
                    }
                    else if (troll.Position.Y > destinationChair[2])
                    {
                        gonorth = true;
                    }
                    else
                    {
                        atTable = false;
                    }
                }

                //if statement to make him go behind chairs                
                if (myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].BaseTiles.Contains(96))
                {
                    if (troll.Position.X <= destinationChair[2] && troll.Position.X >= destinationChair[2] - 24 && troll.Position.Y != destinationChair[1])
                    {
                        goright = false;
                        goleft = true;
                        if (troll.Position.X == destinationChair[2] - 24)
                        {
                            iscorner = true;
                        }
                    }
                    if (iscorner == true)
                    {
                        gosouth = false;
                        gonorth = false;
                        goleft = false;
                        goright = false;
                        if (troll.Position.Y >= destinationChair[1] - 2 && troll.Position.Y <= destinationChair[1] + 2 && troll.Position.X < destinationChair[2])
                        {
                            goright = true;
                            if (troll.Position.Y >= destinationChair[1] - 2 && troll.Position.Y <= destinationChair[1] + 2 && troll.Position.X >= destinationChair[2] - 2 && troll.Position.X <= destinationChair[2] + 2)
                            {
                                iscorner = false;
                            }
                        }
                        else if (troll.Position.Y > destinationChair[1])
                        {
                            gonorth = true;
                        }
                        else if (troll.Position.Y < destinationChair[1])
                        {
                            gosouth = true;
                        }
                    }
                }
                if (myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].BaseTiles.Contains(97))
                {
                    if (troll.Position.X >= destinationChair[2] - 2 && troll.Position.X <= destinationChair[2] + 24 && troll.Position.Y != destinationChair[1])
                    {
                        goleft = false;
                        goright = true;
                        if (troll.Position.X == destinationChair[2] + 24)
                        {
                            iscorner = true;
                        }
                    }
                    if (iscorner == true)
                    {
                        gosouth = false;
                        gonorth = false;
                        goleft = false;
                        goright = false;
                        if (troll.Position.Y >= destinationChair[1] - 2 && troll.Position.Y <= destinationChair[1] + 2)
                        {
                            goleft = true;
                            if (troll.Position.Y >= destinationChair[1] && troll.Position.Y <= destinationChair[1] - 2 && troll.Position.X >= destinationChair[2] - 2 && troll.Position.X <= destinationChair[2] + 2)
                            {
                                iscorner = false;
                            }
                        }
                        else if (troll.Position.Y > destinationChair[1])
                        {
                            gonorth = true;
                        }
                        else if (troll.Position.Y < destinationChair[1])
                        {
                            gosouth = true;
                        }
                    }
                }
                if (sitChair == true)
                {
                    //myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].Pushable = false;
                    gosouth = false;
                    gonorth = false;
                    goleft = false;
                    goright = false;
                    if (isPulledChair == 1)
                    {
                        if (troll.Position.X < destinationChair[2] && troll.Position.X > destinationChair[2] - 74)
                        {
                            goleft = true;
                        }
                        if (troll.Position.X <= destinationChair[2] - 72 && troll.Position.X >= destinationChair[2] - 76)
                        {
                            myMap.GetCellAtWorldPoint(troll.Position).Walkable = false;
                            myMap.GetCellAtWorldPoint(troll.Position).sitChair = true;
                            animationTroll = "SitEast";
                            moveDirTroll = Vector2.Zero;
                            sitTimer -= gameTime.ElapsedGameTime.Milliseconds;
                            if (sitTimer <= 0)
                            {
                                sitChair = false;
                                hasChair = false;
                                iscorner = false;
                                Xcoord.Clear();
                                Ycoord.Clear();
                                sitTimer = 3000;
                                //myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].Pushable = true;
                                myMap.updateMap();
                            }
                        }
                    }
                    if (isPulledChair == 2)
                    {
                        if (troll.Position.X > destinationChair[2] - 24 && troll.Position.X < destinationChair[2] + 24)
                        {
                            goright = true;
                        }
                        if (troll.Position.X <= destinationChair[2] + 26 && troll.Position.X >= destinationChair[2] + 22)
                        {
                            myMap.GetCellAtWorldPoint(troll.Position).Walkable = false;
                            myMap.GetCellAtWorldPoint(troll.Position).sitChair = true;
                            animationTroll = "SitWest";
                            moveDirTroll = Vector2.Zero;
                            sitTimer -= gameTime.ElapsedGameTime.Milliseconds;
                            if (sitTimer <= 0)
                            {
                                sitChair = false;
                                hasChair = false;
                                iscorner = false;
                                Xcoord.Clear();
                                Ycoord.Clear();
                                sitTimer = 3000;
                                //myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].Pushable = true;
                                myMap.updateMap();
                            }
                        }
                    }
                }
                if (gonorth == true)
                {
                    moveDirTroll = new Vector2(0, -ySpeed);
                    animationTroll = "WalkNorth";
                    moveVectorTroll += new Vector2(0, -1);
                }
                if (gosouth == true)
                {
                    moveDirTroll = new Vector2(0, ySpeed);
                    animationTroll = "WalkSouth";
                    moveVectorTroll += new Vector2(0, 1);
                }
                if (goright == true)
                {
                    moveDirTroll = new Vector2(xSpeed, 0);
                    animationTroll = "WalkEast";
                    moveVectorTroll += new Vector2(2, 0);

                }
                if (goleft == true)
                {
                    moveDirTroll = new Vector2(-xSpeed, 0);
                    animationTroll = "WalkWest";
                    moveVectorTroll += new Vector2(-2, 0);
                }
                if (troll.Position.X >= destinationChair[2] - 4 && troll.Position.X <= destinationChair[2] + 4 && troll.Position.Y <= destinationChair[1] + 4 && troll.Position.Y >= destinationChair[1] - 4)
                {
                    if (troll.Position.X < (troll.Position + moveDirTroll).X && myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].BaseTiles.Contains(96) && sitChair == false ||
                        troll.Position.X > (troll.Position + moveDirTroll).X && myMap.Rows[(Xcoord.ElementAt(destinationChair[0]) - 1)].Columns[(Ycoord.ElementAt(destinationChair[0]) - 2)].BaseTiles.Contains(97) && sitChair == false)
                    {
                        isPulledChair = myMap.pullChair(troll.Position + moveDirTroll);
                        if (isPulledChair == 1 || isPulledChair == 2)
                        {
                            sitChair = true;
                        }
                    }
                }
                if (vlad.Position.X - 12 <= troll.Position.X + moveDirTroll.X &&
                vlad.Position.X + 12 >= troll.Position.X + moveDirTroll.X &&
                vlad.Position.Y - 12 <= troll.Position.Y + moveDirTroll.Y    &&
                vlad.Position.Y + 12 >= troll.Position.Y + moveDirTroll.Y)
                {
                    if (name == "Troll")
                    {
                        int rdmDir = random.Next(0,4);
                        if(rdmDir == 0)
                        {
                            vlad.Position += new Vector2(2, 0);
                        }
                        if(rdmDir == 1)
                        {
                             vlad.Position += new Vector2(-2, 0);
                        }
                        if(rdmDir == 2)
                        {
                             vlad.Position += new Vector2(0, 2);
                        }
                        else
                        {
                             vlad.Position += new Vector2(0, -2);
                        }
                    }
                }
                if (myMap.GetCellAtWorldPoint(troll.Position + (moveDirTroll)).Table == true)
                {
                    moveDirTroll = Vector2.Zero;
                }
                if (troll.CurrentAnimation != animationTroll)
                    troll.CurrentAnimation = animationTroll;
                if (moveDirTroll.Length() != 0)
                {
                    troll.MoveBy((int)moveDirTroll.X, (int)moveDirTroll.Y);
                    //if (troll.CurrentAnimation != animationTroll)
                    //    troll.CurrentAnimation = animationTroll;
                }
                else
                {
                    troll.CurrentAnimation = "Idle" + troll.CurrentAnimation.Substring(4);
                }
            }
        }
        //method to determine if troll should go up around or down around a table
        public int tableCheck(int dir)
        {
            int tableDistanceN = 0;
            int tableDistanceS = 0;
            int tableDistanceE = 0;
            int tableDistanceW = 0;
            for (int i = 0; i < 9; i++)
            {
                try
                {
                    if (dir == 1)
                    {
                        if (myMap.GetCellAtWorldPoint(troll.Position + new Vector2(48, -48 * i)).Table == true)
                        {
                            tableDistanceN++;
                        }
                        if (myMap.GetCellAtWorldPoint(troll.Position + new Vector2(48, 48 * i)).Table == true)
                        {
                            tableDistanceS++;
                        }
                    }
                    if (dir == 2)
                    {
                        if (myMap.GetCellAtWorldPoint(troll.Position + new Vector2(-48, 48 * i)).Table == true)
                        {
                            tableDistanceW++;
                        }
                        if (myMap.GetCellAtWorldPoint(troll.Position + new Vector2(48, 48 * i)).Table == true)
                        {
                            tableDistanceE++;
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
            if (dir == 1)
            {
                if (tableDistanceN >= tableDistanceS)
                {
                    return 1;
                }
                if (tableDistanceN < tableDistanceS)
                {
                    return 2;
                }
            }
            if (dir == 2)
            {
                if (tableDistanceW >= tableDistanceE)
                {
                    return 3;
                }
                if (tableDistanceE < tableDistanceW)
                {
                    return 4;
                }
            }
            return 0;            
        }
    }
}
