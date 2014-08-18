using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Booster.Levels.Entities
{
    public class Door: SimpleTile
    {
        public SoundEffect CollisionSound { get; set; }

        public Door(Vector2 position)
            : base(position)
        {

        }

        public override void OnCollision(ICollisionable collisionableObject)
        {
            if (!Active)
            {
                return;
            }
            if (collisionableObject is IKeyOwner && ((IKeyOwner)collisionableObject).Keys > 0)
            {
                CollisionSound.Play();
                ((IKeyOwner)collisionableObject).Keys--;
                Active = false;
            }
        }
    }
}