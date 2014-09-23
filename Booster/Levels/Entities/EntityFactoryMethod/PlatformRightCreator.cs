using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class PlatformRightCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public PlatformRightCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new PlatformRightBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}