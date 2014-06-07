using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class NullEntityCreator : EntityCreator
    {
        public NullEntityCreator()
        {

        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            return null;
        }
    }
}
