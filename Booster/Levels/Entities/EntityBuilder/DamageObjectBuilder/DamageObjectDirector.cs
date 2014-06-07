using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class DamageObjectDirector
    {
        public void Construct(IDamageObjectBuilder damageObjectBuilder)
        {
            damageObjectBuilder.BuildTexture();
            damageObjectBuilder.BuildSourceRect();
            damageObjectBuilder.BuildDestinationRect();
            damageObjectBuilder.BuildLayerDepth();
            damageObjectBuilder.BuildBoundingBox();
            damageObjectBuilder.BuildCollisionType();
            damageObjectBuilder.BuildDamage();
        }
    }
}
