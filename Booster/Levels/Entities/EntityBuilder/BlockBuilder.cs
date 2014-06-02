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

        public BlockBuilder(Vector2 position)
        {
            //block = new SimpleTile(position);
        }

        public void BuildTexture()
        {
            block.Texture = Resources.GetInstance().SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            block.SourceRect = Resources.GetInstance().SpriteSheets["tiles"].ObjectLocation["block"];
        }

        public void BuildDestinationRect()
        {
            block.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            block.LayerDepth = 0.5f;
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
