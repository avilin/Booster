using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public interface IMovable
    {
        Vector2 Position { get; set; }
        Vector2 Speed { get; set; }

        void ApplyAcceleration(GameTime gameTime, Vector2 acceleration);
        Vector2 GetNextPosition(GameTime gameTime);
    }
}
