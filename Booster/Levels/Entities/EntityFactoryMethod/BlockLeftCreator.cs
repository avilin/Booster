using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityBuilder.SimpleTileBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockLeftCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public BlockLeftCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockLeftBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}