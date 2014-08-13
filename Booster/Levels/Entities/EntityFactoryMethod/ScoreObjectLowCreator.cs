using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class ScoreObjectLowCreator : EntityCreator
    {
        private ScoreObjectDirector director;

        public ScoreObjectLowCreator(ScoreObjectDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IScoreObjectBuilder builder = new ScoreObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}