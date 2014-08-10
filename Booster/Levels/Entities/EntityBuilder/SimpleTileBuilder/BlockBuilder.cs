using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class BlockBuilder : ISimpleTileBuilder
    {
        private SimpleTile block;
        private Resources resources;

        public BlockBuilder(Resources resources, Vector2 position)
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
            block.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["snow.png"];
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
