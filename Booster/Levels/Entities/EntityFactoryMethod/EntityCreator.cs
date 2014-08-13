using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public abstract class EntityCreator
    {
        public abstract Entity FactoryMethod(Resources resources, Vector2 position);
    }
}