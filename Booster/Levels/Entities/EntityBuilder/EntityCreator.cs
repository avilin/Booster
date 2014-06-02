using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class SimpleTileCreator
    {
        public void Construct(ISimpleTileBuilder simpleTileBuilder)
        {
            simpleTileBuilder.BuildTexture();
            simpleTileBuilder.BuildSourceRect();
            simpleTileBuilder.BuildDestinationRect();
            simpleTileBuilder.BuildLayerDepth();
            simpleTileBuilder.BuildCollisionType();
        }
    }
}
