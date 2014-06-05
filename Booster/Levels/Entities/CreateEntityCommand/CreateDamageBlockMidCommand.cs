using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.CreateEntityCommand
{
    public class CreateDamageBlockMidCommand :ICreateEntityCommand
    {
        private DamageObjectCreator director;

        public CreateDamageBlockMidCommand(DamageObjectCreator director)
        {
            this.director = director;
        }

        public void Execute(ref Entity entity, Vector2 position, Resources resources)
        {
            IDamageObjectBuilder builder = new DamageObjectMidBuilder(resources, position);
            director.Construct(builder);
            entity = builder.GetResult();
        }
    }
}
