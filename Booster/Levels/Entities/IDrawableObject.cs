using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.Levels.Entities
{
    public interface IDrawableObject
    {
        bool Active { get; set; }
        Rectangle DestinationRect { get; }
        void Draw(SpriteBatch spriteBatch);
    }
}
