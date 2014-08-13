using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class DamageBlockMidCreator :EntityCreator
    {
        private DamageObjectDirector director;

        public DamageBlockMidCreator(DamageObjectDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IDamageObjectBuilder builder = new DamageObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}