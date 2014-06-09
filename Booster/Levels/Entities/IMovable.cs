using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public interface IMovable
    {
        bool Active { get; set; }
        Vector2 Position { get; set; }
        Vector2 Speed { get; set; }

        void Move(GameTime gameTime, Map map);
    }
}
