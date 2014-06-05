using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.CreateEntityCommand
{
    public class CreateDamageBlockLowCommand : ICreateEntityCommand
    {
        private DamageObjectCreator director;

        public CreateDamageBlockLowCommand(DamageObjectCreator director)
        {
            this.director = director;
        }

        public void Execute(ref Entity entity, Vector2 position, Resources resources)
        {
            IDamageObjectBuilder builder = new DamageObjectLowBuilder(resources, position);
            director.Construct(builder);
            entity = builder.GetResult();
        }
    }
}
