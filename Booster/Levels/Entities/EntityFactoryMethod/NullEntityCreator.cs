using Booster.Util;
using Microsoft.Xna.Framework;

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
