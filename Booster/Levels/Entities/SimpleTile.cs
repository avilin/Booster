using Booster.Util;
using Microsoft.Xna.Framework;

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