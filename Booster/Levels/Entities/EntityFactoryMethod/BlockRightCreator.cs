using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockRightCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public BlockRightCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockRightBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}