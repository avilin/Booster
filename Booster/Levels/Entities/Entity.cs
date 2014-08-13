using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public abstract class Entity
    {
        public bool Active { get; set; }
        public Vector2 Position { get; set; }

        protected Entity(Vector2 position)
        {
            Active = true;
            Position = position;
        }
    }
}