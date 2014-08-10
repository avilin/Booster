using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public class Key : SimpleTile
    {
        public SoundEffect CollisionSound { get; set; }

        public Key(Vector2 position)
            : base(position)
        {

        }
        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (!Active)
            {
                return;
            }
            if (collisionableObject is IHaveKeys)
            {
                ((IHaveKeys)collisionableObject).Keys++;
            }
            Active = false;
        }
    }
}
