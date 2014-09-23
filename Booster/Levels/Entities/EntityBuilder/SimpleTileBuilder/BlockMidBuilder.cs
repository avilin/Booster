using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class BlockMidBuilder : ISimpleTileBuilder
    {
        private SimpleTile block;
        private Resources resources;

        public BlockMidBuilder(Resources resources, Vector2 position)
        {
            block = new SimpleTile(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            block.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            block.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["snowMid.png"];
        }

        public void BuildDestinationRect()
        {
            block.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            block.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            block.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            block.CollisionType = CollisionTypes.Block;
        }

        public SimpleTile GetResult()
        {
            return block;
        }
    }
}