using Booster.Util;
using Booster.Util.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities.EntityBuilder
{
    public class PlayerAnimationHurtBuilder : IAnimationBuilder
    {
        private Animation animation = new Animation();
        private Resources resources;

        public PlayerAnimationHurtBuilder(Resources resources, Vector2 position)
        {
            animation.Initialize(position);
            this.resources = resources;
        }

        public void BuildTexture()
        {
            animation.SpriteSheet = resources.SpriteSheets["p1"].SpriteSheet;
        }

        public void BuildFrames()
        {
            List<Frame> frames = new List<Frame>();
            frames.Add(new Frame(resources.SpriteSheets["p1"].ObjectLocation["p1_hurt.png"], 1000));
            animation.Frames = frames;
        }

        public void BuildDestinationRect()
        {
            animation.Box = new Box(20, 32, 20, 32);
        }

        public void BuildScale()
        {
            animation.Scale = 1f;
        }

        public void BuildLooping()
        {
            animation.Looping = true;
        }

        public void BuildLayerDepth()
        {
            animation.LayerDepth = 1f;
        }

        public Animation GetProduct()
        {
            return animation;
        }
    }
}