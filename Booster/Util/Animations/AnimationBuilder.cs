using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util.Animations
{
    public class AnimationBuilder:IAnimationBuilder
    {
        private Animation animation = new Animation();

        public AnimationBuilder(Texture2D spriteSheet, List<Frame> frames, Box box)
        {
            animation.Initialize(spriteSheet, frames, box);
        }

        public void BuildScale(float scale)
        {
            animation.Scale = scale;
        }

        public void BuildLooping(bool looping)
        {
            animation.Looping = looping;
        }

        public void BuildColor(Color color)
        {
            animation.Color = color;
        }

        public void BuildLayerDepth(float layerDepth)
        {
            animation.LayerDepth = layerDepth;
        }

        public void BuildPosition(Vector2 position)
        {
            animation.Position = position;
        }

        public Animation GetProduct()
        {
            return animation;
        }
    }
}
