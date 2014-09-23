using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Booster.Levels.Entities
{
    public abstract class AnimatedEntity : Entity, IDrawableObject
    {
        public Dictionary<String, Animation> Animations { get; set; }
        protected string currentAnimation;

        public Rectangle DestinationRect
        {
            get { return Animations[currentAnimation].DestinationRect; }
        }

        protected AnimatedEntity(Vector2 position) : base(position)
        {
            this.currentAnimation = "default";
            Position = position;
        }

        public abstract void UpdateAnimation(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animations[currentAnimation].Draw(spriteBatch, SpriteEffects.None);
            }
        }
    }
}