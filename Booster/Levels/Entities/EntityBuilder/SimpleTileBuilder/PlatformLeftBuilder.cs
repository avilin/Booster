using Booster.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlatformLeftBuilder : ISimpleTileBuilder
    {
        private SimpleTile platform;
        private Resources resources;

        public PlatformLeftBuilder(Resources resources, Vector2 position)
        {
            platform = new SimpleTile(position);
            this.resources = resources;
        }

        public void BuildTexture()
        {
            platform.Texture = resources.SpriteSheets["tiles"].SpriteSheet;
        }

        public void BuildSourceRect()
        {
            platform.SourceRect = resources.SpriteSheets["tiles"].ObjectLocation["snowHalfLeft.png"];
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
