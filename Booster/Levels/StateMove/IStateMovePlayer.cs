using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Booster.Levels.Entities;

namespace Booster.Levels.StateMove
{
    public interface IStateMove
    {
        void Move(ICollisionableObject entity, Vector2 nextPosition, Map map);
    }
}
