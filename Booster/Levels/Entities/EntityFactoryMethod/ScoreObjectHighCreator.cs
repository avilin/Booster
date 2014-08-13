using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class ScoreObjectHighCreator : EntityCreator
    {
        private ScoreObjectDirector director;

        public ScoreObjectHighCreator(ScoreObjectDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IScoreObjectBuilder builder = new ScoreObjectHighBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}