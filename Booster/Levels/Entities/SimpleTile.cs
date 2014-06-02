using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public class SimpleTile : StaticEntity, ICollisionableObject
    {
        public CollisionTypes CollisionType { get; set; }

        public Box BoundingBox { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return BoundingBox.BoxInPosition(Position);
            }
        }

        public SimpleTile(Vector2 position, Texture2D texture, Rectangle sourceRect, Box box, Box boundingBox, CollisionTypes collisionType)
            : base(position, texture, sourceRect, box)
        {
            CollisionType = collisionType;
            BoundingBox = boundingBox;
            LayerDepth = 0.5f;
        }

        public void OnCollision(ICollisionableObject collisionableObject)
        {

        }
    }
}
