using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ButlerIsland
{
    class PlayerBrain
    {
        TileMap myMap;
        KeyboardState oldState; //used for key release detection
        SpriteAnimation vlad;
        SpriteAnimation troll;
        public PlayerBrain(TileMap myMap, SpriteAnimation vlad, SpriteAnimation troll, KeyboardState oldState)
        {
            this.myMap = myMap;
            this.vlad = vlad;
            this.oldState = oldState;
            this.troll = troll;            
        }
        public void initialize()
        {
            vlad.AddAnimation("WalkEast", 0, 48 * 0, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorth", 0, 48 * 1, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthEast", 0, 48 * 2, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkNorthWest", 0, 48 * 3, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouth", 0, 48 * 4, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthEast", 0, 48 * 5, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkSouthWest", 0, 48 * 6, 48, 48, 8, 0.1f);
            vlad.AddAnimation("WalkWest", 0, 48 * 7, 48, 48, 8, 0.1f);

            vlad.AddAnimation("IdleEast", 0, 48 * 0, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorth", 0, 48 * 1, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthEast", 0, 48 * 2, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleNorthWest", 0, 48 * 3, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouth", 0, 48 * 4, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthEast", 0, 48 * 5, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleSouthWest", 0, 48 * 6, 48, 48, 1, 0.2f);
            vlad.AddAnimation("IdleWest", 0, 48 * 7, 48, 48, 1, 0.2f);

            vlad.Position = new Vector2(130, 130);
            vlad.DrawOffset = new Vector2(-24, -38);
            vlad.CurrentAnimation = "WalkEast";
            vlad.IsAnimating = true;
        }
        public int rollPlayer(GameTime gameTime, int score)
        {
            Vector2 moveVector = Vector2.Zero;
            Vector2 moveDir = Vector2.Zero;
            string animation = "";

            //vlad moves with character input
            KeyboardState ks = Keyboard.GetState();
            //movement
            if (ks.IsKeyDown(Keys.NumPad7))
            {
                moveDir = new Vector2(-2, -1);
                animation = "WalkNorthWest";
                moveVector += new Vector2(-2, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad8) || ks.IsKeyDown(Keys.Up))
            {
                moveDir = new Vector2(0, -2);
                animation = "WalkNorth";
                moveVector += new Vector2(0, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad9))
            {
                moveDir = new Vector2(2, -1);
                animation = "WalkNorthEast";
                moveVector += new Vector2(2, -1);
            }

            if (ks.IsKeyDown(Keys.NumPad4) || ks.IsKeyDown(Keys.Left))
            {
                moveDir = new Vector2(-2, 0);
                animation = "WalkWest";
                moveVector += new Vector2(-2, 0);
            }

            if (ks.IsKeyDown(Keys.NumPad6) || ks.IsKeyDown(Keys.Right))
            {
                moveDir = new Vector2(2, 0);
                animation = "WalkEast";
                moveVector += new Vector2(2, 0);
            }

            if (ks.IsKeyDown(Keys.NumPad1))
            {
                moveDir = new Vector2(-2, 1);
                animation = "WalkSouthWest";
                moveVector += new Vector2(-2, 1);
            }

            if (ks.IsKeyDown(Keys.NumPad2) || ks.IsKeyDown(Keys.Down))
            {
                moveDir = new Vector2(0, 2);
                animation = "WalkSouth";
                moveVector += new Vector2(0, 1);
            }

            if (ks.IsKeyDown(Keys.NumPad3))
            {
                moveDir = new Vector2(2, 1);
                animation = "WalkSouthEast";
                moveVector += new Vector2(2, 1);
            }
            //pushes the chairs in
            if (ks.IsKeyDown(Keys.Space))
            {

            }
            else if (oldState.IsKeyDown(Keys.Space))
            {

                if (vlad.Position.X < (vlad.Position + moveDir).X || vlad.Position.X > (vlad.Position + moveDir).X)
                {
                    if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).sitChair == false && myMap.GetCellAtWorldPoint(vlad.Position + moveDir).Pushable == true)
                    {
                        myMap.pushChair(vlad.Position + moveDir);
                        myMap.updateMap();
                        score += 1;
                    }
                }
                if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).HeightTiles.Contains(101))
                {
                    Conversation.StartConversation("Door");
                    Conversation.Update(gameTime);
                }
            }
            oldState = ks;
            //collision detection with objects            
            if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).Walkable == false)
            {
                moveDir = Vector2.Zero;
            }
            if (myMap.GetCellAtWorldPoint(vlad.Position + moveDir).Chair == true)
            {
                moveDir = Vector2.Zero;
            }
            if (myMap.GetCellAtWorldPoint(vlad.Position).Walkable == false)
            {
                if (myMap.GetCellAtWorldPoint(vlad.Position).BaseTiles.Contains(96))
                {
                    moveDir = new Vector2(-2, 0);
                    animation = "WalkWest";
                }
                if (myMap.GetCellAtWorldPoint(vlad.Position).BaseTiles.Contains(97))
                {
                    moveDir = new Vector2(2, 0);
                    animation = "WalkEast";
                }
            }
            //collision detection with troll
            if (vlad.Position.X + moveDir.X <= troll.Position.X + 12 &&
                vlad.Position.X + moveDir.X >= troll.Position.X - 12 &&
                vlad.Position.Y + moveDir.Y <= troll.Position.Y + 12 &&
                vlad.Position.Y + moveDir.Y >= troll.Position.Y - 12)
            {
                moveDir = Vector2.Zero;
            }
            if (moveDir.Length() != 0)
            {
                vlad.MoveBy((int)moveDir.X, (int)moveDir.Y);
                if (vlad.CurrentAnimation != animation)
                    vlad.CurrentAnimation = animation;
            }
            else
            {
                vlad.CurrentAnimation = "Idle" + vlad.CurrentAnimation.Substring(4);
            }
            

            //camera that centers on vlad
            float vladX = MathHelper.Clamp(vlad.Position.X, 0 + vlad.DrawOffset.X, Camera.WorldWidth);
            float vladY = MathHelper.Clamp(vlad.Position.Y, 0 + vlad.DrawOffset.Y, Camera.WorldHeight);
            vlad.Position = new Vector2(vladX, vladY);
            Vector2 testPosition = Camera.WorldToScreen(vlad.Position);

            if (testPosition.X < 100)
            {
                Camera.Move(new Vector2(testPosition.X - 100, 0));
            }

            if (testPosition.X > (Camera.ViewWidth - 100))
            {
                Camera.Move(new Vector2(testPosition.X - (Camera.ViewWidth - 100), 0));
            }

            if (testPosition.Y < 100)
            {
                Camera.Move(new Vector2(0, testPosition.Y - 100));
            }

            if (testPosition.Y > (Camera.ViewHeight - 100))
            {
                Camera.Move(new Vector2(0, testPosition.Y - (Camera.ViewHeight - 100)));
            }
            return score;
        }
    }
}
