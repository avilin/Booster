using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public abstract class EntityCreator
    {
        public abstract Entity FactoryMethod(Resources resources, Vector2 position);
    }
}
