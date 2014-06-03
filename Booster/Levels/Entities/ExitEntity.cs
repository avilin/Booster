using Booster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public class ExitEntity : SimpleTile
    {
        public ExitEntity(Vector2 position)
            : base(position)
        {

        }

        public override void OnCollision(ICollisionableObject collisionableObject)
        {
            if (collisionableObject is IStateable)
            {
                ((IStateable)collisionableObject).CurrentEntityStates.Add(EntityStates.Win);
            }
        }
    }
}
