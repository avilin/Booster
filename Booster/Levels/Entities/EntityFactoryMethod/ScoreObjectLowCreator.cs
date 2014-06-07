using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class ScoreObjectLowCreator : EntityCreator
    {
        private ScoreObjectDirector director = new ScoreObjectDirector();

        public ScoreObjectLowCreator()
        {
            //this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IScoreObjectBuilder builder = new ScoreObjectLowBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
