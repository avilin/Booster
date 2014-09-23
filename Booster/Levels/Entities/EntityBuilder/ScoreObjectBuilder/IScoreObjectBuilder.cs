using Booster.Levels.Entities;

namespace Booster.Levels.Entities.EntityBuilder
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