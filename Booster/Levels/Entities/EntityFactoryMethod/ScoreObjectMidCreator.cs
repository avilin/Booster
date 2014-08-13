using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class ScoreObjectMidCreator : EntityCreator
    {
        private ScoreObjectDirector director;

        public ScoreObjectMidCreator(ScoreObjectDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IScoreObjectBuilder builder = new ScoreObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}