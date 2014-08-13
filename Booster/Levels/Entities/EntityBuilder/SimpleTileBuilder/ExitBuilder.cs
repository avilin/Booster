using Booster.Levels.Entities;
using Booster.Levels.Entities.EntityBuilder;
using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels
{
    public class ExitBuilder : ISimpleTileBuilder
    {
        private ExitEntity exit;
        private Resources resources;

        public ExitBuilder(Resources resources, Vector2 position)
        {
            exit = new ExitEntity(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            exit.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            exit.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["signExit.png"];
        }

        public void BuildDestinationRect()
        {
            exit.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            exit.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            exit.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            exit.CollisionType = CollisionTypes.None;
        }

        public SimpleTile GetResult()
        {
            return exit;
        }
    }
}