namespace Booster.Levels.Entities.EntityBuilder
{
    public interface IPlayerBuilder
    {
        void BuildResources();
        void BuildBoundingBox();
        void BuildCollisionType();
        void BuildHealth();
        void BuildStatesTime();
        Player GetResult();
    }
}