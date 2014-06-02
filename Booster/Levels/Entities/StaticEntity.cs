using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Booster.Util;

namespace Booster.Levels.Entities
{
    public abstract class StaticEntity : Entity, IDrawableObject
    {
        public float LayerDepth { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public Box destinationRect { get; set; }

        public Rectangle DestinationRect
        {
            get
            {
                return destinationRect.BoxInPosition(Position);
            }
        }

        protected StaticEntity(Vector2 position, Texture2D texture, Rectangle sourceRect, Box destinationRect) : base(position)
        {
            Texture = texture;
            SourceRect = sourceRect;
            this.destinationRect = destinationRect;
            LayerDepth = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(Texture, DestinationRect, SourceRect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, LayerDepth);
            }
        }
    }
}
