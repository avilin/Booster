using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public BlockCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}