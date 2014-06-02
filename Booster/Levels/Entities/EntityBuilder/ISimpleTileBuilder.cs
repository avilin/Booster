using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public interface ISimpleTileBuilder
    {
        void BuildTexture();
        void BuildSourceRect();
        void BuildDestinationRect();
        void BuildLayerDepth();
        void BuildCollisionType();
        SimpleTile GetResult();
    }
}
