namespace Booster.Levels.Entities.EntityBuilder
{
    public class DamageObjectDirector
    {
        public void Construct(IDamageObjectBuilder damageObjectBuilder)
        {
            damageObjectBuilder.BuildResources();
            damageObjectBuilder.BuildSourceRect();
            damageObjectBuilder.BuildDestinationRect();
            damageObjectBuilder.BuildLayerDepth();
            damageObjectBuilder.BuildBoundingBox();
            damageObjectBuilder.BuildCollisionType();
            damageObjectBuilder.BuildDamage();
        }
    }
}