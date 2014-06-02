using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public class ExitEntity : StaticEntity, ICollisionableObject
    {
        public ExitEntity(Vector2 position, Texture2D texture, Rectangle sourceRect, Box box, Box boundingBox)
            : base(position, texture, sourceRect, box)
        {
            BoundingBox = boundingBox;
        }

        public CollisionTypes CollisionType
        {
            get
            {
                return CollisionTypes.None;
            }
        }

        public Box BoundingBox { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return BoundingBox.BoxInPosition(Position);
            }
        }

        public void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IStateable)
            {
                ((IStateable)collisionableObject).CurrentEntityStates.Add(EntityStates.Win);
            }
        }
    }
}
