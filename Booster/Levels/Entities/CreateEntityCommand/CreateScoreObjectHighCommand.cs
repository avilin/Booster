using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.CreateEntityCommand
{
    public class CreateScoreObjectHighCommand : ICreateEntityCommand
    {
        private ScoreObjectCreator director;

        public CreateScoreObjectHighCommand(ScoreObjectCreator director)
        {
            this.director = director;
        }

        public void Execute(ref Entity entity, Vector2 position, Resources resources)
        {
            IScoreObjectBuilder builder = new ScoreObjectHighBuilder(resources, position);
            director.Construct(builder);
            entity = builder.GetResult();
        }
    }
}
