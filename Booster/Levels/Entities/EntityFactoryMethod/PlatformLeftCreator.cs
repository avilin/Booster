using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class PlatformLeftCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public PlatformLeftCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new PlatformLeftBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}