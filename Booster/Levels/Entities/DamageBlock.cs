using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Booster.Levels;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public class DamageBlock : StaticEntity, ICollisionableObject
    {
        public int Damage { get; set; }

        public CollisionTypes CollisionType
        {
            get
            {
                return CollisionTypes.Block;
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

        public DamageBlock(Vector2 position, Texture2D texture, Rectangle sourceRect, Box box, Box boundingBox, int damage)
            : base(position, texture, sourceRect, box)
        {
            BoundingBox = boundingBox;
            Damage = damage;
        }

        public void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IDamageable)
            {
                ((IDamageable)collisionableObject).LoseLife(Damage);
            }
        }
    }
}
