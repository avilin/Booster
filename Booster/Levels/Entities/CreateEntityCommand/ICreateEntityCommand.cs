using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.CreateEntityCommand
{
    public interface ICreateEntityCommand
    {
        void Execute(ref Entity entity, Vector2 position, Resources resources);
    }
}
