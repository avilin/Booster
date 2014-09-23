namespace Booster.Levels.Entities.EntityBuilder
{
    public class SimpleTileDirector
    {
        public void Construct(ISimpleTileBuilder simpleTileBuilder)
        {
            simpleTileBuilder.BuildResources();
            simpleTileBuilder.BuildSourceRect();
            simpleTileBuilder.BuildDestinationRect();
            simpleTileBuilder.BuildLayerDepth();
            simpleTileBuilder.BuildBoundingBox();
            simpleTileBuilder.BuildCollisionType();
        }
    }
}