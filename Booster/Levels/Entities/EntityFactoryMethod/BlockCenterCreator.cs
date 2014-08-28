using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class BlockCenterCreator : EntityCreator
    {
        private SimpleTileDirector director;

        public BlockCenterCreator(SimpleTileDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            ISimpleTileBuilder builder = new BlockCenterBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}