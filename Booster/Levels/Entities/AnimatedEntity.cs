using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Booster.Util.Animations;

namespace Booster.Levels.Entities
{
    public abstract class AnimatedEntity : Entity, IDrawableObject
    {
        protected Dictionary<String, Animation> animations;
        protected string currentAnimation;

        public Rectangle DestinationRect
        {
            get { return animations[currentAnimation].DestinationRect; }
        }

        protected AnimatedEntity(Vector2 position, Dictionary<String, Animation> animations) : base(position)
        {
            this.animations = animations;
            this.currentAnimation = "default";
            Position = position;
        }

        public abstract void UpdateAnimation(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                animations[currentAnimation].Draw(spriteBatch, SpriteEffects.None);
            }
        }
    }
}
