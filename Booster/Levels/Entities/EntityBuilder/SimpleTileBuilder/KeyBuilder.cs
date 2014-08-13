using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class KeyBuilder : ISimpleTileBuilder
    {
        private SimpleTile key;
        private Resources resources;

        public KeyBuilder(Resources resources, Vector2 position)
        {
            key = new Key(position);
            this.resources = resources;
        }

        public void BuildResources()
        {
            key.Texture = resources.SpriteSheets["hud"].SpriteSheet;
            ((Key)key).CollisionSound = resources.SoundEffects["key"];
        }

        public void BuildSourceRect()
        {
            key.SourceRect = resources.SpriteSheets["hud"].ObjectLocation["hud_keyYellow.png"];
        }

        public void BuildDestinationRect()
        {
            key.destinationRect = new Box(16, 16, 16, 16);
        }

        public void BuildLayerDepth()
        {
            key.LayerDepth = 0.5f;
        }

        public void BuildBoundingBox()
        {
            key.BoundingBox = new Box(16, 16, 16, 16);
        }

        public void BuildCollisionType()
        {
            key.CollisionType = CollisionTypes.None;
        }

        public SimpleTile GetResult()
        {
            return key;
        }
    }
}