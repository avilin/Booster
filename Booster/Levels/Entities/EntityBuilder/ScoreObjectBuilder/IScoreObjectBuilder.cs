using Booster.Levels.Entities;

namespace Booster.Levels
{
    public interface IScoreObjectBuilder
    {
        void BuildResources();
        void BuildSourceRect();
        void BuildDestinationRect();
        void BuildLayerDepth();
        void BuildBoundingBox();
        void BuildCollisionType();
        void BuildScore();
        ScoreObject GetResult();
    }
}