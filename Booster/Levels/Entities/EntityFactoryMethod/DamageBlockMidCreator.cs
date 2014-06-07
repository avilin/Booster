using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityFactoryMethod
{
    public class DamageBlockMidCreator :EntityCreator
    {
        private DamageObjectDirector director = new DamageObjectDirector();

        public DamageBlockMidCreator()
        {
            //this.director = director;
        }

        public override Entity FactoryMethod(Resources resources, Vector2 position)
        {
            IDamageObjectBuilder builder = new DamageObjectMidBuilder(resources, position);
            director.Construct(builder);
            return builder.GetResult();
        }
    }
}
