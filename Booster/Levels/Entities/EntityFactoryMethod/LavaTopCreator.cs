using Booster.Levels.Entities.EntityBuilder;
using Booster.Levels.Entities.EntityBuilder.DamageObjectBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class LavaTopCreator : EntityCreator
    {
        private DamageObjectDirector director;

        public LavaTopCreator(DamageObjectDirector director)
        {
            this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IDamageObjectBuilder builder = new LavaTopBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
