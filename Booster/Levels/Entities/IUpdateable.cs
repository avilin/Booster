using System;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public interface IUpdateableObject
    {
        Boolean Active { get; set; }
        void Update(GameTime gameTime);
    }
}