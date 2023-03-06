using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ButlerIsland
{
    //class that allows the sprite to move
    class MobileSprite
    {
        SpriteAnimation asSprite;
        Queue<Vector2> queuePath = new Queue<Vector2>();
        Vector2 v2Target;
        float fSpeed = 1f;
        int iCollisionBufferX = 0;
        int iCollisionBufferY = 0;
        bool bActive = true;
        bool bMovingTowardsTarget = true;
        bool bPathing = true;
        bool bLoopPath = true;
        bool bCollidable = true;
        bool bVisible = true;
        bool bDeactivateAtEndOfPath = false;
        bool bHideAtEndOfPath = false;
        string sEndPathAnimation = null;

        public SpriteAnimation Sprite
        {
            get { return asSprite; }
        }

        public Vector2 Position
        {
            get { return asSprite.Position; }
            set { asSprite.Position = value; }
        }

        public Vector2 Target
        {
            get { return v2Target; }
            set { v2Target = value; }
        }

        public int HorizontalCollisionBuffer
        {
            get { return iCollisionBufferX; }
            set { iCollisionBufferX = value; }
        }

        public int VerticalCollisionBuffer
        {
            get { return iCollisionBufferY; }
            set { iCollisionBufferY = value; }
        }

        public bool IsPathing
        {
            get { return bPathing; }
            set { bPathing = value; }
        }

        public bool DeactivateAfterPathing
        {
            get { return bDeactivateAtEndOfPath; }
            set { bDeactivateAtEndOfPath = value; }
        }

        public bool LoopPath
        {
            get { return bLoopPath; }
            set { bLoopPath = value; }
        }

        public string EndPathAnimation
        {
            get { return sEndPathAnimation; }
            set { sEndPathAnimation = value; }
        }

        public bool HideAtEndOfPath
        {
            get { return bHideAtEndOfPath; }
            set { bHideAtEndOfPath = value; }
        }

        public bool IsVisible
        {
            get { return bVisible; }
            set { bVisible = value; }
        }

        public float Speed
        {
            get { return fSpeed; }
            set { fSpeed = value; }
        }

        public bool IsActive
        {
            get { return bActive; }
            set { bActive = value; }
        }

        public bool IsMoving
        {
            get { return bMovingTowardsTarget; }
            set { bMovingTowardsTarget = value; }
        }

        public bool IsCollidable
        {
            get { return bCollidable; }
            set { bCollidable = value; }
        }

        public Rectangle BoundingBox
        {
            get { return asSprite.BoundingBox; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                    asSprite.BoundingBox.X + iCollisionBufferX,
                    asSprite.BoundingBox.Y + iCollisionBufferY,
                    asSprite.Width - (2 * iCollisionBufferX),
                    asSprite.Height - (2 * iCollisionBufferY));
            }
        }

        public MobileSprite(Texture2D texture)
        {
            asSprite = new SpriteAnimation(texture);
        }

        public void AddPathNode(Vector2 node)
        {
            queuePath.Enqueue(node);
        }

        public void AddPathNode(int X, int Y)
        {
            queuePath.Enqueue(new Vector2(X, Y));
        }

        public void ClearPathNodes()
        {
            queuePath.Clear();
        }

        public void Update(GameTime gameTime)
        {
            if (bActive && bMovingTowardsTarget)
            {
                if (!(v2Target == null))
                {
                    // Get a vector pointing from the current location of the sprite
                    // to the destination.
                    Vector2 Delta = new Vector2(v2Target.X - asSprite.X, v2Target.Y - asSprite.Y);

                    if (Delta.Length() > Speed)
                    {
                        Delta.Normalize();
                        Delta *= Speed;
                        Position += Delta;
                    }
                    else
                    {
                        if (v2Target == asSprite.Position)
                        {
                            if (bPathing)
                            {
                                if (queuePath.Count > 0)
                                {
                                    v2Target = queuePath.Dequeue();
                                    if (bLoopPath)
                                    {
                                        queuePath.Enqueue(v2Target);
                                    }
                                }
                                else
                                {
                                    if (!(sEndPathAnimation == null))
                                    {
                                        if (!(Sprite.CurrentAnimation == sEndPathAnimation))
                                        {
                                            Sprite.CurrentAnimation = sEndPathAnimation;
                                        }
                                    }

                                    if (bDeactivateAtEndOfPath)
                                    {
                                        IsActive = false;
                                    }

                                    if (bHideAtEndOfPath)
                                    {
                                        IsVisible = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            asSprite.Position = v2Target;
                        }
                    }
                }
            }
            if (bActive)
                asSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bVisible)
            {
                asSprite.Draw(spriteBatch, 0, 0);
            }
        }
    }
}
