using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class DoorBuilder : ISimpleTileBuilder
    {
        private SimpleTile door;
        private Resources resources;

        public DoorBuilder(Resources resources, Vector2 position)
        {
            door = new Door(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            door.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
            ((Door)door).CollisionSound = resources.SoundEffects["door"];
        }

        public void BuildSourceRect()
        {
            door.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["lock_yellow.png"];
        }

        public void BuildDestinationRect()
        {
            door.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            door.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            door.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            door.CollisionType = CollisionTypes.Block;
        }

        public SimpleTile GetResult()
        {
            return door;
        }
    }
}