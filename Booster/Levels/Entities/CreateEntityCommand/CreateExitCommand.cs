using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.CreateEntityCommand
{
    public class CreateExitCommand : ICreateEntityCommand
    {
        private SimpleTileCreator director;

        public CreateExitCommand(SimpleTileCreator director)
        {
            this.director = director;
        }

        public void Execute(ref Entity entity, Vector2 position, Resources resources)
        {
            ISimpleTileBuilder builder = new ExitBuilder(resources, position);
            director.Construct(builder);
            entity = builder.GetResult();
        }
    }
}
