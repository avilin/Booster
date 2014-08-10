using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public interface IDamageObjectBuilder
    {
        void BuildResources();
        void BuildSourceRect();
        void BuildDestinationRect();
        void BuildLayerDepth();
        void BuildBoundingBox();
        void BuildCollisionType();
        void BuildDamage();
        DamageBlock GetResult();
    }
}
