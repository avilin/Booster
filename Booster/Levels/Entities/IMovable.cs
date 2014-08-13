using Microsoft.Xna.Framework;

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