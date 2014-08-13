using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class PlayerCreator : EntityCreator
    {
        private PlayerDirector director;

        public PlayerCreator(PlayerDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IPlayerBuilder builder = new PlayerBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}