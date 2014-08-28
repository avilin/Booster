using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockMidCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public BlockMidCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}