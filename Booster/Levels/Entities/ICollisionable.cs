using Microsoft.Xna.Framework;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public interface ICollisionable
    {
        bool Active { get; set; }
        CollisionTypes CollisionType { get; }
        Box BoundingBox { get; set; }
        Rectangle HitBox { get; }

        void OnCollision(ICollisionable collisionableObject);
    }
}