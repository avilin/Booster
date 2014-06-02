using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public interface ICollisionableObject
    {
        Boolean Active { get; set; }
        CollisionTypes CollisionType { get; }
        Box BoundingBox { get; set; }
        Rectangle HitBox { get; }

        void OnCollision(ICollisionableObject collisionableObject);
    }
}
