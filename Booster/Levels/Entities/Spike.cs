using Booster.Util;
using Microsoft.Xna.Framework;

namespace Booster.Levels.Entities
{
    public class Spike : DamageObject, IMoveable
    {
        private Range range;
        private bool onRangeLimit;
        private Duration onRangeLimitDuration;

        public Vector2 Speed { get; set; }

        public Spike(Vector2 position)
            : base(position)
        {
            Speed = Vector2.UnitY * 0.02f;
            range = new Range((int)position.Y, (int)position.Y + 32);
            onRangeLimit = true;
            onRangeLimitDuration = new Duration(2000);
        }

        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (Position.Y == range.Max)
            {
                return;
            }
            if (collisionableObject is IDamageable)
            {
                ((IDamageable)collisionableObject).LoseLife(Damage);
            }
        }

        public void Move(GameTime gameTime, Map map)
        {
            if (onRangeLimit)
            {
                onRangeLimit = !onRangeLimitDuration.Update(gameTime);
                return;
            }

            Position = Position + Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!range.IsInRange((int)Position.Y))
            {
                float y = MathHelper.Clamp(Position.Y, range.Min, range.Max);
                Position = Vector2.UnitX * Position + Vector2.UnitY * y;
                Speed *= -1;
                onRangeLimit = true;
            }
        }
    }
}