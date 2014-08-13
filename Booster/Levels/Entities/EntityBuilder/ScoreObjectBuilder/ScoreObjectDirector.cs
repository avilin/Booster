namespace Booster.Levels
{
    public class ScoreObjectDirector
    {
        public void Construct(IScoreObjectBuilder scoreObjectBuilder)
        {
            scoreObjectBuilder.BuildResources();
            scoreObjectBuilder.BuildSourceRect();
            scoreObjectBuilder.BuildDestinationRect();
            scoreObjectBuilder.BuildLayerDepth();
            scoreObjectBuilder.BuildBoundingBox();
            scoreObjectBuilder.BuildCollisionType();
            scoreObjectBuilder.BuildScore();
        }
    }
}