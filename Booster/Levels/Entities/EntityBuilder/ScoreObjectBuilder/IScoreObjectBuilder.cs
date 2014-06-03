using Booster.Levels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels
{
    public interface IScoreObjectBuilder
    {
        void BuildTexture();
        void BuildSourceRect();
        void BuildDestinationRect();
        void BuildLayerDepth();
        void BuildBoundingBox();
        void BuildCollisionType();
        void BuildScore();
        ScoreObject GetResult();
    }
}
