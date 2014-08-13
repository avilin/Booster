using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlatformRightBuilder : ISimpleTileBuilder
    {
        private SimpleTile platform;
        private Resources resources;

        public PlatformRightBuilder(Resources resources, Vector2 position)
        {
            platform = new SimpleTile(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            platform.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            platform.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["snowHalfRight.png"];
        }

        public void BuildDestinationRect()
        {
            platform.destinationRect = new Box(16, 16, 16, 0);
        }

        public void BuildLayerDepth()
        {
            platform.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            platform.BoundingBox = new Box(16, 16, 16, 0);
        }

        public void BuildCollisionType()
        {
            platform.CollisionType = CollisionTypes.Top;
        }

        public SimpleTile GetResult()
        {
            return platform;
        }
    }
}