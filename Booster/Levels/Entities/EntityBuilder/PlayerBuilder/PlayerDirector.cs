namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlayerDirector
    {
        public void Construct(IPlayerBuilder playerBuider)
        {
            playerBuider.BuildResources();
            playerBuider.BuildBoundingBox();
            playerBuider.BuildCollisionType();
            playerBuider.BuildHealth();
            playerBuider.BuildStatesTime();
        }
    }
}