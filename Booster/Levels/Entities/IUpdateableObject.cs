using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public interface IUpdateableObject
    {
        bool Active { get; set; }
        void Update(GameTime gameTime);
    }
}