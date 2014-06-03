using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels
{
    public class ScoreObjectCreator
    {
        public void Construct(IScoreObjectBuilder scoreObjectBuilder)
        {
            scoreObjectBuilder.BuildTexture();
            scoreObjectBuilder.BuildSourceRect();
            scoreObjectBuilder.BuildDestinationRect();
            scoreObjectBuilder.BuildLayerDepth();
            scoreObjectBuilder.BuildBoundingBox();
            scoreObjectBuilder.BuildCollisionType();
            scoreObjectBuilder.BuildScore();
        }
    }
}
