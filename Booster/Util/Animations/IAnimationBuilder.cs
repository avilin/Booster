using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Util.Animations
{
    public interface IAnimationBuilder
    {
        void BuildTexture();
        void BuildFrames();
        void BuildDestinationRect();
        void BuildScale();
        void BuildLooping();
        void BuildLayerDepth();
        Animation GetProduct();
    }
}
