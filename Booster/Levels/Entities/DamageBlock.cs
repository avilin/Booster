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
    public class DamageBlock : SimpleTile
    {
        public int Damage { get; set; }

        public DamageBlock(Vector2 position)
            : base(position)
        {
            Damage = 0;
        }

        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IDamageable)
            {
                ((IDamageable)collisionableObject).LoseLife(Damage);
            }
        }
    }
}
