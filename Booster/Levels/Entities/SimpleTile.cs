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

        public SimpleTile(Vector2 position)
            : base(position)
        {

        }

        public virtual void OnCollision(ICollisionableObject collisionableObject)
        {

        }
    }
}
