using Microsoft.Xna.Framework;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public interface ICollisionableObject
    {
        bool Active { get; set; }
        CollisionTypes CollisionType { get; }
        Box BoundingBox { get; set; }
        Rectangle HitBox { get; }

        void OnCollision(ICollisionableObject collisionableObject);
    }
}