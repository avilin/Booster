using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Booster.Levels.Entities
{
    public class Key : SimpleTile
    {
        public SoundEffect CollisionSound { get; set; }

        public Key(Vector2 position)
            : base(position)
        {

        }
        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (!Active)
            {
                return;
            }
            if (collisionableObject is IKeyOwner)
            {
                CollisionSound.Play();
                ((IKeyOwner)collisionableObject).Keys++;
            }
            Active = false;
        }
    }
}