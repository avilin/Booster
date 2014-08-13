namespace Booster.Levels.Entities.EntityBuilder
{
    public interface ISimpleTileBuilder
    {
        void BuildResources();
        void BuildSourceRect();
        void BuildDestinationRect();
        void BuildLayerDepth();
        void BuildBoundingBox();
        void BuildCollisionType();
        SimpleTile GetResult();
    }
}