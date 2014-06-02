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
        void BuildScale(float scale);
        void BuildLooping(bool looping);
        void BuildColor(Color color);
        void BuildLayerDepth(float layerDepth);
        void BuildPosition(Vector2 position);
        Animation GetProduct();
    }
}
